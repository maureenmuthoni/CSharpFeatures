using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{


    public class Charger : Enemy
    {
        [Header("Charger")]
        public float impactForce = 10f;
        public float knockback = 5f;

        private Rigidbody rigid;

        protected override void Awake()
        {
            base.Awake();
        }
        protected override void Attack()
        {
            // Add force to self
            rigid.AddForce(transform.position
        }


        void OnCollisionEnter(Collision col)
        {
            // if collision hits player
            if (col.gameObject != null)
            {
                // Add impactForce to player
            }


        }

     }
}
