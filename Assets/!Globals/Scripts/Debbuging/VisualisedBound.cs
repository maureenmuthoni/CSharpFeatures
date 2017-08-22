using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Renderer))]

public class VisualisedBound : MonoBehaviour
{
  private Renderer rend;

    void onDrawGizmos()
    {
        rend = GetComponent<Renderer>();
        Gizmos.DrawWireCube(transform.position, rend.bounds.size);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
