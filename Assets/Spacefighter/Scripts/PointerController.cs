using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour {

    // Settings
    public bool returns_to_center = false;
    public bool instant_snapping = false;
    public float center_speed = 5f;
    public float deadzone_radius = 0f;
    public bool invert_y = false;
    public float mouse_sensitivity = 15f;

    public Vector2 pointerPosition;
    public Vector2 screenCenter;
    public Vector2 positionToCenter;

    public static PointerController instance = null;

    public Texture pointerTexture;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        pointerPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        
	}

    // Update is called once per frame
    void Update()
    {
        float x_axis = Input.GetAxis("Mouse X");
        float y_axis = Input.GetAxis("Mouse Y");

        if (invert_y)
            y_axis = -y_axis;

        pointerPosition += new Vector2(x_axis * mouse_sensitivity, y_axis * mouse_sensitivity);

        if (returns_to_center && Vector2.Distance(pointerPosition, screenCenter) > deadzone_radius)
        {
            pointerPosition.x = Mathf.Lerp(pointerPosition.x, screenCenter.x, center_speed * Time.deltaTime);
            pointerPosition.y = Mathf.Lerp(pointerPosition.y, screenCenter.y, center_speed * Time.deltaTime);
        }

        positionToCenter = pointerPosition - screenCenter;
        positionToCenter = Vector2.ClampMagnitude(positionToCenter, 500);
    }

    private void OnGUI()
    {
        GUI.DrawTexture(
            new Rect(
                pointerPosition.x - pointerTexture.width / 2,
                Screen.height - pointerPosition.y - pointerTexture.height / 2,
                pointerTexture.width,
                pointerTexture.height),
            pointerTexture);
    }
}
