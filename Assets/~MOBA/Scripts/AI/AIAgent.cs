using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

namespace MOBA
{
    public class AIAgent : MonoBehaviour
    {

        public float maxSpeed = 10f;
        public float maxDistance = 5f;
        public bool updatePosition = true;
        public bool updateRotation = true;

        [HideInInspector]
        public Vector3 velocity;

        private Vector3 force;
        private List<SteeringBehaviour> behaviours;
        private NavMeshAgent nav;

        //Initialization
        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            behaviours = new List<SteeringBehaviour>(GetComponents<SteeringBehaviour>());
        }
        // Calculates all forces from attached SteeringBehaviours
        void ComputeForces()
        {
            force = Vector3.zero;
            for (int i = 0; i < behaviours.Count; i++)
            {
                SteeringBehaviour b = behaviours[i];
                // check if behaviour is active and enabled
                if (!b.isActiveAndEnabled)
                {
                    // skip over to next behaviour
                    continue;
                }
                //Apply behaviour's force to our final force
                force += b.GetForce() * b.weighting;
                //check if force has gone over maxSpeed
                if (force.magnitude > maxSpeed)
                {
                    // Set force = force.normalized * maxSpeed
                    force = force.normalized * maxSpeed;
                    // Exit for loop
                    break;
                }

            }
        }

        // Applies the velocity to agent
        void ApplyVelocity()
        {
            //Increase velocity by force
            velocity += force * Time.deltaTime;
            //Update my nav's speed to velocity
            nav.speed = velocity.magnitude;
            //is there a velocity
            if (velocity.magnitude > 0 && nav.updatePosition)
            {
                if (velocity.magnitude > maxSpeed)
                {
                    // cap velocity to maxspeed
                    velocity = velocity.normalized * maxSpeed;
                }
                // predict the next position
                Vector3 pos = transform.position + velocity;
                //perfom NavMesh sampling
                NavMeshHit navHit;
                if (NavMesh.SamplePosition(pos, out navHit, maxDistance, -1))
                {
                    // Set nav destination to nav hit position
                    nav.SetDestination(navHit.position);
                }
            }

        }

        // Update
        void Update()
        {
            nav.updatePosition = updatePosition;
            nav.updateRotation = updateRotation;
            ComputeForces();
            ApplyVelocity();
        }
    }
}