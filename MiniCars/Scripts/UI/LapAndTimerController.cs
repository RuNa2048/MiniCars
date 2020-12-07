using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LapAndTimerController : MonoBehaviour
{
	[Header("References")]
	public GameObject racePanel;
	public TextMeshProUGUI textCurrentLap;
	public TextMeshProUGUI textCurrentTime;
	public TextMeshProUGUI textLastLap;
	public TextMeshProUGUI textBestTime;
	public CheckpointHandler checkpointHandler;

	float m_currentLap = -1;
	float m_currentTime;
	float m_lastLap;
	float m_bestTime;

	private void Update()
	{
		if (checkpointHandler == null)
			return;

		if (checkpointHandler.currentLap != m_currentLap)
		{
			m_currentLap = checkpointHandler.currentLap;
			textCurrentLap.text = $"LAP: {m_currentLap}";
		}

		if (checkpointHandler.currentLapTime != m_currentTime)
		{
			m_currentTime = checkpointHandler.currentLapTime;
			textCurrentTime.text = $"TIME: {(int)m_currentTime / 60}:{(m_currentTime) % 60:00.000}";
		}

		if (checkpointHandler.lastLapTime != m_lastLap)
		{
			m_lastLap = checkpointHandler.lastLapTime;
			textLastLap.text = $"LAST {(int)m_lastLap / 60}:{m_lastLap % 60:00.000}";
		}

		if (checkpointHandler.bestLapTme != m_bestTime)
		{
			m_bestTime = checkpointHandler.bestLapTme;
			textBestTime.text = m_bestTime < 1000000 ? $"BEST {(int)m_bestTime / 60}:{m_bestTime % 60:00.000}" : "BEST: NONE";
		}
	}
}
