using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inheritance
{
    public class Splodey: Enemy
    {
        [Header("Splodey")]
        public float splosionRadius = 5f;
        public float splosionRate = 3f;
        public float impactForce = 10f;
        public GameObject explosionParticles;

        // Use this for initialization
     public  override void Attack() 
        {
            // Start to ignition timer
             // IF explosionTimer > splosionRate
             // call Explode()

        }

        // Update is called once per frame
        void Explode() 
        {
            // Perfom  physics OverlapSphere with splosionRadius
            //Loop through all hits
            // If player
            // Add impact to rigidbody

            // Destroy self
        }
    }
}
