using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{

    public string[] levels;

    public GameObject levelFinishedUIObject;
    public GameObject gameFinishedUIObject;
    public GameObject gameOverUIObject;

    public Tornado tornado;
    public Car car;

    public bool finished;

    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    public IEnumerator GameLoop()
    {
        AsyncOperation op;

        while (true)
        {
            op = SceneManager.LoadSceneAsync(levels[0], LoadSceneMode.Additive);
            while (!op.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("Load Level Completed");

            car.gameObject.SetActive(true);
            CarPath path = FindObjectOfType<CarPath>();
            car.StartPathFollowing(path);

            tornado.gameObject.SetActive(true);
            TornadoStartPoint tornadoStartPoint = FindObjectOfType<TornadoStartPoint>();
            tornado.transform.position = tornadoStartPoint.transform.position;

            //wait for finish
            finished = false;
            while (!finished)
            {
                yield return new WaitForEndOfFrame();
            }

            car.StopPathFollowing();
            CleanUI();
            levelFinishedUIObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            CleanUI();

            car.gameObject.SetActive(false);
            tornado.gameObject.SetActive(false);

            op = SceneManager.UnloadSceneAsync(levels[0], UnloadSceneOptions.None);

            while (!op.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            Debug.Log("Unload Level Completed");
        }
        

        //foreach level
        //load Level
        //wait for finish or gameover

        //on game over, restart game
        //on level finish contunie
    }


    public IEnumerator LoadLevel(string levelName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        while (!op.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Load Level Completed");

    }

    public IEnumerator UnloadLevel(string levelName)
    {
        AsyncOperation op = SceneManager.UnloadSceneAsync(levelName, UnloadSceneOptions.None);

        while (!op.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Unload Level Completed");
    }

    public void LevelFinished()
    {
        finished = true;
    }

    public void GameOver()
    {
        CleanUI();
        gameOverUIObject.SetActive(true);
    }

    public void GameFinished()
    {
        CleanUI();
        gameFinishedUIObject.SetActive(true);
    }

    public void CleanUI()
    {
        gameOverUIObject.SetActive(false);
        gameFinishedUIObject.SetActive(false);
        levelFinishedUIObject.SetActive(false);
    }


}
