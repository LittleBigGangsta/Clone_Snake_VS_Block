using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{

    public GameObject RestartScreen;
    public GameObject WinScreen;
    public GameObject RestartButton;

    public PlayerMovement PlayerMovement;

    public AudioSource MainTheme;

    public enum State
    {
        Playing,
        Won,
        Loss,
    }

    public State currentState { get; private set; }


    public void OnPlayerDied()
    {
        if (currentState != State.Playing) return;

        currentState = State.Loss;
        PlayerMovement.enabled = false;
        Debug.Log("GameOver");
        RestartScreen.SetActive(true);
        RestartButton.SetActive(false);

        /*GameOverSound.Play();*/
    }

    public void OnPlayerReachedFinish()
    {
        if (currentState != State.Playing) return;

        currentState = State.Won;
        PlayerMovement.enabled = false;
        Debug.Log("You won!");
        WinScreen.SetActive(true);

        /*WinSound.Play();*/
    }

    public void MainThemePlay()
    {
        MainTheme.Play();
    }

    public void MainThemeStop()
    {
        MainTheme.Stop();
    }
}
