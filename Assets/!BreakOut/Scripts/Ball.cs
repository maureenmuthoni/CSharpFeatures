﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{

    public class Ball : MonoBehaviour
    {
        public float speed = 5f; //speed at which the ball travels
        private Vector3 velocity; // velocity = direction x speed

        public void Fire(Vector3 direction)
        {
            velocity = direction * speed;
        }
        void OnCollisionEnter2D(Collision2D other)
        {
            //Grab the contact point of collision
            ContactPoint2D contact = other.contacts[0];
            // calculate reflect using velocity and normal
            Vector3 reflect = Vector3.Reflect(velocity, contact.normal);
            //Redirecting the velocity to reflection
            velocity = reflect.normalized * velocity.magnitude;
            if (other.gameObject.tag=="Block")
            {
                Destroy(other.gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // move position based on velocity
            transform.position += velocity * Time.deltaTime;
        }
    }
}
