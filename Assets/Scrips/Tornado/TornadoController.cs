using UnityEngine;

public class TornadoController : MonoBehaviour
{

    public Transform tr;
    public Transform cam;

    public GameSettings gameSettings;

    private Vector2 oldMousePositon;
    private Vector2 mouseDelta;

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

        Vector3 speed =
            cam.forward.normalized * 
                (((mouseDelta.y / Screen.height) * gameSettings.tornadoControllYSpeed
                    * gameSettings.tornadoControllSpeed)
                    + gameSettings.tornadoStaticForwardSpeed)
            
            + cam.right.normalized * 
                (mouseDelta.x / Screen.width ) * gameSettings.tornadoControllXSpeed 
                    * gameSettings.tornadoControllSpeed;


        speed.y = 0f;

        tr.position += speed * Time.deltaTime;

    }

}
