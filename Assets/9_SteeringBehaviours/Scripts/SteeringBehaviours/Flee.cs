using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

namespace SteeringBehaviours
{

    public class Flee : SteeringBehaviour
    {
        public Transform target;
        public float StoppingDistance = 1f;


        public override Vector3 GetForce()
        {
            // SET force to vector3.zero
            Vector3 force = Vector3.zero;
            // IF target == null
            if (target == null)
            {
                // return force
                return force;
            }

            // LET desiredForce = target position - transform position
            Vector3 desiredForce = transform.position - target.position;
            // -(target.position - transform.position) = - target.position + transform.position
            // IF desiredForce magnitude > stoppingDistance
            if (desiredForce.magnitude > StoppingDistance)
            {
                // desiredForce = desiredForce normalized x weighting
                desiredForce = desiredForce.normalized * weighting;
                // force = desiredForce - owner.velocity
                force = desiredForce - owner.velocity;
            }
            #region GizmosGL
            GizmosGL.color = Color.green;
            GizmosGL.AddLine(transform.position, transform.position + desiredForce, 0.1f, 0.1f);
            #endregion

            // Return force
            return force;
        }
    }
}
