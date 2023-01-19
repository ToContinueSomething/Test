using System;
using System.Collections;
using Sources.Infrastructure;
using TMPro;
using UnityEngine.Networking;

namespace Sources.WebInit
{
    public class InternetConnector
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private TMP_Text _text;

        public event Action Completed;

        public InternetConnector(ICoroutineRunner coroutineRunner,TMP_Text text)
        {
            _text = text;
            _coroutineRunner = coroutineRunner;
        }

        public void Enable(string url) => 
            _coroutineRunner.StartCoroutine(Connect(url));

        private IEnumerator Connect(string url)
        {
            bool isCheck = true;

            while (isCheck)
            {
                UnityWebRequest request = UnityWebRequest.Get(url);

                yield return request.SendWebRequest();

                if (IsError(request) == false)
                {
                    isCheck = false;
                    Completed?.Invoke();
                }

                _text.text = "There is no internet connection...";
            }
        }

        private static bool IsError(UnityWebRequest request) =>
            request.result == UnityWebRequest.Result.ConnectionError;
    }
}