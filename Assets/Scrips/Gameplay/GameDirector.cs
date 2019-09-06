using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{

    public string[] levels;

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

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(LoadLevel(levels[0]));
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(UnloadLevel(levels[0]));
        }



    }

    public void LevelFinished()
    {
        print("Level Finished");
    }

    public void GameOver()
    {
        print("Game Over");
    }


}
