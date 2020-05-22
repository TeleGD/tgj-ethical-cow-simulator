using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
	public GameObject cowPrefab; 
	//Production rate in Cows/seconds
	private float productionRate = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(produceCows());
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator produceCows() {
		yield return new WaitForSeconds(2);
		while (true) {
			productionRate += 0.01f;
			GameObject cow = Instantiate(cowPrefab, transform.GetChild(0).position, Quaternion.Euler(0, 90, 0));
			cow.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -5f);
			GetComponent<Animation>().Play();
			yield return new WaitForSeconds(1 / productionRate);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.GetChild(0).position, 0.5f);
	}
}
