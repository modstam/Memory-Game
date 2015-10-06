using UnityEngine;
using System.Collections;
using System;

public class NodeWatcher : MonoBehaviour, INodeListener {
    int id;
    bool shown;
    bool clickable;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setNode(Node node)
    {
        id = node.id;
        shown = node.shown;
        clickable = node.enabled;
    }

    public void onNodeChanged(Node node)
    {
        if(shown != node.shown)
        {
            shown = node.shown;
            show(shown);
        }
        
        if(clickable != node.enabled)
        {
            clickable = node.enabled;
            enable(clickable);
        }
        
    }

    void show(bool show)
    {
        Debug.Log("Show: " + show);
    }

    void enable(bool clickable)
    {
        gameObject.SetActive(clickable);
    }




}
