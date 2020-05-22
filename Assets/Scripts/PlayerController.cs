using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody body;
    public float speed = 20;
    public float friction = 10;

    private void Start()
    {
        body = GetComponent<Rigidbody>();   
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(-body.velocity.z, 0, body.velocity.x) * 4;
    }

    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(dir.sqrMagnitude > 1)
            dir.Normalize();
        body.AddForce(dir * speed);
        body.AddForce(FlattenVector(-body.velocity * friction));
    }

    public Vector3 FlattenVector(Vector3 vec)
    {
        return new Vector3(vec.x, 0, vec.z);
    }
}
