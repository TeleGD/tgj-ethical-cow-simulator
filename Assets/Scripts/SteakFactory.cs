﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteakFactory : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cow"))
        {
            Destroy(other.gameObject);
        }
    }
}