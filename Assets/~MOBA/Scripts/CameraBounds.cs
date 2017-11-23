using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MOBA
{

    public class CameraBounds : MonoBehaviour
    {
        public Vector3 size = new Vector3(80f, 0f, 50f);
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, size);
        }

        // Adjusts the position to constrain it within size
        public Vector3 GetAdjustedPos(Vector3 incomingPos)
        {

            Vector3 pos = transform.position;
            Vector3 halfsize = size * 0.5f;

            // Is inomingpos outside the positive Z?
            if (incomingPos.z > pos.z + halfsize.z)
            {
                incomingPos.z = pos.z + halfsize.z;
            }
            // Is inomingpos outside the negative Z??
            if (incomingPos.z < pos.z - halfsize.z)
            {
                incomingPos.z = pos.z - halfsize.z;
            }

            // Is inomingpos outside the positive X?
            if (incomingPos.x > pos.x + halfsize.x)
            {
                incomingPos.x = pos.x + halfsize.x;
            }
            // Is inomingpos outside the negative x?
            if (incomingPos.x < pos.x - halfsize.x)
            {
                incomingPos.x = pos.x - halfsize.x;
            }

            // Is inomingpos outside the positive y?
            if (incomingPos.y > pos.y + halfsize.y)
            {
                incomingPos.y = pos.y + halfsize.y;
            }
            // Is inomingpos outside the negative x?
            if (incomingPos.y < pos.y - halfsize.y)
            {
                incomingPos.y = pos.y - halfsize.y;
            }


            //returns the adjusted incomingPos
            return incomingPos;
        }
    }
}
