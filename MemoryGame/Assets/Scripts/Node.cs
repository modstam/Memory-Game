using UnityEngine;
using System.Collections;

public class Node {
    public int id; //The 'value' of the card
    // A pair of nodes have the same id.
    public bool enabled; //disable found pairs for example
    public bool shown; //show cards when clicked for example

    public Node(int id)
    {
        this.id = id;
        enabled = true;
        shown = false;
    }

    public void setEnabled(bool enabled)
    {
        this.enabled = enabled;
    }

    public void setShown(bool shown)
    {
        this.shown = shown;
    }

    public override string ToString()
    {
        return "" + id;
    }

    public void Listen(INodeListener listener)
    {
        listener.setNode(this);
    }

}
