using UnityEngine;
using System.Collections;
using System;

public class Card : MonoBehaviour, INodeListener {
    int id;
    bool shown;
    bool clickable;

    bool normalMode = true;

    int row;
    int col;

    SpriteRenderer myRenderer;
    MemoryGame memoryGame;

    Color colorTint;
    public Sprite normalSprite;
    public Sprite backSprite;

    void Awake()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

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
        //shown = true; //DEBUG
        clickable = node.enabled;
    }

    public void setMemoryGame(MemoryGame game)
    {
        Debug.Log("Setting memory game: " + game);
        memoryGame = game;
        normalMode = true;
    }

    public void setMemoryGame(MemoryGame game, Color colorTint)
    {
        
        memoryGame = game;
        normalMode = false;
        this.colorTint = colorTint;
        Debug.Log("Setting memory game: " + game + ",c: " + this.colorTint.r);
    }

    public void onNodeChanged(Node node)
    {
        Debug.Log("Node has changed!");
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
        if (show)
        {
            myRenderer.sprite = normalSprite;
            if(!normalMode)
                myRenderer.color = colorTint;
        }
        else
        {
            myRenderer.sprite = backSprite;
            if (!normalMode)
                myRenderer.color = Color.white; //remove color tint
        }
            

    }

    void enable(bool clickable)
    {
        //gameObject.SetActive(clickable);
        
        if (!clickable)
        {
            Debug.Log("Disabling");
            myRenderer.color = new Color(0.60f, 0.60f, 0.60f);
        }
    }

    public void setSpriteNormal(Sprite sprite)
    {
        normalSprite = sprite;
        if (shown)
            myRenderer.sprite = normalSprite;
    }

    public void setSpriteBack(Sprite sprite)
    {
        backSprite = sprite;
        if(!shown)
            myRenderer.sprite = backSprite;
    }

    public void setPos(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    //On click
    void OnMouseUp()
    {
        if(clickable)
            memoryGame.clickNode(row, col);
    }


}
