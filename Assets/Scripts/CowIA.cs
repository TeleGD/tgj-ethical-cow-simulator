using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowIA : MonoBehaviour
{

	public float speed = 5;
	public float directionChangeInterval = 3;
	public float maxHeadingChange = 30;

	public Rigidbody rb;
	float heading;
	Vector3 targetRotation;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();

		// Set random initial rotation
		heading = Random.Range(0, 360);
		transform.eulerAngles = new Vector3(0, heading, 0);

		StartCoroutine(NewHeading());
	}

	void Update()
	{
		//transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
		
	}

	/// <summary>
	/// Repeatedly calculates a new direction to move towards.
	/// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
	/// </summary>
	IEnumerator NewHeading()
	{
		while (true)
		{
			NewHeadingRoutine();
			yield return new WaitForSeconds(Random.Range(-100, 100) / 50f);
			
		}
	}

	/// <summary>
	/// Calculates a new direction to move towards.
	/// </summary>
	void NewHeadingRoutine()
	{
		/*var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
		var ceil = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
		heading = Random.Range(floor, ceil);
		targetRotation = new Vector3(0, heading, 0);*/
		rb.velocity = new Vector3(Random.Range(-100, 100) / 50f, Random.Range(-100, 100) / 50f, Random.Range(-100, 100) / 50f);
	}

}
