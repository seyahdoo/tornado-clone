using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{

    public string[] levels;

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
    }

    public void UnloadLevel(string levelName)
    {
        SceneManager.UnloadSceneAsync(levelName, UnloadSceneOptions.None);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadLevel(levels[0]);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UnloadLevel(levels[0]);
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
