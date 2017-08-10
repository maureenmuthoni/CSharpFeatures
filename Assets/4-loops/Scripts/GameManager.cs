using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BreakOut
{
    public class GameManager : MonoBehaviour
    {
        public int width = 20;
        public int height = 20;
        public GameObject[] blockPrefabs;

        // Use this for initialization
        void Start()
        {
            GenerateBlocks();

        }

        // Function with arguments
        //<return-type> <function-name> (arguments)
        GameObject GetBlockByIndex(int index)
        {
            // Error handling
            if (index > blockPrefabs.Length || index < 0)
                return null;
            
                //Randomly Spawn a new GameObject
            
            GameObject randomPrefab = blockPrefabs[index];
            GameObject clone = Instantiate(randomPrefab);
            // ... and return it
            return clone; //when you run sth and gain some input.you return what u have creted(what you are returning is a GameObject
        }

        GameObject GetRandomBlock()
        {
            int randomIndex = Random.Range(0, blockPrefabs.Length);
            GameObject randomPrefab = blockPrefabs[randomIndex];
            GameObject clone = Instantiate(randomPrefab);
            return clone;
        }

        void GenerateBlocks()
        {
            // Loop through the width
            for (int x = 0; x < width; x++)
            { //open brace
                for (int y = 0; y < height; y++)
                {
                    GameObject block = GetRandomBlock();
                    // Set the new position
                    Vector3 pos = new Vector3(x, y, 0);
                    block.transform.position = pos;
                }

            }  // Close brace
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
