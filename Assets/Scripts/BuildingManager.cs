using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Transform ghostItems;
    private bool rotate = false;
    private int currentItem;

    public GameObject[] prefabs;
    public int[] prices;

    public Material canBuild;
    public Material cannotBuild;

    private void Start()
    {
        for (int i = 0; i < ghostItems.childCount; i++)
        {
            ghostItems.GetChild(i).gameObject.SetActive(i == 0);
        }
    }

    private void Update()
    {
        if (!GameManager.gameStarted)
            return;

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                currentItem++;
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                currentItem--;
            if (currentItem < 0)
                currentItem = ghostItems.childCount - 1;
            if (currentItem >= ghostItems.childCount)
                currentItem = 0;

            GameManager.instance.UpdatePrice(prices[currentItem]);

            for(int i = 0; i < ghostItems.childCount; i++)
            {
                ghostItems.GetChild(i).gameObject.SetActive(i == currentItem);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            rotate = !rotate;
            ghostItems.eulerAngles = new Vector3(0, rotate ? 90 : 0, 0);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Buildable"))
        {
            ghostItems.position = hit.point;
            if(GameManager.instance.steakCount >= prices[currentItem])
            {
                for (int i = 0; i < ghostItems.childCount; i++)
                {
                    ghostItems.GetChild(i).GetComponent<MeshRenderer>().material = canBuild;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    GameManager.instance.RemoveSteaks(prices[currentItem]);
                    Instantiate(prefabs[currentItem], hit.point + ghostItems.GetChild(currentItem).localPosition, ghostItems.rotation);
                }
            }
            else
            {
                for (int i = 0; i < ghostItems.childCount; i++)
                {
                    ghostItems.GetChild(i).GetComponent<MeshRenderer>().material = cannotBuild;
                }
            }
        }
        else
            ghostItems.position = Vector3.down * 10;
    }
}
