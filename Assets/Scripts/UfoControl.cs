using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UfoControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(TranscendCow());
    }

    IEnumerator TranscendCow()
    {
        while (true)
        {
            GameObject[] cows = GameObject.FindGameObjectsWithTag("Cow");
            int i = Random.Range(0, cows.Length - 1);
            GameObject cow = cows[i];
            Vector3 endPos = new Vector3(cow.transform.position.x, transform.position.y, cow.transform.position.z);

            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * 2;
                transform.position = Vector3.Lerp(transform.position, endPos, t);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(1);
            t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * 2;
                cow.GetComponent<Rigidbody>().isKinematic = true;
                cow.transform.position = transform.position + Vector3.down *((1-t)*transform.position.y);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(1);
            GameObject hooker = GameObject.Find("Hooker");
            endPos = new Vector3(hooker.transform.position.x, transform.position.y, hooker.transform.position.z);
            t = 0;
            while (t < 1)
            {
                
                t += Time.deltaTime * 2;
                transform.position = Vector3.Lerp(transform.position, endPos, t);
                cow.transform.position = transform.position + Vector3.down*t;
                yield return new WaitForEndOfFrame();
            }
            cow.GetComponent<Rigidbody>().isKinematic = false;
            yield return new WaitForSeconds(1);
        }
        
    }

}
