using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class RequestTime : MonoBehaviour
{
    private string url = "https://worldtimeapi.org/api/timezone/Europe/Moscow";
    // "https://yandex.com/time/sync.json"

    public DateTimeOffset dateTime;

    [Serializable]
    public class Time
    {
        public string unixtime;
        //public string clocks;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GetRequest(Action<DateTimeOffset> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("Http error: " + webRequest.error);
                    break;
            }

            string response = webRequest.downloadHandler.text;
            string time = JsonUtility.FromJson<Time>(response).unixtime;
            long timeDouble = long.Parse(time);
            DateTimeOffset dateTime = DateTimeOffset.FromUnixTimeSeconds(timeDouble).ToLocalTime();

            if (dateTime != null)
            {
                callback(dateTime);
            }
        }
    }

    public void ResponseCallback(DateTimeOffset data)
    {
        dateTime = data;
    }
}
