using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalonCollider : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Water")
		{
			PlayerCarController playerCar = transform.root.GetComponent<PlayerCarController>();
			if (playerCar)
			{
				playerCar.OnWater();
			}
		}
	}
}
