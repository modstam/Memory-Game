using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemoryGame : MonoBehaviour
{
    Memory memory;
    public IntPair selectedCoords;
    int NUM_PAIRS;
    public float colWidth = 1.5f;
    public float rowHeight = 2.25f;
    public float cardHeight = 1.5f;
    public float cardWidth = 1f;
    public List<Sprite> normalSprites;
    public Transform cardPrefab;
    public Color alternateColorTint;
    Card[][] cards;
    Game game;

    int clicks;
    int remainingPairs;

    public MemoryGame(int numPairs)
    {
        
    }

    public void StartGame(Game game, bool normalMode)
    {
        this.game = game;
        clicks = 0;
        NUM_PAIRS = normalSprites.Count;
        memory = new Memory(NUM_PAIRS);
        selectedCoords = new IntPair();
        remainingPairs = NUM_PAIRS;
        int rows = memory.getRows();
        int cols = memory.getCols();
        Vector2 startPos = new Vector2((-colWidth * (cols)) / 2, (rowHeight * (rows)) / 2); //Where to place the first card
        Debug.Log("CARDHEIGHT: " + cardHeight);
        startPos.y -= ((rowHeight-cardHeight) / 2);
        startPos.x += ((colWidth - cardWidth) / 2);
        Debug.Log("Startpos: " + startPos);
        //testMemoryInit();

        cards = new Card[rows][];
        //Initialize card matrix
        for (int i = 0; i < rows; ++i)
        {
            cards[i] = new Card[cols];
            for (int j = 0; j < cols; ++j)
            {
                Debug.Log("COLWIDTH: " + colWidth);
                Transform newCard = (Transform)Instantiate(cardPrefab,
                    new Vector2(startPos.x + (j * colWidth), startPos.y - (i * rowHeight)),
                    transform.rotation);
                newCard.parent = this.transform;

                cards[i][j] = newCard.GetComponent<Card>();
                memory.nodeListen(i, j, cards[i][j]);
                cards[i][j].setPos(i, j);
                cards[i][j].setSpriteNormal(normalSprites[memory.getId(i,j)]);

                if(normalMode)
                    cards[i][j].setMemoryGame(this);
                else
                    cards[i][j].setMemoryGame(this, alternateColorTint);
            }
        }
    }

    public bool clickNode(int row, int col)
    {
        Debug.Log("Clicked node: (" + row + ", " + col + "), Previous selection: (" + selectedCoords.a + ", " + selectedCoords.b + ").");
        if (memory.isNodeEnabled(row, col))
        {
            Debug.Log("Enabled.");
            clicks++;
            if (selectedCoords.a == -1) //if we didn't have a previous selection
            {
                Debug.Log("New selection");
                selectedCoords = new IntPair(row, col); //set the previous selection
                memory.showNode(row, col);
            }
            else if (selectedCoords.equals(row, col)) // if we clicked our selected node
            {
                Debug.Log("Same selection");
                memory.hideNode(row, col);
                selectedCoords.a = -1; //Deselect nodes
                selectedCoords.b = -1;
            }
            else //if we clicked a new node
            {
                Debug.Log("Paircheckingtime");
                memory.showNode(row, col);
                bool foundPair = memory.isPair(selectedCoords, row, col); //check if we found a pair
                if(!foundPair)
                {
                    memory.hideNode(selectedCoords.a, selectedCoords.b);
                    selectedCoords.a = row; //Select the new node
                    selectedCoords.b = col;
                } else
                {
                    Debug.Log("Found a pair!");
                    remainingPairs--;
                    memory.disableNode(selectedCoords.a, selectedCoords.b);
                    memory.disableNode(row, col);
                    selectedCoords.a = -1; //Deselect nodes
                    selectedCoords.b = -1;

                    if(remainingPairs <= 0)
                        game.GameOver(clicks); // GAME OVER
                }
            }
            
            


            return true;
        }
        else
        {
            return false;
        }
    }

    public void testMemoryInit()
    {
        for (int numPairs = 1; numPairs <= 30; ++numPairs)
        {
            memory = new Memory(numPairs);
            Debug.Log("Num Pairs: " + numPairs + ", " + memory);
        }
    }
}
