﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool gameStarted = false;
    public static bool infinityMode = false;

    public Text scoreDisplay;
	public Text timeDisplay;
    public Text volumeDisplay;

    public int steakCount = 0;
    public int deadCows = 0;
	public float secondsPlayed = 0;

    public int gameOverThreshold = 20;

    private Vector3 gameCamPos;

    void Start()
    {
        instance = this;
        Transform camPos = GameObject.Find("Menu Camera Pos").transform;
        gameCamPos = Camera.main.transform.position;
        Camera.main.transform.SetPositionAndRotation(camPos.position, camPos.rotation);
        SetVolume(50);
	}

    public void StartGame(bool infinity)
    {
        gameStarted = true;
        scoreDisplay.gameObject.SetActive(true);
        timeDisplay.gameObject.SetActive(true);
        infinityMode = infinity;
        Factory.instance.StartGame();
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

        if (!infinityMode && deadCows >= gameOverThreshold)
        {
            SceneManager.LoadScene(0);
        }

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

    public void SetVolume(float vol)
    {
        AudioListener.volume = vol / 100f;
        volumeDisplay.text = "Sound volume : " + (int)vol + "%";
    }
}
