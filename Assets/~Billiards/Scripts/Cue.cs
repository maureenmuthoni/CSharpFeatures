using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Billiards
{

    public class Cue : MonoBehaviour
    {
        public Ball targetBall; // Target ball selected(which is generally the cue ball
        public float minPower = 0f; // the min power which maps to the distance
        public float maxPower = 20f; // The max power which maps to the distance
        public float maxDistance = 5f; // the maximum distance in units the cue can be dragged back

        private float hitPower; // The final calculated hit poewr to fire the ball
        private Vector3 aimDirection; // The aim direction the ball should fire
        private Vector3 prevMousePos; //The mouse position obtained when left-clicking
        private Ray mouseRay; // The ray of the mouse

        // Help visualize the mouse ray direction of fire
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(mouseRay.origin, mouseRay.origin + mouseRay.direction * 1000f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(targetBall.transform.position, targetBall.transform.position + aimDirection * hitPower);

        }

        //Update is called once per frame
        void Update()
        {
            // Check if left mouse button is pressed
            if (Input.GetMouseButtonDown(0))
            {
                // Store the click position as the 'prevMousePos'
                prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            // Check if left mouse button is pressed
            if (Input.GetMouseButton(0))
            {
                // Perform drag mechanic
                Drag();
            }
            else
            {
                // Perform aim mechanic
                Aim();
            }
            // Check if left mouse button is up
            if (Input.GetMouseButtonUp(0))
            {
                // Hit the ball
               Fire();
            }
        }

        // Rotates the cue  to whererver  the mouse is pointing(using raycast)
        // Update is called once per frame
        void Aim()
        {
            // calculate mouse ray before perfoming raycast
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Raycast Hit container for the hit information
            RaycastHit hit;
            // perform the Raycast
            if (Physics.Raycast(mouseRay, out hit))
            {
                // Obtain direction from the cue's position to the raycast's hit point
                Vector3 dir = transform.position - hit.point;
                // convert direction to angle in degrees
                float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                //  Rotate towards that angle
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                // Position cue to the ball's position;
                transform.position = targetBall.transform.position;
            }

        }

        void Deactivate()
        // Deactivates the Cue
        {
            Aim();
            gameObject.SetActive(false);
        }
        void Activate()
            // Activates the cue
        {
            Aim();
            gameObject.SetActive(true);
        }
        void Drag()
        {
            Vector3 targetPos = targetBall.transform.position;
            Vector3 currMousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Calculate the distance from previous mouse position to current mouse position
            float distance = Vector3.Distance(prevMousePos, currMousepos);
            // Clamp the distance between 0 - maxDistance
            distance = Mathf.Clamp(distance, 0, maxDistance);
            // calculate a percentage for the distance
            float distPercentage = distance / maxDistance;
            // Use percentage of distance to map to the minPower - maxPower values
            hitPower = Mathf.Lerp(minPower, maxPower, distPercentage);
            // Position the cue back using distance
            transform.position = targetPos - transform.forward * distance;
            // Get direction to target ball 
            aimDirection = (targetPos - transform.position).normalized;
        
        }
        void Fire()
        {
            // Hit the ball with direction and power
          targetBall.Hit(aimDirection, hitPower);
            // Deactivate the cue when done
            Deactivate();
        }
  }

        }
     
    

    
		
	

