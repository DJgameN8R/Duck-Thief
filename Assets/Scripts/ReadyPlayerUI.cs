using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyPlayerUI : MonoBehaviour
{
    public GameObject readyPlayerUI;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState == GameState.preGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameManager.gameState = GameState.game;
                readyPlayerUI.SetActive(false);
            }
        }
    }
}
