﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{


    public class Charger :Enemy
    {
        [Header("Charger")]
        public float impactForce = 10f;
        public float knockback = 5f;

        public override void Attack()
        {
            // Add force to self
        }
        void OnCollisionEnter(Collision col)
        {
            // if collision hits player
            // Add impactForce to player
        }



    }
}