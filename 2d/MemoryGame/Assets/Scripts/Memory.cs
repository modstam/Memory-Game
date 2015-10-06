using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Memory {
    Node[][] nodes;
    int numPairs;

    public Memory(int numPairs)
    {
        init(numPairs);
    }

    public void init(int numPairs)
    {
        this.numPairs = numPairs;
        int numNodes = numPairs * 2;
        int rows = (int) Mathf.Floor(Mathf.Sqrt(numNodes)); // Number of nodes vertically
        int cols = numNodes/rows; //Number of nodes horizontally
        if(rows*cols < numNodes)
        {
            rows--;
            cols = numNodes / rows;
        }
        //Debug.Log("numPairs: " + numPairs + ", w: " + cols + ", h: " + rows);
        this.nodes = new Node[rows][];
        //Initialize node matrix
        for(int i = 0; i < rows; ++i)
        {
            nodes[i] = new Node[cols];
        }

        //Create a list of all numPairs*2 nodes
        List<int> ids = new List<int>();
        for(int i = 0; i < numNodes; ++i) {
            ids.Add(i % numPairs); // Two nodes with the same id
        }

        //Create a matrix with nodes and pairs located at random positions.
        for(int i = 0; i < rows; ++i) {
            for (int j = 0; j < cols; ++j) {
                int rand = Random.Range(0, ids.Count - 1); //Get a random index
                //Debug.Log("i,j: [" + i + ", " + j + "] rand: " + rand + ", ids[rand]: " + ids[rand] + ", nodes[i][j]" + nodes[i][j]);
                nodes[i][j] = new Node(ids[rand]); //Create a node with the random id
                ids.RemoveAt(rand); //Remove that id from the available ids list
            }
        }
        
    }

    public void nodeListen(int row, int col, INodeListener listener)
    {
        nodes[row][col].Listen(listener);
    }

    //public bool isPair(Node node1, Node node2)
    //{
    //    return node1.id.Equals(node2.id);
    //}

    public bool isPair(IntPair node1, IntPair node2)
    {
        return nodes[node1.a][node1.b].id.Equals(nodes[node2.a][node2.b].id);
    }

    public bool isPair(IntPair node1, int row2, int col2)
    {
        return nodes[node1.a][node1.b].id.Equals(nodes[row2][col2].id);
    }

    public bool isNodeEnabled(int row, int col)
    {
        return nodes[row][col].enabled;
    }

    public void showNode(int row, int col)
    {
        nodes[row][col].setShown(true);
    }

    public void hideNode(int row, int col)
    {
        nodes[row][col].setShown(false);
    }

    public void toggleShowNode(int row, int col)
    {
        nodes[row][col].setShown(!nodes[row][col].shown);
    }

    public bool isNodeShown(int row, int col)
    {
        return nodes[row][col].shown;
    }

    public void enableNode(int row, int col)
    {
        nodes[row][col].setEnabled(true);
    }

    public void disableNode(int row, int col)
    {
        nodes[row][col].setEnabled(false);
    }

    public void toggleEnabledNode(int row, int col)
    {
        nodes[row][col].setEnabled(!nodes[row][col].enabled);
    }

    public int getId(int row, int col)
    {
        return nodes[row][col].id;
    }

    public int getRows()
    {
        return nodes.Length;
    }

    public int getCols()
    {
        return nodes[0].Length;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder("Memory matrix: \n");
        for (int i = 0; i < nodes.Length; ++i)
        {
            for (int j = 0; j < nodes[i].Length; ++j)
            {
                sb.Append("[" + nodes[i][j] + "] ");
            }
            sb.Append("\n");
        }

        return sb.ToString();
    }

    public void printMatrix()
    {
        Debug.Log(ToString());
    }

}
