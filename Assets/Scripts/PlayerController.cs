using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody body;
    public float speed = 20;
    public float friction = 10;
    private GameObject grabbedCow;

    private void Start()
    {
        body = GetComponent<Rigidbody>();   
    }

    private void Update()
    {
        print(grabbedCow);
        //attrape vache
        if(Input.GetButtonDown("Jump"))
        {
            //trouve la vache la plus proche
            GameObject[] cows = GameObject.FindGameObjectsWithTag("Cow");
            if(cows.Length != 0)
            {
                float nearestDist = 4;
                for (int i = 0; i < cows.Length; i++)
                {
                    float dist = Vector3.SqrMagnitude(transform.position - cows[i].transform.position);
                    if (dist < nearestDist)
                    {
                        grabbedCow = cows[i];
                        dist = nearestDist;
                    }
                }
            }
        }

        //relache vache
        if (Input.GetButtonUp("Jump"))
        {
            grabbedCow = null;
        }

        //animation du joueur
        transform.eulerAngles = new Vector3(-body.velocity.z, 0, body.velocity.x) * 4;
    }

    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(dir.sqrMagnitude > 1)
            dir.Normalize();
        body.AddForce(dir * speed);
        body.AddForce(FlattenVector(-body.velocity * friction));

        if(grabbedCow != null)
        {
            Vector3 cowDir = (transform.position - grabbedCow.transform.position);
            grabbedCow.GetComponent<Rigidbody>().AddForce(cowDir * cowDir.magnitude * 10);
        }
    }

    public Vector3 FlattenVector(Vector3 vec)
    {
        return new Vector3(vec.x, 0, vec.z);
    }
}
