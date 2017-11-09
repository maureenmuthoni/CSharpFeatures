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
        public bool UpdatePosition = true;
        public bool updatePosition = true;

        [HideInInspector]
        public Vector3 velocity;

        private Vector3 force;
        private List<SteeringBehaviour> behaviours;
        private NavMesh nav;

        //Initialization
        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            behaviours = new List<SteeringBehaviour>(GetComponents<SteeringBehaviour>());
        }
        // Calculates all forces from attached SteeringBehaviours
        void ComputeForces()
        {

        }

        // Applies the velocity to agent
        void ApplyVelocity()
        {

        }

        // Update
        void Update()
        {

        }
    }
}