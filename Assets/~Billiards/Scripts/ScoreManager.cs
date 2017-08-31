using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int count;

	// Use this for initialization
	void Start ()
    {
        count = 0;
	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision other)
    {
        // if collided with the Ball
        if(other.gameObject.tag == "Ball")
        {
            // Add to 1 score
            count += count;
        }
    }
		
	}
