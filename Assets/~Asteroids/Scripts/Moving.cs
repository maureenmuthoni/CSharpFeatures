using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{


    public class Moving : MonoBehaviour
    {
        public float accceleration = 50f;
        public float rotationspeed = 360f;

        private Rigidbody2D rigid;

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
        }
        void Acceleerate()
        {
            float InputV = Input.GetAxis("Vertical");
        }
        // Rotates the player(in degrees)
        void Rotate()
        {
            float InputH = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.back, rotationspeed * InputH * Time.deltaTime);
        }
        // Update is called once per frame
        void Update()
        {
            Acceleerate();
            Rotate();

        }
    }
}
