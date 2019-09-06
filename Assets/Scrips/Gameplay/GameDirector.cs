using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{

    public string[] levels;

    private int currentLevelCount = 0;
    public string currentlyLoadedLevelName = "";
    private bool switchingLevel = false;

    public GameObject levelFinishedUIObject;
    public GameObject gameFinishedUIObject;
    public GameObject gameOverUIObject;

    public Tornado tornado;
    public Car car;

    private void Start()
    {
        StartCoroutine(SwitchLevel(levels[0], 0f));
    }

    public IEnumerator SwitchLevel(string newlevel, float delay)
    {
        if (switchingLevel) yield break;

        switchingLevel = true;
        yield return new WaitForSeconds(delay);

        CleanUI();

        car.gameObject.SetActive(false);
        tornado.gameObject.SetActive(false);

        AsyncOperation op;

        if(currentlyLoadedLevelName.Length > 0)
        {
            op = SceneManager.UnloadSceneAsync(currentlyLoadedLevelName, UnloadSceneOptions.None);

            while (!op.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            Debug.Log("Unload Level Completed");
        }
        

        op = SceneManager.LoadSceneAsync(newlevel, LoadSceneMode.Additive);
        while (!op.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Load Level Completed");

        currentlyLoadedLevelName = newlevel;

        car.gameObject.SetActive(true);
        CarPath path = FindObjectOfType<CarPath>();
        car.StartPathFollowing(path);

        tornado.gameObject.SetActive(true);
        TornadoStartPoint tornadoStartPoint = FindObjectOfType<TornadoStartPoint>();
        tornado.transform.position = tornadoStartPoint.transform.position;

        switchingLevel = false;
    }

    public void LevelFinished()
    {
        car.StopPathFollowing();
        CleanUI();

        //load next level
        //if there is no next level -> GameFinished()
        currentLevelCount++;
        if (currentLevelCount >= levels.Length)
        {
            gameFinishedUIObject.SetActive(true);
            return;
        }
        else
        {
            levelFinishedUIObject.SetActive(true);
        }

        StartCoroutine(SwitchLevel(levels[currentLevelCount], 1.5f));
    }

    public void GameOver()
    {
        car.StopPathFollowing();
        CleanUI();
        gameOverUIObject.SetActive(true);

        //laod first level
        currentLevelCount = 0;
        StartCoroutine(SwitchLevel(levels[currentLevelCount], 1.5f));

    }

    public void CleanUI()
    {
        gameOverUIObject.SetActive(false);
        gameFinishedUIObject.SetActive(false);
        levelFinishedUIObject.SetActive(false);
    }

}
