using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
	[Header("References")]
	public Transform target;

	[Header("Moving")]
	public Vector3 offset;
	public Vector3 eulerRotation;
	public float damper;

	private void Start()
	{
		transform.eulerAngles = eulerRotation;
	}

	private void Update()
	{
		if (target == null)
			return;

		transform.position = Vector3.Lerp(transform.position, target.position + offset, damper * Time.deltaTime);
	}
}
