using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    
    public Transform memoryGamePrefab;
    public GameObject startGameButton;
    public Text timerText;
    public Text btnText;

    MemoryGame memoryGame;
    Transform memGameTransform;
    bool normalMode = false;
    bool playing = false;

    float timer = 0;
    DBHandler dbHandler;

	// Use this for initialization
	void Start () {
        //GenerateBoard(normalSprites.Count);
        dbHandler = new DBHandler();

	}
	
	// Update is called once per frame
	void Update () {
        if (playing) {
            timer += Time.deltaTime;
            timerText.text = timer.ToString("F0");
        }
        
    }

    public void startGame()
    {
        if (normalMode)
            StartNormalGame();
        else
            StartAlternateGame();
        timer = 0f;
        playing = true;
    }

    public void StartNormalGame()
    {
        if(memGameTransform != null)
            Destroy(memGameTransform.gameObject);
        startGameButton.SetActive(false);
        memGameTransform = (Transform)Instantiate(memoryGamePrefab,Vector2.zero, transform.rotation);
        memoryGame = memGameTransform.GetComponent<MemoryGame>();
        memoryGame.StartGame(this, true);
    }

    public void StartAlternateGame()
    {
        if (memGameTransform != null)
            Destroy(memGameTransform.gameObject);
        startGameButton.SetActive(false);
        memGameTransform = (Transform)Instantiate(memoryGamePrefab, Vector2.zero, transform.rotation);
        memoryGame = memGameTransform.GetComponent<MemoryGame>();
        memoryGame.StartGame(this, false);
    }

    public void GameOver(int clicks)
    {
        playing = false;

        //SEND CLICKS AND TIMER TO DATABASE
        dbHandler.addSessionWWW(clicks, timer, normalMode);

        normalMode = !normalMode; //Change game mode for next game
        startGameButton.SetActive(true);
        btnText.text = "Next Game";
    }

}
