using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    public bool enableLog = true;
    public bool enableLogWarning = true;
    public bool enableLogError = true;

    public void Log(object message, Object sender = null)
    {
        if (enableLog)
        {
            if (sender != null)
                Debug.Log(message, sender);
            else
                Debug.Log(message);
        }
    }

    public void LogWarning(object message, Object sender = null)
    {
        if (enableLogWarning)
        {
            if (sender != null)
                Debug.LogWarning(message, sender);
            else
                Debug.LogWarning(message);
        }
    }

    public void LogError(object message, Object sender = null)
    {
        if (enableLogError)
        {
            if (sender != null)
                Debug.LogError(message, sender);
            else
                Debug.LogError(message);
        }
    }
}
