using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacknForthMovingFlyable : Flyable
{

    public Vector3 speed = Vector3.right;
    public float lastSpeedChange = 0f;
    public float speedChangeCooldown = .3f;

    private void Update()
    {
        if (isPhysicsDisabled)
        {
            tr.position += speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(Time.time - lastSpeedChange > speedChangeCooldown)
        {
            lastSpeedChange = Time.time;
            speed *= -1;
        }
    }

}
