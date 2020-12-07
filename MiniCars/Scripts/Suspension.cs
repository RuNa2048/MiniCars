using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspension : MonoBehaviour
{
	public GameObject wheelModel;
	public Vector3 localRotOffset;

	WheelCollider m_wheelCollider;

    void Start()
    {
		m_wheelCollider = GetComponent<WheelCollider>();
    }

    void FixedUpdate()
    {
		if (wheelModel && m_wheelCollider)
		{
			Vector3 pos;
			Quaternion rot;
			m_wheelCollider.GetWorldPose(out pos, out rot);

			wheelModel.transform.rotation = rot;
			wheelModel.transform.localRotation *= Quaternion.Euler(localRotOffset);
			wheelModel.transform.position = pos;
		}
    }
}
