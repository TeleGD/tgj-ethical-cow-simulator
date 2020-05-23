using System.Collections;
using System.Collections.Generic;
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
        GameObject[] cows = GameObject.FindGameObjectsWithTag("Cow");
        int i = Random.Range(0, cows.Length - 1);
        GameObject cow = cows[i];
        Vector3 endPos = new Vector3(cow.transform.position.x, transform.position.z, cow.transform.position.y);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 2;
            transform.position = Vector3.Lerp(transform.position, endPos, t);
            yield return new WaitForEndOfFrame();
        }
    }
}
