using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace loopArrays
{
    public class Loops : MonoBehaviour
    {
        public GameObject[] spawnprefabs;
        public float frequency = 5;
        public float amplitude = 6;
        public int spawnAmount = 10;
        public float spawnRadius = 5f;
        public string message = "print This";
        public float printTime = 2f;
        private float timer = 0;

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }

        // Use this for initialization
        void Start()
        {
            /* while(condition)
             {

           //statement
             }
          */
            SpawnObjectsWithSine();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SpawnObjectsWithSine()
        {
            ////loop through until timer gets printTime
            //while (timer <= printTime) // STICK AROUND
            //{
            //    // count Up timer in seconds
            //    timer += Time.deltaTime;
            //    print("PUT THE COOKIE DOWN!");
            //    //}


                // Statement(s)
            

            for (int i = 0; i < spawnAmount; i++)
            {
                // Spawned new Game Object
                int randomIndex = Random.Range(0, spawnprefabs.Length);
                // store randomly selected prefab
                GameObject randomPrefab = spawnprefabs[randomIndex];
                // Instantiate randomly selected prefab
                GameObject clone = Instantiate(randomPrefab);
                // Grab the MeshRenderer
                MeshRenderer rend = clone.GetComponent<MeshRenderer>();
                // change the colour
                float r = Random.Range(0, 2);
                float g = Random.Range(0, 2);
                float b = Random.Range(0, 2);
                float a = 1;
                rend.material.color = new Color(r, g, b, a);
                // Generated Random position within circle(sphere actually)
                float x = Mathf.Sin(i * frequency) * amplitude;
                float y = i;
                float z = Mathf.Cos(i * frequency) * amplitude;
                Vector3 randomPos = transform.position + new Vector3(x, y, z);
                // set spawned object's position
                clone.transform.position = randomPos;

            }
        }

        void SpawnObjects()
        {
            /*
            for (initialization; condition; iteration)
            {
            // Statement(s)
            }
            */

            for (int i = 0; i < 10; i++)
            {
                //GameObject clone = Instantiate(spawnprefab);

            }

        }

    }
}


