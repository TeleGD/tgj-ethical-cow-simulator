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
			rb.AddForce(new Vector3(Random.Range(-100, 100) / 100f, 2, Random.Range(-100, 100) / 100f) * 100);
			GetComponent<AudioSource>().pitch = Random.Range(80, 120) / 100f;
			GetComponent<AudioSource>().Play();
		}
	}

}
