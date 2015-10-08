using UnityEngine;
using System.Collections;

public class DBTest : MonoBehaviour {
    DBHandler db;

	// Use this for initialization
	void Start () {
        db = new DBHandler();
        db.addSession(10, 20, true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TestDB() {
        
       
    }
}
