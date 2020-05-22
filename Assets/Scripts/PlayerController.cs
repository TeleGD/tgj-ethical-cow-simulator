using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody body;
    public float speed = 20;
    public float friction = 10;
    private Rigidbody grabbedCow;

    public Transform left_arm;
    public Transform right_arm;

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
                        grabbedCow = cows[i].GetComponent<Rigidbody>();
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

        if (grabbedCow != null)
        {
            Vector3 cowDir = transform.position - grabbedCow.transform.position;
            Vector3 right = Vector3.Cross(Vector3.up, cowDir.normalized);

            Vector3 leftDir = transform.position - right - grabbedCow.transform.position;
            left_arm.position = transform.position - leftDir / 2;
            left_arm.rotation = Quaternion.LookRotation(Vector3.up, cowDir);
            left_arm.localScale = new Vector3(0.2f, leftDir.magnitude / 2, 0.2f);

            Vector3 rightDir = transform.position + right - grabbedCow.transform.position;
            right_arm.position = transform.position - rightDir / 2;
            right_arm.rotation = Quaternion.LookRotation(Vector3.up, cowDir);
            right_arm.localScale = new Vector3(0.2f, rightDir.magnitude / 2, 0.2f);
        }
        else
        {
            left_arm.localScale = Vector3.zero;
            right_arm.localScale = Vector3.zero;
        }

        if(transform.position.y < -20)
        {
            transform.position = Vector3.up * 10;
        }
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
            Vector3 cowDir = transform.position - grabbedCow.transform.position;
            if(cowDir.sqrMagnitude > 2)
            {
                grabbedCow.AddForce(cowDir.normalized * speed);
                grabbedCow.AddForce(FlattenVector(-grabbedCow.velocity * friction));
            }
        }
    }

    public Vector3 FlattenVector(Vector3 vec)
    {
        return new Vector3(vec.x, 0, vec.z);
    }
}
