using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper2D
{
    public class Grid : MonoBehaviour
    {
        public enum MineState
        {
            LOSS = 0,
            WIN = 1
        }
        public enum MouseButton
        {
            LEFT_MOUSE = 0,
            RIGHT_MOUSE = 1,
            MIDDLE_MOUSE = 2
        }
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
            // position tile
            clone.transform.position = new Vector3(spacing / 2, spacing / 2, 0) + pos;
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

        // Update is called once per frame
        //void FixedUpdate()
        //{
        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        // LET mouseRay = Camera ScreenPointToRay mousePosition
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        // LET hit = Physics2D RayCast from mouse ray
        //        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        //        // IF  hit collider != null
        //        // Check if something is hit
        //        if (hit.collider != null)
        //        {
        //            // LET hitTile = hit collider's tile component
        //            Tile t = hit.collider.GetComponent<Tile>();
        //            if (t)
        //            {
        //                // LET adjacentMines = GetAdjacentMineCountAt hitTile
        //                int adjacentMines = GetAdjacentMineCountAt(t);
        //                // CALL hitTile.Reveal(adjacentMines)
        //                t.Reveal(adjacentMines);
        //            }
        //        }
        //    }
        //}

        // Count adjacent mines at element
        public int GetAdjacentMineCountAt(Tile t)
        {
            int count = 0;
            // Loop through all elements and have each axes go between -1 to 1
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    // calculate desired coordinates from one attained
                    int desiredX = t.x + x;
                    int desiredY = t.y + y;

                    // iF desiredx is within range of tiles array length
                    if (desiredX < height && desiredX >= 0)
                    {
                        if (desiredY < height && desiredY >= 0)
                        {
                            // IF the element at index isMine
                            Tile tile = tiles[desiredX, desiredY];
                            if(tile.isMine)
                            {
                                //Increment count  by 1
                                count++;
                            }
                        }
                    }
                }
            }
            return count;
        }

        public void FFuncover(int x, int y, bool[,] visited)
        {
            // IF  x >=0 AND y >=0 AND x < width AND y < height
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                // IF visited[x, y]
                if (visited[x, y])
                {
                    // RETURN
                    return;
                }
                // let tile = tiles[x, y]
                Tile tile = tiles[x, y];
                // LET adjacentMines = GetAdjacentMineCountAt(tile)
                int adjacentMines = GetAdjacentMineCountAt(tile);
                // CALL tile.Reveal(adjacentMines)
                tile.Reveal(adjacentMines);

                // IF adjacentMines > 0
                if (adjacentMines > 0)
                {
                    // RETURN
                    return;
                }

                // SET visited[x, y] = true
                visited[x, y] = true;

                // CALL FFuncover(x - 1, y, visited)
                FFuncover(x - 1, y, visited);
                // CALL FFuncover(x + 1, y, visited)
                FFuncover(x + 1, y, visited);
                // CALL FFuncover(x, y - 1, visited)
                FFuncover(x, y - 1, visited);
                // CALL FFuncover(x, y + 1, visited)
                FFuncover(x, y + 1, visited);
            }          
        }

        // Uncovers all mines that are in the grid
        public void UncoverMines(int mineState) 
        {
            // FOR x = 0 to x < width
            for (int x = 0; x < width; x++)
            {
                // FOR y = 0 to y < height
                for (int y = 0; y < height; y++)
                {
                    // LET currentTile = tiles[x, y]
                    Tile currentTile = tiles[x, y];
                    // IF currentTile.isMine
                    if (currentTile.isMine)
                    {
                        // let adjacentMines = GetAdjacentMineCountAT(currentTile)
                        int adjacentMines = GetAdjacentMineCountAt(currentTile);
                        // CALL currentTile.Reveal(adjacentMines, mineState)
                        currentTile.Reveal(adjacentMines, mineState);
                    }
                }
            }
        }

        // Detects if there are no more empty tiles in the game
        bool NoMoreEmptyTiles()
        {
            // LET emptyTileCount = 0
            int emptyTileCount = 0;
            // FOR x = 0 to x < width
            for (int x = 0; x < width; x++)
            {
                // FOR y = 0 to y < height
                for (int y = 0; y < height; y++)
                {
                    // LET currentTile = tile = tiles[x, y]
                    Tile currentTile = tiles[x, y];
                    // IF !currentTile.isRevealed AND !currentTile.isMine
                    if (!currentTile.isRevealed && !currentTile.isMine)
                    {
                        // SET emptyTileCount = emptyTileCount + 1
                        emptyTileCount = emptyTileCount + 1;
                    }
                }
            }
            // RETURN emptyTileCount == 0;
            return emptyTileCount == 0;
        }

        // Takes in a little selected by the user in some way to reveal it
        public void SelectTile(Tile selectedTile)
        {
            // LET adjacentMines = GetAdjacentMineCountAt(selectedTile)
            int adjacentMines = GetAdjacentMineCountAt(selectedTile);
            // CALL selectedTile.Reveal(adjacentMines)
            selectedTile.Reveal(adjacentMines);
            // IF selectedTile isMine
            if (selectedTile.isMine)
            {
                // CALL UncoverMines(0)
                UncoverMines(0);
                // [EXTRA] perfom Game over logic
            }

            // ELSEIF adjacentMines == 0
            else if (adjacentMines == 0)
            {
                // LET x= selectedTile.x
                int x = selectedTile.x;
                // LET y = selectedTile.y
                int y = selectedTile.y;
                // CALL FFuncover(x, y, new bool[width, height])
                FFuncover(x, y, new bool[width, height]);
                // IF NoMoreEmptyTiles()
                if (NoMoreEmptyTiles())
                {
                    // CALL UncoverMines(1)
                    UncoverMines(1);
                    // [EXTRA] perfom Win logic
                }
            }
        }

        void Update()
        {
            // IF Mouse Button  is Down
            if (Input.GetMouseButtonDown((int)MouseButton.LEFT_MOUSE))
            {
                // LET ray = Ray from Camera using Input.mousePosition
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // LET hit = Physics2D RayCast (ray.origin, ray.direction)
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                // IF hit's collider != null
                if (hit.collider != null)
                {
                    // LET hitTile = hit collider's Tile component
                    Tile hitTile = hit.collider.GetComponent<Tile>();
                    // IF hitTile != null
                    if (hitTile != null)
                    {
                        // Call selectTile(hitTile)
                        SelectTile(hitTile);
                    }
                }
            }

            // Use flag on GetMouseButtonDown((int)MouseButton.RIGHT_MOUSE)
            if (Input.GetMouseButtonDown((int)MouseButton.RIGHT_MOUSE))
            {
                // LET ray = Ray from Camera using Input.mousePosition
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // LET hit = Physics2D RayCast (ray.origin, ray.direction)
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                // IF hit's collider != null
                if (hit.collider != null)
                {
                    // LET hitTile = hit collider's Tile component
                    Tile hitTile = hit.collider.GetComponent<Tile>();
                    // IF hitTile != null
                    if (hitTile != null)
                    {
                        if (!hitTile.isRevealed)
                        {
                            hitTile.ToggleFlag();
                        }
                    }
                }
            }
        }
    }
}