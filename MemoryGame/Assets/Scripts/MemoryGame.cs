using UnityEngine;
using System.Collections;

public class MemoryGame : MonoBehaviour
{
    Memory memory;
    IntPair selectedCoords;
    public static int NUM_PAIRS = 10;

    int remainingPairs;

    // Use this for initialization
    void Start()
    {
        memory = new Memory(NUM_PAIRS);
        selectedCoords = new IntPair();
        remainingPairs = NUM_PAIRS;
        //testMemoryInit();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool clickNode(int row, int col)
    {
        if (memory.isNodeEnabled(row, col))
        {
            if (selectedCoords.a == -1) //if we didn't have a previous selection
            {
                selectedCoords = new IntPair(row, col); //set the previous selection
                memory.showNode(row, col);
            }
            else if (selectedCoords.containsBoth(row, col)) // if we clicked our selected node
            {
                memory.hideNode(row, col);
                selectedCoords.a = -1; //Deselect nodes
                selectedCoords.b = -1;
            }
            else //if we clicked a new node
            {
                bool foundPair = memory.isPair(selectedCoords, row, col); //check if we found a pair
                if(!foundPair)
                {
                    remainingPairs--;
                    memory.hideNode(selectedCoords.a, selectedCoords.b);
                    memory.hideNode(row, col);
                    selectedCoords.a = -1; //Deselect nodes
                    selectedCoords.b = -1;
                } else
                {
                    memory.disableNode(selectedCoords.a, selectedCoords.b);
                    memory.disableNode(row, col);
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
