using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaughterHouse : MonoBehaviour
{
	public GameObject steackPrefab;
    public ParticleSystem bloodpref;

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
		Invoke("ThrowSteak", 1.5f);
        EmitParticle();
        Destroy(cow);
    }

    public void EmitParticle()
    {
        ParticleSystem blood = Instantiate(bloodpref, transform.parent.GetChild(0).position, Quaternion.Euler(0, 0, 0));
        blood.Play();
    }


    public void ThrowSteak()
	{
		GameObject steak = Instantiate(steackPrefab, transform.parent.GetChild(0).position, Quaternion.Euler(0, 0, 0));
		steak.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-100, 100) / 100f, 4f, 5f);
        steak.GetComponent<Rigidbody>().angularVelocity = Random.onUnitSphere * 20;
        Destroy(steak, 5);
	}
}
