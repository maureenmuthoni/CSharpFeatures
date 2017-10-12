using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI
{
    [RequireComponent(typeof(AI_Agent))]

    public class SteeringBehaviour : MonoBehaviour
    {
        public float weighting = 7.5f;
        public Vector3 force;
        public AI_Agent owner;

        protected virtual void Awake()
        {
            owner = GetComponent<AI_Agent>();
        }

        public  virtual Vector3 GetForce()
        {
            return Vector3.zero;
        }
    }
}
