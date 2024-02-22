using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class LogFilter : MonoBehaviour
{
    void Awake()
    {
        // Subscribe to the LogMessageReceived event
        Application.logMessageReceived += HandleLog;

        // ... Your other initialization code ...
    }

    void HandleLog(string logText, string stackTrace, LogType type)
    {
        // Check if the log is from the Adaptive Performance module
        if (logText.Contains("[Adaptive Performance]"))
        {
            // Suppress the log from the Adaptive Performance module
            return;
        }

        // Handle other logs as needed
        Debug.unityLogger.Log(type, logText);
    }

    void OnDestroy()
    {
        // Unsubscribe from the LogMessageReceived event when the script is destroyed
        Application.logMessageReceived -= HandleLog;
    }
}

