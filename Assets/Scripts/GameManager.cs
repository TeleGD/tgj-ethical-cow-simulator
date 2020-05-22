using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool gameStarted = false;

    public Text scoreDisplay;

    public int steakCount = 0;
    public int deadCows = 0;

    private Vector3 gameCamPos;
    private Quaternion gameCamRot;

    void Start()
    {
        instance = this;
        Transform camPos = GameObject.Find("Menu Camera Pos").transform;
        gameCamPos = Camera.main.transform.position;
        gameCamRot = Camera.main.transform.rotation;
        Camera.main.transform.SetPositionAndRotation(camPos.position, camPos.rotation);
    }

    public void StartGame()
    {
        gameStarted = true;
        Factory.instance.StartGame();
    }

    private void Update()
    {
        if(gameStarted)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, gameCamPos, Time.deltaTime * 3);
        }
    }

    public void FallenCow()
    {
        deadCows++;
        UpdateUI();
    }

    public void AddSteak()
    {
        steakCount++;
        UpdateUI();
    }

    public void RemoveSteaks(int amount)
    {
        steakCount -= amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreDisplay.text = "Steaks : " + steakCount + "\nCasualties : " + deadCows;
    }
}
