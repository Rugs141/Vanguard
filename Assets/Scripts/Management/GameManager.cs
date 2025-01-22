using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;
    public GameObject gameOverUI;

    private void Start()
    {
        gameIsOver = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
        {
            return;
        }
        //REMOVE THIS LATER
        if (Input.GetKeyDown(KeyCode.E))
        {
            EndGame();
        }

        if (PlayerStats.lives <=0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
