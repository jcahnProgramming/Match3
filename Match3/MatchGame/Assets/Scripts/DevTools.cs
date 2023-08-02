using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTools : Singleton<DevTools>
{
    [Header("General Settings")]
    public bool isDevelopmentBuild;
    public bool isShowingDebugLogs;
    public bool isUsingMultiplayer;


    public void Log(int logLevel, string scriptName, string message)
    {
        string msg = scriptName.ToUpper() + "[" + logLevel + "]: " + message;

        Debug.Log(msg);
    }

}
