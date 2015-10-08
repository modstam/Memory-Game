using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;

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
        WWW www;

        Hashtable session = new Hashtable();
        session.Add("time", time);
        session.Add("tries", tries);

        Stream byteStream = new MemoryStream(Encoding.UTF8.GetBytes(JSON.JsonEncode(session)));
        byte[] array = ReadFully(byteStream);

        if (colorSynced)
        {
            www = new WWW(sessionsColorSyncURL, array);
        }
        else
        {
            www = new WWW(sessionsNonColorSyncURL, array);
        }
        www.Dispose();
        www = null;
        byteStream.Close();
        byteStream.Dispose();
        byteStream = null;

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