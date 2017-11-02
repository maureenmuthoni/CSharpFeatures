using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace platformer2D
{

    [RequireComponent(typeof(Rigidbody2D))]

    public class Controller2D : MonoBehaviour
    {
        public float accelerate = 20f;
        public float jumpHeight = 10f;
        public float rayDistance = 1f;
        public LayerMask hitLayers;
        public bool isGrounded = false;

        private Rigidbody2D rigid2D;

        // Use this for initialization
        void Start()
        {
            rigid2D = GetComponent<Rigidbody2D>();
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + -transform.up * rayDistance);
      
        }
        void FixedUpdate()
        {
            //perform the rayscast  and only detect from the ground(ignore)
            RaycastHit2D hit= Physics2D.Raycast(transform.position, -transform.up, rayDistance, hitLayers);
            //Detect if ray hit something
            if(hit.collider != null)
            {
                //Set isGrounded to false when you are grounded
                isGrounded = true;
            }
            else
            {
                //Set isGrounded to false when you are not grounded
                isGrounded = false;
            }
        }
        public void move(float inputH)
        {
            rigid2D.AddForce(transform.right * inputH * accelerate);
        }

       public void Jump()
        {
            rigid2D.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        }
    }
}
