using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameUI : MonoBehaviour
{
    public GameObject gameLoseUI;
    public GameObject gameWinUI;
    public GameObject gameRespawnUI;
    private PlayerController disable;

    private bool gameIsOver;
    private bool gameIsOverFromFall;
    void Start()
    {
        Guard.OnGuardHasSpottedPlayer += ShowGameLoseUI;
        PlayerController.OnPlayerRespawn += ShowRespawnUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver || gameIsOverFromFall)
        {
            if (Input.GetKeyDown(KeyCode.R))
            { 
                SceneManager.LoadScene(0);
            }
        }
    }

    void ShowGameWinUI()
    {
        OnGameOver(gameWinUI);
    }
    void ShowGameLoseUI()
    {
        OnGameOver(gameLoseUI);
    }

    void ShowRespawnUI()
    {
        OnPlayerRespawnAfterFalling(gameRespawnUI);
    }

    void OnGameOver(GameObject gameOverUI)
    {
        gameOverUI.SetActive(true);
        gameIsOver = true;
        Guard.OnGuardHasSpottedPlayer -= ShowGameLoseUI;
        PlayerController.OnPlayerRespawn -= ShowRespawnUI;

    }

    void OnPlayerRespawnAfterFalling(GameObject gameRespawnUI)
    {
        gameRespawnUI.SetActive(true);
        gameIsOverFromFall = true;
        PlayerController.OnPlayerRespawn -= ShowRespawnUI;
        Guard.OnGuardHasSpottedPlayer -= ShowGameLoseUI;


    }
}
