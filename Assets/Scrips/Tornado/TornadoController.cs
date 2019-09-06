﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoController : MonoBehaviour
{

    public Transform tr;
    public Transform cam;

    public Vector2 oldMousePositon;
    public Vector2 touchStartMousePosition;
    public Vector2 mouseDelta;

    public GameSettings gameSettings;

    private void OnEnable()
    {
        oldMousePositon = Input.mousePosition;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            oldMousePositon = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePosition = Input.mousePosition;
            mouseDelta = currentMousePosition - oldMousePositon;
            oldMousePositon = currentMousePosition;
        }

        Vector3 posChangeForThisFrame =
            cam.forward.normalized * (((mouseDelta.y / Screen.height) * gameSettings.tornadoControllYSpeed) + gameSettings.tornadoStaticForwardSpeed)
            + cam.right.normalized * (mouseDelta.x / Screen.width ) * gameSettings.tornadoControllXSpeed;

        posChangeForThisFrame.y = 0f;

        tr.position += posChangeForThisFrame * gameSettings.tornadoControllSpeed * Time.deltaTime;

    }

}
