using UnityEngine;
using System.Collections;
using System.Net;

public class DBHandler : MonoBehaviour
{

    private string sessionsColorSyncURL = "https://blazing-inferno-8421.firebaseio.com/sessions/colorSyncedSessions.json";
    private string sessionsNonColorSyncURL = "https://blazing-inferno-8421.firebaseio.com/sessions/NonColorSyncedSessions.json";

    public bool DEBUG = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (DEBUG)
        {
            //Test DB once
            testDBConnection();
            DEBUG = false;
        }

    }

    public bool addSession(int tries, float time, bool colorSynced)
    {
        Hashtable session = new Hashtable();
        session.Add("time", time);
        session.Add("tries", tries);
        HttpWebRequest request;
        string sessionsURL;

        //Switch table depending on if the game had synced colors or not
        if (colorSynced)
        {
            request = (HttpWebRequest)WebRequest.Create(sessionsColorSyncURL);
            sessionsURL = sessionsColorSyncURL;
        }
        else
        {
            request = (HttpWebRequest)WebRequest.Create(sessionsNonColorSyncURL);
            sessionsURL = sessionsNonColorSyncURL;
        }

        request.Method = "POST";
        request.KeepAlive = false;


        HTTP.Request theRequest = new HTTP.Request("post", sessionsURL, session);
        theRequest.Send((callback) =>
        {

            Hashtable jsonObj = (Hashtable)JSON.JsonDecode(callback.response.Text);
            if (jsonObj == null)
            {
                Debug.LogError("server returned null or malformed response ):");
            }
            if (DEBUG)
            {
                foreach (var callBackObject in jsonObj)
                {
                    Debug.Log(callBackObject.ToString());
                }
            }

        });
        return true;
    }

    private void testDBConnection()
    {

        addSession(1337, 1337f, true);
        addSession(1338, 1338f, false);
    }
}