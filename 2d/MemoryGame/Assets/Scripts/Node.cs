using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {
    public int id; //The 'value' of the card
    // A pair of nodes have the same id.
    public bool enabled; //disable found pairs for example
    public bool shown; //show cards when clicked for example

    List<INodeListener> listeners;

    public Node(int id)
    {
        this.id = id;
        enabled = true;
        shown = false;
        listeners = new List<INodeListener>();
    }

    public void setEnabled(bool enabled)
    {
        this.enabled = enabled;
        notifyListeners();
    }

    public void setShown(bool shown)
    {
        this.shown = shown;
        notifyListeners();
    }

    public override string ToString()
    {
        return "" + id;
    }

    public void Listen(INodeListener listener)
    {
        listener.setNode(this);
        listeners.Add(listener);
    }

    private void notifyListeners()
    {
        Debug.Log("Notifying all " + listeners.Count + " number of listeners...");
        for (int i = 0; i < listeners.Count; ++i)
        {
            listeners[i].onNodeChanged(this);
        }
    }

}
