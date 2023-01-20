using System;
using UnityEngine;

namespace Sources.Data
{
    public class ScoreData
    {
        private const string BestValuePrefs = "Score";
        
        public int Value { get; private set; }
        public int BestValue { get; private set; }
        
        public event Action<int> ValueChanged;
        public event Action<int> BestValueChanged;

        public ScoreData()
        {
            BestValue = PlayerPrefs.GetInt(BestValuePrefs, 0);
            Value = 0;
        }

        public void Add(int value)
        {
            if (value < 0)
                throw new ArgumentException(nameof(value));

            Value += value;
            ValueChanged?.Invoke(Value);
        }
        
        public void Reset()
        {
            Value = 0;
            ValueChanged?.Invoke(Value);
        }
        
        public void Save()
        {
            if (Value > BestValue)
            {
                PlayerPrefs.SetInt(BestValuePrefs, Value);
                BestValue = Value;
                BestValueChanged?.Invoke(BestValue);
            }
        }
    }
}