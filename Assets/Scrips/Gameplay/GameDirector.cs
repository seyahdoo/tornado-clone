using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{

    public GameSettings gameSettings;

    private int currentLevelCount = 0;
    public string currentlyLoadedLevelPath = "";
    private bool switchingLevel = false;
    private bool waitingForTouchToStart = false;

    public GameObject levelFinishedUIObject;
    public GameObject gameFinishedUIObject;
    public GameObject gameOverUIObject;
    public GameObject touchToStartUIObject;

    public TornadoPhysics tornadoPhysics;
    public TornadoController tornadoController;
    public Car car;

    private void Start()
    {
        //Load first level on game start
        StartCoroutine(
            SwitchLevel(
                gameSettings.levels[0], 0f));
    }

    public IEnumerator SwitchLevel(SceneReference newlevel, float delay)
    {
        //dont reenter this routine while its working
        if (switchingLevel) yield break;
        switchingLevel = true;

        yield return new WaitForSeconds(delay);

        //Disable car and tornado, so there is no unnessesary interaction(collision and triggering) on load time
        car.gameObject.SetActive(false);
        tornadoPhysics.gameObject.SetActive(false);

        AsyncOperation op;

        //if there is an already loaded old level, UNLOAD old level
        if(currentlyLoadedLevelPath.Length >= 1)
        {
            op = SceneManager.UnloadSceneAsync(currentlyLoadedLevelPath, UnloadSceneOptions.None);
            while (!op.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        //LOAD new level
        op = SceneManager.LoadSceneAsync(newlevel.ScenePath, LoadSceneMode.Additive);
        while (!op.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        currentlyLoadedLevelPath = newlevel.ScenePath;

        //place car and enable it
        car.SetNewPath(FindObjectOfType<CarPath>());
        car.gameObject.SetActive(true);

        //place tornado and enable it
        tornadoController.enabled = false;
        tornadoPhysics.enabled = false;
        tornadoPhysics.tr.position = FindObjectOfType<TornadoStartPoint>().transform.position;
        tornadoPhysics.gameObject.SetActive(true);

        //wait for touch to start the levels
        waitingForTouchToStart = true;

        //"TOUCH TO START" text
        ClearUI();
        touchToStartUIObject.SetActive(true);

        //reenter semaphore released
        switchingLevel = false;
    }

    private void Update()
    {
        //Wait for touch to start the game
        if (waitingForTouchToStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                waitingForTouchToStart = false;
                car.StartPathFollowing();
                tornadoPhysics.enabled = true;
                tornadoController.enabled = true;
                ClearUI();
            }
        }
    }

    public void LevelFinished()
    {
        //Pause Game
        car.StopPathFollowing();
        tornadoController.enabled = false;

        //load next level
        //if there is no next level -> "Game Finished"
        currentLevelCount++;
        if (currentLevelCount >= gameSettings.levels.Length)
        {
            //"GAME FINISHED" text
            ClearUI();
            gameFinishedUIObject.SetActive(true);
        }
        else
        {
            //"LEVEL FINISHED" text
            ClearUI();
            levelFinishedUIObject.SetActive(true);

            StartCoroutine(
                SwitchLevel(
                    gameSettings.levels[currentLevelCount], 1.5f));
        }

    }

    public void GameOver()
    {
        //Pause game
        car.StopPathFollowing();
        tornadoController.enabled = false;

        //"GAME OVER" text
        ClearUI();
        gameOverUIObject.SetActive(true);

        //laod first level
        currentLevelCount = 0;
        StartCoroutine(
            SwitchLevel(
                gameSettings.levels[currentLevelCount], 1.5f));

    }

    public void ClearUI()
    {
        gameOverUIObject.SetActive(false);
        gameFinishedUIObject.SetActive(false);
        levelFinishedUIObject.SetActive(false);
        touchToStartUIObject.SetActive(false);
    }

}
