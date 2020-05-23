using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaughterHouse : MonoBehaviour
{
	public GameObject steackPrefab;

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cow"))
        {
            GameManager.instance.AddSteak();
            PlayerController.instance.ReleaseCow();
            StartCoroutine(SuckCow(other.gameObject));
        }
    }

    IEnumerator SuckCow(GameObject cow)
    {
        Vector3 startPos = cow.transform.position;
        cow.GetComponent<Collider>().enabled = false;
        cow.GetComponent<Rigidbody>().isKinematic = true;

        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime * 2;
            cow.transform.position = Vector3.Lerp(startPos, transform.position + Vector3.right * 3, t);
            yield return new WaitForEndOfFrame();
        }
		ThrowSteack();
        Destroy(cow);
    }

	public void ThrowSteack()
	{
			GameObject steack = Instantiate(steackPrefab, transform.parent.GetChild(0).position, Quaternion.Euler(0, 0, 0));
			steack.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 5f);
	}
}
