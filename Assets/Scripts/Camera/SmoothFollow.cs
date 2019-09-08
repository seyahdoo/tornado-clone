using UnityEngine;

public class SmoothFollow : MonoBehaviour
{

    public Transform tr;
    public Transform target;

    public float movementSpeed = .1f;
    public float rotationSpeed = .1f;

    void Update()
    {
        tr.position = Vector3.Lerp(tr.position, target.position, movementSpeed * Time.deltaTime);
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, rotationSpeed * Time.deltaTime);
    }

    public void TeleportToDestination()
    {
        tr.position = target.position;
        tr.rotation = target.rotation;
    }

}
