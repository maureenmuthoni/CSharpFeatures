using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inheritance
{
    public class Splodey : Enemy
    {
        [Header("Splodey")]
        public float splosionRadius = 5f;
        public float splosionRate = 3f;
        public float impactForce = 10f;
        public GameObject explosionParticles;

        private float splosionTimer = 0f;

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            // start splosionTimer
            splosionTimer += Time.deltaTime;
        }

        protected override void OnAttackEnd()
        {
            // IF explosionTimer > splosionRate
            if (splosionTimer > splosionRate)
            {
                //call splode
                Splode();
                Destroy(gameObject);
            }
        }

        void Splode()
        {
            // Perfom  physics OverlapSphere with splosionRadius
            Collider[] hits = Physics.OverlapSphere(transform.position, splosionRadius);
            //Loop through all hits
            foreach (var hit in hits)
            {
                Health h = hit.GetComponent<Health>();
                if (h != null)
                {
                    h.TakeDamage(damage);


                }
                Rigidbody r = hit.GetComponent<Rigidbody>();
                if (r != null)
                {

                    r.AddExplosionForce(impactForce, transform.position, splosionRadius);
                }
            }                                   
        }
    }
}
