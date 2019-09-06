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
        StartCoroutine(
            SwitchLevel(
                gameSettings.levels[0], 0f));
    }

    public IEnumerator SwitchLevel(SceneReference newlevel, float delay)
    {
        if (switchingLevel) yield break;

        switchingLevel = true;
        yield return new WaitForSeconds(delay);

        ClearUI();

        car.gameObject.SetActive(false);
        tornadoPhysics.gameObject.SetActive(false);

        AsyncOperation op;

        if(currentlyLoadedLevelPath.Length >= 1)
        {
            op = SceneManager.UnloadSceneAsync(currentlyLoadedLevelPath, UnloadSceneOptions.None);
            while (!op.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

        }

        op = SceneManager.LoadSceneAsync(newlevel.ScenePath, LoadSceneMode.Additive);
        while (!op.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        currentlyLoadedLevelPath = newlevel.ScenePath;

        car.gameObject.SetActive(true);
        CarPath path = FindObjectOfType<CarPath>();
        car.SetNewPath(path);

        tornadoController.enabled = false;
        tornadoPhysics.gameObject.SetActive(true);
        TornadoStartPoint tornadoStartPoint = FindObjectOfType<TornadoStartPoint>();
        tornadoPhysics.transform.position = tornadoStartPoint.transform.position;

        ClearUI();
        touchToStartUIObject.SetActive(true);
        waitingForTouchToStart = true;

        switchingLevel = false;
    }

    private void Update()
    {
        if (waitingForTouchToStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                waitingForTouchToStart = false;
                car.StartPathFollowing();
                tornadoController.enabled = true;
                ClearUI();
            }
        }
    }

    public void LevelFinished()
    {
        car.StopPathFollowing();
        tornadoController.enabled = false;
        ClearUI();

        //load next level
        //if there is no next level -> GameFinished()
        currentLevelCount++;
        if (currentLevelCount >= gameSettings.levels.Length)
        {
            gameFinishedUIObject.SetActive(true);
        }
        else
        {
            levelFinishedUIObject.SetActive(true);
            StartCoroutine(
                SwitchLevel(
                    gameSettings.levels[currentLevelCount], 1.5f));
        }

    }

    public void GameOver()
    {
        car.StopPathFollowing();
        tornadoController.enabled = false;
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
