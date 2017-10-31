using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Asteroids
{


    public class Shooting : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public float bulletSpeed = 20f;
        public float  shootRate = 0.2f;
        private float shootTimer = 0f;

        // Use this for initialization
        void Shoot()
            //shoot a bullet
        {
            // Create a new bullet clone
            GameObject clone = Instantiate(bulletPrefab, transform.position, transform.rotation);
            // Grab the Rigidbody2D from the clone
             Rigidbody2D rigid = clone.GetComponent<Rigidbody2D>();
            // Add a force to the bullet (using speed)
            rigid.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        }

        // Update is called once per frame

        void Update()
        {
            //count up shoot timer
            shootTimer += Time.deltaTime;
//If shootTimer > shootRate
if(shootTimer > shootRate)
            {
                // IF space bar is pressed
                if (Input.GetKey(KeyCode.Space))
                {
                    // Shoot a projectile
                    // Reset ShootTimer
                    shootTimer = 0f;
                }

            }

        }
    }

}
