using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
	[Header("Move")]
	[Range(0.001f, 1f)]
	public float steerSpeed = 0.2f;
	public AnimationCurve motorTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));
	public AnimationCurve turnInput = AnimationCurve.Linear(-1f, -1f, 1f, 1f);
	[Range(2, 16)]
	public float diffGearing = 4f;
	public float brakeForce = 1500f;
	[Range(0, 45f)]
	public float steerAngle = 30f;

	[Header("Physics")]
	public Vector3 centerOfMass;

	[Header("Wheels")]
	[SerializeField] WheelCollider[] driveWheel;
	public WheelCollider[] DriveWheel { get { return driveWheel; } }
	[SerializeField] WheelCollider[] turnWheel;
	public WheelCollider[] TurnWheel { get { return turnWheel; } }

	public float speed { get; private set; }
	public float throttle { get; private set; }
	public float steering { get; private set; }

	IInput m_input;
	Rigidbody m_rigidBody;
	WheelCollider[] m_wheels;
	bool m_isGrounded;
	int m_lastGroundCheck;
	bool waterCollision;

	private void Start()
	{
		m_input = GetComponent<IInput>();
		m_rigidBody = GetComponent<Rigidbody>();

		m_wheels = GetComponentsInChildren<WheelCollider>();

		m_rigidBody.centerOfMass = centerOfMass;

		foreach (WheelCollider wheel in m_wheels)
		{
			wheel.motorTorque = 0.0001f;
		}
	}

	private void FixedUpdate()
	{
		GroundCheck();
		Move();
	}

	void Move()
	{
		speed = transform.InverseTransformDirection(m_rigidBody.velocity).z * 3.6f;

		throttle = GetInput(GameConstants.k_AxisNameVertical);
		throttle = Mathf.Clamp(throttle, -1f, 1f);
		steering = turnInput.Evaluate(GetInput(GameConstants.k_AxisNameHorizontal)) * steerAngle;

		foreach (WheelCollider wheel in turnWheel)
		{
			wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, steering, steerSpeed);
		}

		foreach (WheelCollider wheel in m_wheels)
		{
			wheel.brakeTorque = 0;
		}

		if (Mathf.Abs(speed) < 4 || Mathf.Sign(speed) == Mathf.Sign(throttle))
		{
			foreach (WheelCollider wheel in driveWheel)
			{
				wheel.motorTorque = throttle * motorTorque.Evaluate(speed) * diffGearing / driveWheel.Length;
			}
		}
		else
		{
			foreach(WheelCollider wheel in m_wheels)
			{
				wheel.brakeTorque = Mathf.Abs(throttle) * brakeForce;
			}
		}
	}

	float GetInput(string input)
	{
		return Input.GetAxis(input);
	}

	public bool GroundCheck()
	{
		if (m_lastGroundCheck == Time.frameCount)
		{
			return m_isGrounded;
		}

		m_lastGroundCheck = Time.frameCount;
		m_isGrounded = true;
		foreach (WheelCollider wheel in m_wheels)
		{
			if (!wheel.gameObject.activeSelf || !wheel.isGrounded)
			{
				m_isGrounded = false;
			}
		}

		return m_isGrounded;
	}

	public void OnWater()
	{
		print("Water");
	}
}
