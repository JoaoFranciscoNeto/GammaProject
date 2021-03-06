﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceFighter
{
    [RequireComponent(typeof(SpacefighterController))]
    public class SpacefighterPlayer : MonoBehaviour
    {
        SpacefighterController controller;


        // Use this for initialization
        void Start()
        {
            controller = GetComponent<SpacefighterController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            float roll = -Input.GetAxis("Roll");
            float yaw = PointerController.instance.positionToCenter.x;
            float pitch = PointerController.instance.positionToCenter.y;

            bool turbo = Input.GetButton("Turbo");
            bool brake = Input.GetButton("Brake");

            controller.Move(roll, yaw, pitch, turbo, brake);
        }
    }
}