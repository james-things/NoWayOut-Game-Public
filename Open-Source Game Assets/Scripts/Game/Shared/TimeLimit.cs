using UnityEngine;

// Inspired by https://forum.unity.com/threads/how-to-set-time-limit.71532/
// however substantially altered and made robust for replaying of levels

namespace NoWayOut.Scripts.Game
{
    public class TimeLimit : MonoBehaviour
    {
        [SerializeField] private float initTimeLimit;
        
        public float timeLimit { get; private set; }
        public float timeLeft { get; private set; }
        public string timeLeftString { get; private set; }
        public bool overTimeLimit = false;
        
        private float _timeTaken = 0;

        void Start()
        {
            timeLimit = initTimeLimit;
            _timeTaken = 0;
            overTimeLimit = false;
        }

        void Update() 
		{
            // update time counters
            _timeTaken = Time.timeSinceLevelLoad;
            timeLeft = timeLimit - _timeTaken;

            // Format the times for display
            timeLeftString = FormatTime((int)timeLeft);
            
            // Check if time limit has been exeeded
            CheckExceedLimit(_timeTaken);
        }
        
        // check if time limit has been reached
        void CheckExceedLimit(float timeTaken)
        {
            if (timeTaken >= timeLimit) overTimeLimit = true;
        }

        // format time
        string FormatTime(int time)
        {
            int minutes = time / 60;
            int seconds = time % 60;
            string timeText = string.Format("{0:00} {1:00}", minutes, seconds);
            
            return timeText;
        }
    }
}