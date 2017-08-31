﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace TowerDefence
{

    public class AIAgent : MonoBehaviour
    {
        public Transform target;
        private NavMeshAgent nav;

        // Use this for initialization
        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            //IF target != null
            if (target != null)
            {
                // set destinction to target's position
                nav.SetDestination(target.position);
            }

        }
    }
}