using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public CarPath path;

    public float speed = 1f;
    public float guideDistance = 1f;


    public int checkpointCount = 0;
    public Transform guide;

    public Transform tr;

    private void Start()
    {
        guide.position = tr.position;
        guide.LookAt(path.checkpoints[checkpointCount].transform);
        guide.position += guide.forward * guideDistance;
    }

    private void Update()
    {
        Transform target = path.checkpoints[checkpointCount].transform;

        if (Vector3.Distance(target.position, guide.position) < (speed * Time.deltaTime))
        {
            checkpointCount += 1;
            checkpointCount %= path.checkpoints.Length;
        }

        guide.LookAt(target);
        guide.position += guide.forward * speed * Time.deltaTime;

        tr.LookAt(guide);
        tr.position += tr.forward * speed * Time.deltaTime;

        guide.position += guide.forward * (guideDistance - Vector3.Distance(guide.position, tr.position));

    }




}
