using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Minesweeper2D
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        // store x and y coordinate in array for later use
        public int x = 0;
        public int y = 0;
        public bool isMine = false; // Is the current tile a mine
        public bool isRevealed = false; // has the tile already been revealed?
        [Header("References")]
        public Sprite[] emptySprites; // list of empty sprites ie, empty, 1, 2, 3, 4, etc...
        public Sprite[] mineSprites; // The mineSprites
        public Sprite defaultSprite;
        public Sprite flagSprite;

        private SpriteRenderer rend; // Reference to sprite renderer

        private bool isFlagged = false;

        void Awake()
        {
            //Grab reference to sprite renderer
            rend = GetComponent<SpriteRenderer>();
        }

        void Start() {
            // Randomly decide if it's a mine or not
            isMine = Random.value < .05f;
        }
        public void Reveal(int adjacentMines, int mineState = 0)
        {
            // Flags the tile as being revealed
            isRevealed = true;
            // checks if the tile is a mine
            if (isMine)
            {
                // Sets sprite to mine sprte
                rend.sprite = mineSprites[mineState];
            }
            else
            {
                // Sets sprite to appropriate texture based on adjacent mines
                rend.sprite = emptySprites[adjacentMines];
            }
        }
        // Update is called once per frame
        void Update() {

        }
        public void ToggleFlag()
        {
            isFlagged = !isFlagged;
            if (isFlagged)
            {
                // SET rend.sprite to flagSprite
                rend.sprite = flagSprite;
            }
            else
            {
                // SET rend.sprite to defaultSprite
                rend.sprite = defaultSprite;
            }
        }
    }
}
