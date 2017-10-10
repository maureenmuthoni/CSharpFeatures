using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clean Code: CTRL+K+D
namespace Variables
{

    public class Player : MonoBehaviour
    {
        public float movementSpeed = +20f;
        public float rotationSpeed = 2f;
        public float deceleration = 0.1f;

        private Rigidbody rigid;

        // use this for initialization
        void Start()
        {
            // Get component from GameObject
            rigid = GetComponent<Rigidbody>();
        }



        // Update is called one per frame
        void Update()
        {
            // Call Movement()
            Movement();
            Rotation();
        }
        void Movement()
        {
            float inputV = Input.GetAxis("Vertical");
            rigid.AddForce(transform.right * inputV *movementSpeed);
            rigid.velocity += -rigid.velocity * deceleration * Time.deltaTime;
        }

        
        void Rotation()
        {
            float inputH=Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.back * inputH * rotationSpeed * Time.deltaTime);
        }

    }
}

