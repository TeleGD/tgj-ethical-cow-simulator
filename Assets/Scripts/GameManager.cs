using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool gameStarted = false;

    public Text scoreDisplay;
	public Text timeDisplay;

	public int steakCount = 0;
    public int deadCows = 0;
	public float secondsPlayed = 0;

    private Vector3 gameCamPos;
    private Quaternion gameCamRot;

    void Start()
    {
        instance = this;
        Transform camPos = GameObject.Find("Menu Camera Pos").transform;
        gameCamPos = Camera.main.transform.position;
        gameCamRot = Camera.main.transform.rotation;
        Camera.main.transform.SetPositionAndRotation(camPos.position, camPos.rotation);

		timeDisplay.enabled = false;
	}

    public void StartGame()
    {
        gameStarted = true;
        Factory.instance.StartGame();

		timeDisplay.enabled = true;
	}

    private void Update()
    {
        if(gameStarted)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, gameCamPos, Time.deltaTime * 3);

			secondsPlayed += Time.deltaTime;

			timeDisplay.text = "Time Played:\n";
			timeDisplay.text += (System.Math.Floor(secondsPlayed / 3600) > 1) ? System.Math.Floor(secondsPlayed / 3600)+"h : " : "" ;
			timeDisplay.text += System.Math.Floor(secondsPlayed / 60) %60 + "m : ";
			timeDisplay.text += System.Math.Floor(secondsPlayed) % 60 + "s";
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
