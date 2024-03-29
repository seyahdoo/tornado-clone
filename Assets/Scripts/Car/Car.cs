﻿using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform tr;
    public Transform guide;
    
    public GameSettings gameSettings;
    public CarPath path;
    public GameDirector gameDirector;
    public SmoothFollow cameraSmoothFollow;

    private bool followingPath = false;
    private int checkpointCount = 0;

    public void SetNewPath(CarPath path)
    {
        this.path = path;
        checkpointCount = 1;
        tr.position = path.checkpoints[0].transform.position;
        tr.LookAt(path.checkpoints[checkpointCount].transform);
        guide.position = tr.position;
        guide.LookAt(path.checkpoints[checkpointCount].transform);
        guide.position += guide.forward * gameSettings.carGuideDistance;

        cameraSmoothFollow.TeleportToDestination();
    }

    public void StartPathFollowing()
    {
        followingPath = true;
    }

    public void StopPathFollowing()
    {
        followingPath = false;
    }

    private void Update()
    {
        if(followingPath)
        {
            Transform target = path.checkpoints[checkpointCount].transform;

            if (Vector3.Distance(target.position, guide.position) < (gameSettings.carSpeed * Time.deltaTime))
            {
                checkpointCount += 1;
                checkpointCount %= path.checkpoints.Length;
            }

            guide.LookAt(target);
            guide.position += guide.forward * gameSettings.carSpeed * Time.deltaTime;

            tr.LookAt(guide);
            tr.position += tr.forward * gameSettings.carSpeed * Time.deltaTime;

            guide.position += guide.forward * (gameSettings.carGuideDistance - Vector3.Distance(guide.position, tr.position));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameDirector.GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        gameDirector.GameOver();
    }

}
