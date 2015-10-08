using UnityEngine;
using System.Collections;

public class DBTest : MonoBehaviour {
    DBHandler db;

	// Use this for initialization
	void Start () {
        //db = new DBHandler();
        db = GetComponent<DBHandler>();
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void TestDB() {
        Debug.Log("Sending to db");
        db.addSessionWWW(10, 20f, true);
    }
}
