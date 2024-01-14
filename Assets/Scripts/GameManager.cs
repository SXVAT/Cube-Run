using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class    GameManager : MonoBehaviour
{
    public GameObject GameOverGO;
    public GameObject playButton;
    public GameObject enemySpawner;
    public GameObject playerShip;
    public GameObject scoreUITextGO;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver
    }

    GameManagerState GMState;

    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    void UpdateGameManagerState()
    {
        switch(GMState)
        {
            case GameManagerState.Opening:

                GameOverGO.SetActive(false);

                playButton.SetActive(true);

                break;
            case GameManagerState.Gameplay:

                scoreUITextGO.GetComponent<GameScore>().Score = 0;
                
                playButton.SetActive(false);

                playerShip.GetComponent<PlayerControl>().Init();

                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                
                break;
            case GameManagerState.GameOver:

                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();

                GameOverGO.SetActive(true);

                Invoke("ChangeToOpeningState", 8f);

                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();   
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }
     
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
