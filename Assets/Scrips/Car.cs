using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform tr;
    public Transform guide;
    
    public GameSettings gameSettings;
    public CarPath path;
    public GameDirector gameDirector;

    public float guideDistance = 1f;

    private int checkpointCount = 0;

    private void Start()
    {
        guide.position = tr.position;
        guide.LookAt(path.checkpoints[checkpointCount].transform);
        guide.position += guide.forward * guideDistance;
    }

    private void Update()
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

        guide.position += guide.forward * (guideDistance - Vector3.Distance(guide.position, tr.position));

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
