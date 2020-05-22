using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
	public GameObject cowPrefab; 
	//Production rate in Cows/seconds
	private float productionRate = 0.5f;
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
			GameObject cow = Instantiate(cowPrefab, new Vector3(-4.3f, 0.4f, 3.7f), Quaternion.Euler(0, 90, 0));
			cow.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -5f);
		}
	}
}
