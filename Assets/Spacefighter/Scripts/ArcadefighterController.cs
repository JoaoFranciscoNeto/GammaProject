using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadefighterController : MonoBehaviour
{

    // Movement
    public float speed = 20.0f;
    public float turbo_speed = 40.0f;
    public float brake_speed = 5.0f;

    public float airSpeed;
    public float weight = 50f;
    public float acceleration = 10f;
    public float handling = .2f;
    public float roll_speed = 50f;
    public float dragPercent = 1.1f;

    // Graphics
    GameObject graphics;

    float timeToRoll = 3f;
    float timeToRollDec;

    // Use this for initialization
    void Start()
    {
        graphics = transform.Find("Graphics").gameObject;

        timeToRollDec = timeToRoll;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Move(float roll, float yaw, float pitch, bool turbo, bool brake)
    {
        transform.Rotate(
            pitch * Time.deltaTime * handling,
            yaw * Time.deltaTime * handling,
            0
            );

        RollUp();

        UpdateBanking(yaw);

        if (turbo)
            airSpeed = Mathf.Lerp(airSpeed, turbo_speed, acceleration * Time.deltaTime);
        else if (brake)
            airSpeed = Mathf.Lerp(airSpeed, brake_speed, acceleration * Time.deltaTime);
        else
            airSpeed = Mathf.Lerp(airSpeed, speed, acceleration * Time.deltaTime);

        transform.position += transform.forward * Time.deltaTime * airSpeed;

    }

    void UpdateBanking(float yaw)
    {
        Quaternion localRot = graphics.transform.localRotation;
        Vector3 euler = localRot.eulerAngles;

        euler.z = Mathf.Clamp(-yaw * 5, -45, 45);

        localRot.eulerAngles = euler;

        graphics.transform.localRotation = Quaternion.Slerp(graphics.transform.localRotation, localRot, Time.deltaTime * 2);
    }

    bool RollUp()
    {
        float ang = Vector3.SignedAngle(transform.up, Vector3.up, transform.forward);
        transform.Rotate(0, 0, ang * Time.deltaTime);
        return ang == 0;

    }
}
