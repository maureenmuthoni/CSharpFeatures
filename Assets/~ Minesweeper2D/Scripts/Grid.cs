using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper2D
{
    public class Grid : MonoBehaviour
    {
        public GameObject tilePrefab;
        public int width = 10;
        public int height = 10;
        public float spacing = .155f;
        private Tile[,] tiles;

        // Functionality for spawning tiles
        Tile SpawnTile(Vector3 pos)
        {
            // Clone tile prefab
            GameObject clone = Instantiate(tilePrefab);
            clone.transform.position = pos; // position tile
            Tile currentTile = clone.GetComponent<Tile>(); // Get Tile Component
            return currentTile;  //Return current tile
        }
        // Spawns tiles in a grid-like partten
        void GenerateTiles()
        {
            // Create new 2D array f size widthx height
            tiles = new Tile[width, height];

            // Loop through the entire tile list
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Store halfSize for later use
                    Vector2 halfSize = new Vector2(width / 2, height / 2);
                    // Pivot tiles around Grid
                    Vector2 pos = new Vector2(x - halfSize.x, y - halfSize.y);
                    // Apply spacing
                    pos *= spacing;
                    // Spawn the tile
                    Tile tile = SpawnTile(pos);
                    // Attach newley spawned tile to
                    tile.transform.SetParent(transform);
                    // Store it's array coordinates within itself for future reference
                    tile.x = x;
                    tile.y = y;
                    // Store tile in array at those coordinates
                    tiles[x, y] = tile;
                }

            }
        }

        // Use this for initialization
        void Start()
        {
            GenerateTiles();
        }

        // Count adjacent mines at element
        public int GetAdjacentMineCountAt(Tile t)
        {
            int count = 0;
            // Loop through all elements and have each axs go between -1 to 1
            for (int x = -1; x <= 1; x++)
            {
                // calculate desired coordinates from one attained
                int desiredX = t.x + x;
                // iF desiredx is within range of tiles array length
                //Inrement count  by 1
            }
            return count;
        }
    }
}
        
