using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject fence;
    public Transform ghostFence;
    private bool rotate = false;

    private void Update()
    {
        if (!GameManager.gameStarted)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            rotate = !rotate;
            ghostFence.eulerAngles = new Vector3(0, rotate ? 90 : 0, 0);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Buildable"))
        {
            ghostFence.position = hit.point;
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(fence, hit.point, ghostFence.rotation);
            }
        }
        else
            ghostFence.position = Vector3.down * 2;
    }
}
