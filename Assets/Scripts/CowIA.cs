using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowIA : MonoBehaviour
{
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		StartCoroutine(RepeatJump());
	}

	private void Update()
	{
		if(transform.position.y < -20)
		{
			GameManager.instance.FallenCow();
			Destroy(gameObject);
		}
	}

	IEnumerator RepeatJump()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(20, 60) / 15f);

			Transform closestHayStraw = getClosestHayStraw();

			if(closestHayStraw == null)
				rb.AddForce(new Vector3(Random.Range(-100, 100) / 100f, 2, Random.Range(-100, 100) / 100f) * 100);
			else
			{
				//rb.AddForce(new Vector3(closestHayStraw.position.x + Random.Range(-25, 25) / 100f, 2, closestHayStraw.position.z + Random.Range(-25, 25) / 100f) * 100);
				Vector3 dir = new Vector3(closestHayStraw.position.x - rb.position.x, 0, closestHayStraw.position.z - rb.position.z);

				rb.AddForce(new Vector3(dir.x + Random.Range(-50, 50) / 50f, 20, dir.z + Random.Range(-50, 50) / 50f) *10);
			}
				
			GetComponent<AudioSource>().pitch = Random.Range(80, 120) / 100f;
			GetComponent<AudioSource>().Play();
		}
	}

	public Transform getClosestHayStraw()
	{
		Transform closestHayStraw = null;
		GameObject[] hayStraws = GameObject.FindGameObjectsWithTag("straw");
		if (hayStraws.Length != 0)
		{
			float nearestDist = 10;
			for (int i = 0; i < hayStraws.Length; i++)
			{
				float dist = Vector3.SqrMagnitude(transform.position - hayStraws[i].transform.position);
				if (dist < nearestDist)
				{
					closestHayStraw = hayStraws[i].GetComponent<Transform>();
					dist = nearestDist;
				}
			}
		}
		return closestHayStraw;
	}

}
