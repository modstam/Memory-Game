using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class DBHandler
{

    private string sessionsColorSyncURL = "https://blazing-inferno-8421.firebaseio.com/sessions/colorSyncedSessions.json";
    private string sessionsNonColorSyncURL = "https://blazing-inferno-8421.firebaseio.com/sessions/NonColorSyncedSessions.json";

/**
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
                    // Debug.LogError("server returned null or malformed response ):");
                }
            if (DEBUG)
            {
                foreach (var callBackObject in jsonObj)
                {
                        // Debug.Log(callBackObject.ToString());
                    }
            }

        });


        return true;
    }

**/
    public void addSessionWWW(int tries, float time, bool colorSynced)
    {

       


        string jsonString = "{\"session\": [{\"time\":\"" + time + "\", \"tries\":\"" + tries + "\"}]}";

        byte[] body = Encoding.UTF8.GetBytes(jsonString);
        Dictionary<string,string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        headers.Add("Connection", "close"); 


        if (colorSynced)
        {
           WWW www = new WWW(sessionsColorSyncURL, body, headers);

        }
        else
        {
           WWW www = new WWW(sessionsNonColorSyncURL, body, headers);

        }
        
               
        
        
        //byteStream.Close();
        //byteStream.Dispose();
        //byteStream = null;
    

    }

    private void testDBConnection()
    {

        addSessionWWW(1337, 1337f, true);
        addSessionWWW(1338, 1338f, false);
    }


    public static byte[] ReadFully( Stream input)
    {
        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }
}