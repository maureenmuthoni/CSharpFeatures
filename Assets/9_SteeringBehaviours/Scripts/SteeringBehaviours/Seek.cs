using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{

    public class Seek : SteeringBehaviour
    {
        public Transform target;
        public float StoppingDistance = 1f;

      
        public override  Vector3 GetForce()
        {
            // SET force to vector3.zero
            Vector3 force = Vector3.zero;
            // IF target == null
            // return force



            // LET dedsiredForce = target position - transform position
            // IF desiredForce magnitude > stoppingDistance
            
            // desiredForce = desirewdForce normalized x weighting
            // force = desiredForce - owner.velocity

            // Retun force
            return force;
        }
    }
}
