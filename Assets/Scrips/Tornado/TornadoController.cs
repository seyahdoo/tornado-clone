using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoController : MonoBehaviour
{

    public Transform tr;
    public Transform cam;

    public Vector2 oldMousePositon;
    public Vector2 touchStartMousePosition;
    public Vector2 mouseDelta;

    public float tornadoControllSpeed = 1f;
    public float tornadoControllXSpeed = 1f;
    public float tornadoControllYSpeed = 1f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartMousePosition = Input.mousePosition;
            oldMousePositon = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePosition = Input.mousePosition;
            mouseDelta = currentMousePosition - oldMousePositon;
            oldMousePositon = currentMousePosition;
        }

        Vector3 posChangeForThisFrame =
            cam.forward.normalized * (mouseDelta.y / Screen.height) * tornadoControllYSpeed
            + cam.right.normalized * (mouseDelta.x / Screen.width ) * tornadoControllXSpeed;

        posChangeForThisFrame.y = 0f;

        tr.position += posChangeForThisFrame * tornadoControllSpeed * Time.deltaTime;

    }

}
