using System;
using System.IO;
using UnityEngine;

// Script to copy log file to clipboard and redirect user to our survey
namespace NoWayOut.Scripts.UI
{
   public class Hyperlink : MonoBehaviour
   {
      private string _logFilePath;
      private string _logContents;

      private void Start()
      {
         _logContents = "";
      }

      public void OpenSurveyLink()
      {
         _logFilePath = Application.dataPath + "/EventLog.txt";

         // try to copy log to the clipboard automatically, dump any error to debug log
         try
         {
            _logContents = File.ReadAllText(_logFilePath);
            GUIUtility.systemCopyBuffer = _logContents;
         }
         catch (Exception e)
         {
            Debug.Log(e.ToString());
         }
         
         Application.OpenURL("https://forms.gle/eJTYMoS7GWQcQ3e77");
      }
   }
}
