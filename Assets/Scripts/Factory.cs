using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
	public GameObject cowPrefab; 
	//Production rate in Cows/seconds
	private float productionRate = 0.2f;
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
		while (true) {
			yield return new WaitForSeconds(1/productionRate);
			productionRate += 0.02f;
			GameObject cow = Instantiate(cowPrefab, transform.position, Quaternion.Euler(0, 90, 0));
			cow.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -5f);
		}
	}
}
