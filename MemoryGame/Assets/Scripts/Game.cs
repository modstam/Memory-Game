using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    Memory memory;
    public static int NUM_PAIRS = 10;

	// Use this for initialization
	void Start () {
        memory = new Memory(NUM_PAIRS);
        //testMemoryInit();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool clickNode(int row, int col)
    {
        if(memory.isNodeEnabled(row, col))
        {

            return true;
        } else
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
