using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour
{
    public GameObject[] players;

    [SerializeField] public GameObject currentPlayer;
    
    
    void Start()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerController>().enabled = false;
        }

        currentPlayer = players[0];
    }

    public void ChangePlayer(GameObject player)
    {
        currentPlayer.GetComponent<PlayerController>().enabled = false;
        currentPlayer = player;
        this.gameObject.SetActive(false);
    }
}
