using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI; // For artificial intelligence
using GGL; // For Drawing

namespace MOBA
{
    public class PathFollowing : SteeringBehaviour
    {
        public Transform target; // Get to the target
        public float nodeRadius = 1f; // How big each node is for the agent to seek  to
        public float targetRadius = 3f; // seperate from the nodes that the agents follows

        private int currentNode = 0; // keep track of the individual nodes
        private bool isAtTarget = false; // Has the agent reached the target node?
        private NavMeshAgent nav; // Reference to the agent component
        private NavMeshPath path; //Stores the calculated path in this variable

        private void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            path = new NavMeshPath();
        }

        Vector3 Seek(Vector3 target)
        {
            Vector3 force = Vector3.zero;

            // Get distance to target
            Vector3 desiredForce = target - transform.position;
            // Calculate distance
            float distance = isAtTarget ? targetRadius : nodeRadius;
            // is the magnitude greater than distance?
            if (desiredForce.magnitude > distance)
            {
                // Apply weighting to force
                desiredForce = desiredForce.normalized * weighting;
                // apply desirewd force to force ( removing current owner's velocity)
                force = desiredForce - owner.velocity;
            }
            // Return the force

            return force;
        }
        void Update()
        {

            // is the path calculated?i
            if (path != null)
            {
                Vector3[] corners = path.corners;
                if(corners.Length > 0)
                {
                    Vector3 targetPos = corners[corners.Length - 1];
                    // Draw the target
                    Gizmos.color = new Color(1, 0, 0, 0.3f);

                    //. calculate distance from agent to target
                    float distance = Vector3.Distance(transform.position, targetPos);
                    // If distance is greater than the radius
                    if (distance >= targetRadius)
                    {
                        Gizmos.color = Color.cyan;
                        // Draw lines between nodes
                        for (int i = 0; i < corners.Length - 1; i++)
                        {
                            Vector3 nodeA = corners[i];
                            Vector3 nodeB = corners[i + 1];
                            GizmosGL.AddLine(nodeA, nodeB, 0.1f, 0.1f);
                            GizmosGL.AddSphere(nodeB, 1f);
                            GizmosGL.color = Color.blue;
                        }
                    }
                }

            }
        }
        public override Vector3 GetForce()
        {
            Vector3 force = Vector3.zero;

            if (!target)
                return force;

            // Calculate the path(target.position, path))
            {
                if (nav.CalculatePath(target.position, path))
                {
                    if (path.status == NavMeshPathStatus.PathComplete)
                    {
                        Vector3[] corners = path.corners;
                        // Is there any corners in the path?
                        if (corners.Length > 0)
                        {
                            int lastIndex = corners.Length - 1;
                            // is currentNode at the end of the list?
                            if (currentNode >= corners.Length)
                            {
                                // Cap currentNode to the end of the array ( target Node)



                                currentNode = lastIndex;
                            }

                            // Get the current corner position
                            Vector3 currentPos = corners[currentNode];
                           
                            // Get distance to current pos
                            float distance = Vector3.Distance(transform.position, currentPos);
                            // Is the distance within the node radius?
                            if (distance <= nodeRadius)
                            {
                                // Move to next node
                                currentNode++;
                            }

                            // Is the agent at the target?
                            float distanceToTarget = Vector3.Distance(transform.position, target.position);
                            isAtTarget = distanceToTarget <= targetRadius;

                            // seek towards current node's position
                            force = Seek(currentPos);

                            // Seek towards current node's position
                            force = Seek(currentPos);

                        }

                    }
                }
                return force;
            }
        }
    }
}
