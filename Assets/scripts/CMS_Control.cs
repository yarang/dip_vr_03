using UnityEngine;
using System.Collections;

public class CMS_Control : MonoBehaviour
{
	public bool magnetDetectionEnabled = true;
	private GameManager gameManager;

	void Awake()
	{
		gameManager = GetComponent<GameManager> ();
		CardboardMagnetSensor.SetEnabled(magnetDetectionEnabled);
		// Disable screen dimming:
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	void Update()
	{
		if (CardboardMagnetSensor.CheckIfWasClicked () || Input.GetMouseButtonDown (0)) {
			gameManager.IsOn_Magnet = true;
			CardboardMagnetSensor.ResetClick ();
		}
	}
}

