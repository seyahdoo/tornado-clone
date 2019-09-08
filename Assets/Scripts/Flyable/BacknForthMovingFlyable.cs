using UnityEngine;

public class BacknForthMovingFlyable : Flyable
{

    public Vector3 speed = Vector3.right;
    public float lastSpeedChangeTime = 0f;
    public float speedChangeCooldown = .3f;

    private void Update()
    {
        if (isKinematic)
        {
            tr.position += speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(Time.time - lastSpeedChangeTime > speedChangeCooldown)
        {
            lastSpeedChangeTime = Time.time;
            speed *= -1;
        }
    }

}
