using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacknForthMovingFlyable : Flyable
{

    public Vector3 speed = Vector3.right;


    private void Update()
    {
        if (isPhysicsDisabled)
        {
            tr.position += speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed *= -1;
    }

}
