﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inheritance
{

    public class Splody : Enemy
    {
        [Header("Splody")]
        public float explosionRadius = 5f;
        public float knockback = 20f;


        // Polymorphism!
        protected override void Attack()
        {
            // Play an animation
            // perfom explosion physics!
            Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (var hit in hits)
            {

                Health h = hit.gameObject.GetComponent<Health>();
                if (h != null)
                {
                    h.TakeDamage(damage);
                }
                Rigidbody r = hit.gameObject.GetComponent<Rigidbody>();
                if(r != null)
                {
                    // Add explosion force
                    rigid.AddExplosionForce(knockback, transform.position, explosionRadius, 0, ForceMode.Impulse);
                }
            }
                     
        }
        protected override void OnAttackEnd()
        {
            // GET REKKKKTTT
            Destroy(gameObject);
        }
    }
}
