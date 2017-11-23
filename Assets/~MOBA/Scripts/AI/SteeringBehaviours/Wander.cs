using UnityEngine;
using System.Collections;

using GGL;

namespace MOBA
{

    public class Wander : SteeringBehaviour
    {
        public float offset = 1.0f;
        public float radius = 1.0f;
        public float jitter = 0.2f;


        private Vector3 targetDir;
        private Vector3 randomDir;

        public override Vector3 GetForce()
        {

            // Force starts at zero (no velocity)
            Vector3 force = Vector3.zero; // to hero


            /*
                       * -32767               0                    32767
                       * |--------------------|--------------------|
                       *            |____________________|
                       *                 Random Range
                       */

            //Randomize range between values
            float randX = Random.Range(0, 0x7fff) - (0x7fff * 0.5f);
            float randZ = Random.Range(0, 0x7fff) - (0x7fff * 0.5f);

            #region Calculate Random Direction
            // create random direction vector
            randomDir = new Vector3(randX, 0, randZ);
            //Normalize the random direction
            randomDir = randomDir.normalized;
            //randomDir.Normalize();
            // Multiply jitter to randomDir
            randomDir *= jitter;

            #endregion

            #region calculate Target Direction
            // Append target dir with randomDir
            targetDir += randomDir;

            // normalize target dir 
            targetDir = targetDir.normalized;
            //targetDir.Normalized();

            // Amplify it by the radius
            targetDir *= radius;
            #endregion
            // Calculate seek position using targetDir;
            Vector3 seekPos = transform.position + targetDir;
            seekPos += transform.forward.normalized * offset;

            #region GizmosGl
            Vector3 forwardPos = transform.position + transform.forward.normalized * offset;

            Circle c = GizmosGL.AddCircle(forwardPos + Vector3.up * 0.1f, radius, Quaternion.LookRotation(Vector3.down));
            c.color = new Color(1, 0, 0, 0.5f);
            c = GizmosGL.AddCircle(seekPos + Vector3.up * 0.15f, radius * 0.6f, Quaternion.LookRotation(Vector3.down));
            c.color = new Color(0, 0, 1, 0.5f);
            #endregion 


            #region Wander
            // calculate direction
            Vector3 direction = seekPos - transform.position;
            //Is direction valid? (not zero)
            if (direction.magnitude > 0)
            {
                // calculate force
                Vector3 desiredForce = direction.normalized * weighting;
                force = desiredForce - owner.velocity;

            }


            #endregion

            // Return the force .... luke


            return force;
        }

        // CTRL + M + 0 = FOLDS CODE
        //ctrl +  M + P = UNFOLDS CODE
        //CTRL + K + C = COMMENT LINE
    }
}