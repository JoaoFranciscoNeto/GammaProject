using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceFighter
{
    [RequireComponent (typeof(Rigidbody))]
    public class SpacefighterController : MonoBehaviour
    {
        // Components
        Rigidbody rbody;
        GameObject graphics;

        // Movement
        public float thrust = 20.0f;
        public float turbo_thrust = 40.0f;
        public float brake_thrust = 5.0f;
        public float acceleration = 5.0f;
        public float turn_speed = 15.0f;
        public float roll_speed = 7f;
        public float pitch_yaw_strength = 0.5f;

        // Banking
        public float bank_angle_clamp = 360f;
        public float bank_rotation_speed = 3f;

        // Inputs
        [HideInInspector]
        public float roll_in, yaw_in, pitch_in;
        [HideInInspector]
        public bool turbo_active = false;
        [HideInInspector]
        public bool brake_active = false;

        // State
        public float current_thrust;

        // Use this for initialization
        void Start()
        {
            rbody = GetComponent<Rigidbody>();
            graphics = transform.Find("Graphics").gameObject;
            current_thrust = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {


        }

        public void Move(float roll, float yaw, float pitch, bool turbo, bool brake)
        {
            roll_in = roll;
            yaw_in = yaw;
            pitch_in = pitch;
            turbo_active = turbo;
            brake_active = brake;

            pitch_in *= pitch_yaw_strength;
            yaw_in *= pitch_yaw_strength;
            roll_in *= roll_speed;
            

            if (turbo_active)
            {
                current_thrust = Mathf.Lerp(current_thrust, turbo_thrust, acceleration * Time.deltaTime);
            } else if (brake_active)
            {
                current_thrust = Mathf.Lerp(current_thrust, brake_thrust, acceleration * Time.deltaTime);
            } else
            {
                current_thrust = Mathf.Lerp(current_thrust, thrust, acceleration * Time.deltaTime);
            }

            rbody.AddRelativeTorque(
                    (pitch * turn_speed * Time.deltaTime),
                    (yaw * turn_speed * Time.deltaTime),
                    (roll * turn_speed * Time.deltaTime)
                );

            rbody.AddRelativeForce(0,0,current_thrust);

            rbody.velocity = Vector3.ClampMagnitude(rbody.velocity, 50);

            UpdateBanking();
        }

        private void UpdateBanking()
        {
            Quaternion rotation = graphics.transform.rotation;
            Vector3 eulerRot = rotation.eulerAngles;

            eulerRot.z += Mathf.Clamp((-yaw_in * turn_speed * Time.deltaTime), -bank_angle_clamp, bank_angle_clamp);
            rotation.eulerAngles = eulerRot;

            graphics.transform.rotation = Quaternion.Slerp(graphics.transform.rotation, rotation, bank_rotation_speed * Time.deltaTime);
        }
    }
}