using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GGL;
/*
* Obtains user selection on the nav mesh and
* 'directs' all agents to that location using either
* Seek or PathFollowing behaviours
*/
namespace MOBA
{
    [RequireComponent(typeof(Camera))]
    
    public class AIAgentDirector : MonoBehaviour
    {
        public LayerMask hitLayers;
        public float rayDistance = 1000f;
        public AIAgent[] agentsToDirect;

        private Camera cam;
        private Transform selectionPoint;

        // Use this for initialization
        void Start()
        {
            cam = GetComponent<Camera>();
            GameObject g = new GameObject("Target Location");
            selectionPoint = g.transform;

        }

        // Assigns target to all agents in 'agentsToDirect'
        void AssignTargetToAllAgents(Transform target)
        {
            // Loop through all agentsToDirect
            foreach (var agent in agentsToDirect)
            {
                Seek s = agent.GetComponent<Seek>();
                if (s != null)
                    s.target = target;  // Assign target to seek component on agent

                //Path following
                PathFollowing p = agent.GetComponent<PathFollowing>();
                // Is pathFpllowing attatched to agent?
                if (p != null)
                {
                    p.target = target; // Assign target to path following component on agent
                }



            }
        }
        void Update()
        {
            // Is mouse down?
            if (Input.GetMouseButtonDown(0))
            {
                Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                // perfom raycast
                if(Physics.Raycast(camRay, out rayHit, rayDistance, hitLayers))
                {
                    NavMeshHit navHit;
                    // check if raycast point is on nav mesh
                    if(NavMesh.SamplePosition(rayHit.point, out navHit, rayDistance, -1))
                    {
                        // set selection point
                        selectionPoint.position = navHit.position;
                        // Assign target to all agents
                        AssignTargetToAllAgents(selectionPoint);
                    }
                }
                 
            }
        }
    }
}
