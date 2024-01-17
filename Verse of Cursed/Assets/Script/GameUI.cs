using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameUI : MonoBehaviour
{
    public GameObject gameLoseUI;
    public GameObject gameWinUI;
    public GameObject gameRespawnUI;

    private bool gameIsOver;
    private bool gameIsOverFromFall;
    // Start is called before the first frame update
    void Start()
    {
        Guard.OnGuardHasSpottedPlayer += ShowGameLoseUI;
        PlayerController.OnPlayerRespawn += ShowRespawnUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            { 
                SceneManager.LoadScene(0);
            }
        }
        
        if (gameIsOverFromFall)
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
    }

    void OnPlayerRespawnAfterFalling(GameObject gameRespawnUI)
    {
        gameRespawnUI.SetActive(true);
        gameIsOverFromFall = true;
        PlayerController.OnPlayerRespawn -= ShowRespawnUI;

    }
}
