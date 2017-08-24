using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Breakout
{
    public class Paddle : MonoBehaviour
    {
        public float movementSpeed = 20f;
        public Ball currentBall;
        public Vector3[] directions = new Vector3[]
        {
            new Vector2(.5f, .5f), // index0
            new Vector2(-.5f, .5f)
        };
        // Use this for initialization
        void Start()
        {
            // Grabs currentBall fom the children of the paddle
            currentBall = GetComponentInChildren<Ball>();
        }
        void Fire()
        {
            // Detach children (Ball)
            currentBall.transform.SetParent(null); //... i am  batman
            //Generate random dir from list of directions , // Randomly select a direction
            Vector3 randomDir = directions[Random.Range(0, directions.Length)];
            // Fire off ball in randomDirection
            currentBall.Fire(randomDir);
        }
        void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
        }

        void Movement()
        {
            // Get input on the horizontal axis
            float inputH = Input.GetAxis("Horizontal");
            //Set force to direction(inputH to decide on which direction)
            Vector3 force = transform.right * inputH;
            // Apply movement speed to force
            force *= movementSpeed;
            // Apply deltaTime to force
            force *= Time.deltaTime;
            // Add force to position
            transform.position += force;
        }

        // Update is called once per frame
        void Update()
        {
            CheckInput();
            Movement();
        }
    }
}
