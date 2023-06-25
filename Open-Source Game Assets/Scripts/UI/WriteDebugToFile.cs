using UnityEngine;
using System.IO;

// from https://www.youtube.com/watch?app=desktop&v=A12GhwUxTrY
public class WriteDebugToFile : MonoBehaviour
{
    string filename = "";
    
    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }
    
    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/EventLog.txt";
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        TextWriter tw = new StreamWriter(filename, true);
        
        tw.WriteLine("[" + System.DateTime.Now + "] " + logString);
        
        tw.Close();
    }
}
