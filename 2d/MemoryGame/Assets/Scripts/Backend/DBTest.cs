using UnityEngine;
using System.Collections;

public class DBTest : MonoBehaviour {
    DBHandler db;

	// Use this for initialization
	void Start () {
        //db = new DBHandler();
        db = new DBHandler();
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void TestDB() {
       db.addSessionWWW(10, 20f, true);
    }
}
