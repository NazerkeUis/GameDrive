using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Checkpoints")]
    public int currentCheckpoint = 0;
    public int totalCheckpoints = 4;

    [Header("Lives")]
    public int lives = 3;

    [Header("Game State")]
    public bool gateOpened = false;
    public bool gameFinished = false;

    [Header("References")]
    public GateController gate;

    [Header("UI")]
    public TMP_Text livesText;
    public TMP_Text checkpointText;
    public GameObject gameOverText;
    public GameObject youWinText;

    [Header("Scene Return Delay")]
    public float returnDelay = 2f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateUI();

        if (gameOverText != null)
            gameOverText.SetActive(false);

        if (youWinText != null)
            youWinText.SetActive(false);
    }

    public void PassCheckpoint(int checkpointNumber)
    {
        if (gameFinished) return;

        if (checkpointNumber == currentCheckpoint + 1)
        {
            currentCheckpoint++;
            Debug.Log("Checkpoint " + checkpointNumber + " passed!");
            UpdateUI();
        }
    }

    public void ReachFinishZone()
    {
        if (gameFinished) return;

        if (currentCheckpoint == totalCheckpoints && !gateOpened)
        {
            gateOpened = true;

            if (gate != null)
                gate.OpenGate();

            Debug.Log("Gate opened!");
        }
        else
        {
            Debug.Log("Pass all checkpoints first!");
        }
    }

    public void ReachWinZone()
    {
        if (gameFinished) return;

        if (gateOpened)
        {
            gameFinished = true;
            Debug.Log("YOU WIN!");

            if (youWinText != null)
                youWinText.SetActive(true);

            StartCoroutine(LoadMenu());
        }
    }

    public void LoseLife()
    {
        if (gameFinished) return;

        lives--;
        Debug.Log("Lives left: " + lives);
        UpdateUI();

        if (lives <= 0)
        {
            gameFinished = true;
            Debug.Log("GAME OVER!");

            if (gameOverText != null)
                gameOverText.SetActive(true);

            StartCoroutine(LoadMenu());
        }
    }

    void UpdateUI()
    {
        if (livesText != null)
        {
            livesText.text = GetLivesDisplay();
        }

        if (checkpointText != null)
        {
            checkpointText.text = "Checkpoints: " + currentCheckpoint + "/" + totalCheckpoints;
        }
    }

    string GetLivesDisplay()
    {
        string hearts = "";

        for (int i = 0; i < lives; i++)
        {
            hearts += "♥ ";
        }

        return hearts;
    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(returnDelay);
        SceneManager.LoadScene("menu");
    }
}