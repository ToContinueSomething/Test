using System;
using UnityEngine;

namespace Sources.Data
{
    [Serializable]
    public class PlayerProgress
    {
       public ScoreData ScoreData { get; set; }
        public string InitialLevel { get; set; }
  

        public PlayerProgress(string initialLevel)
        {
            ScoreData = new ScoreData();
            InitialLevel = initialLevel;
        }
    }
}