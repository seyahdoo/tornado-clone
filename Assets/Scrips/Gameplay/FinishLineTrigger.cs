using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{

    public GameDirector gameDirector;

    private void OnTriggerEnter(Collider other)
    {
        gameDirector.LevelFinished();
    }

}
