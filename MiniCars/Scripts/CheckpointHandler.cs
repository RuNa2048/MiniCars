using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{
	public float bestLapTme { get; private set; } = Mathf.Infinity;
	public float lastLapTime { get; private set; } = 0;
	public float currentLapTime { get; private set; } = 0;
	public int currentLap { get; private set; } = 0;

	Transform m_checkpointsParent;
	PlayerCarController m_playerCarController;
	float m_lapTimerStamp;
	int m_lastCheckpointPassed = 0;
	int m_countCheckpoints;
	int m_checkpointLayer;

	private void Awake()
	{
		m_checkpointsParent = GameObject.Find("Checkpoints").transform;

		m_countCheckpoints = m_checkpointsParent.childCount;
		m_checkpointLayer = LayerMask.NameToLayer("Checkpoint");
	}

	private void Update()
	{
		currentLapTime = m_lapTimerStamp >= 0 ? Time.time - m_lapTimerStamp : 0f;
	}

	void StartLap()
	{
		//
		Debug.Log("Start lap!");
		//
		currentLap++;
		m_lastCheckpointPassed = 1;
		m_lapTimerStamp = Time.time;
	}

	void EndLap()
	{
		//
		Debug.Log("End lap!");
		//
		lastLapTime = Time.time - m_lapTimerStamp;
		bestLapTme = Mathf.Min(lastLapTime, bestLapTme);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer != m_checkpointLayer)
		{
			return;
		}

		if (other.gameObject.name == "1")
		{
			if (m_lastCheckpointPassed == m_countCheckpoints)
			{
				EndLap();
			}

			if (currentLap == 0 || m_lastCheckpointPassed == m_countCheckpoints)
			{
				StartLap();
			}

			return;
		}

		if (other.gameObject.name == (m_lastCheckpointPassed + 1).ToString())
		{
			m_lastCheckpointPassed++;
			//
			Debug.Log(m_lastCheckpointPassed);
			//
		}
	}
}
