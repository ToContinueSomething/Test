using System;
using System.Collections;
using Sources.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}