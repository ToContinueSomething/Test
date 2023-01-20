using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.WebInit
{
    public class FirebaseRemoteActivator : MonoBehaviour
    {
        [SerializeField] private Text _errorText;
        
        private Firebase.DependencyStatus _dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
        public string Url => FirebaseRemoteConfig.DefaultInstance.GetValue(FirebaseRemoteKeys.Key).StringValue;

        public event Action Interrupted;
        public event Action Completed;

        public void Enable()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                _dependencyStatus = task.Result;

                if (_dependencyStatus == Firebase.DependencyStatus.Available)
                    InitializeFirebase();
                else
                    ReportError($"Could not resolve all Firebase dependencies: {_dependencyStatus}");
            });
        }

        private void InitializeFirebase()
        {
            Dictionary<string, object> defaults =
                new Dictionary<string, object> {{FirebaseRemoteKeys.Key, ""}};

            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
                .ContinueWithOnMainThread(task =>
                {
                    Debug.Log("RemoteConfig configured and ready!");
                    FetchDataAsync();
                });
        }

        private Task FetchDataAsync()
        {
            Debug.Log("Fetching data...");
            System.Threading.Tasks.Task fetchTask =
                Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
                    TimeSpan.Zero);
            return fetchTask.ContinueWithOnMainThread(FetchComplete);
        }

        private void FetchComplete(Task fetchTask)
        {
            SetStatus(fetchTask);

            var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;

            switch (info.LastFetchStatus)
            {
                case Firebase.RemoteConfig.LastFetchStatus.Success:
                    Activate(info);
                    break;
                case Firebase.RemoteConfig.LastFetchStatus.Failure:
                    DisplayFetchFailureReason(info);
                    break;
                case Firebase.RemoteConfig.LastFetchStatus.Pending:
                    ReportError("Latest Fetch call still pending.");
                    break;
            }
        }

        private void DisplayFetchFailureReason(ConfigInfo info)
        {
            switch (info.LastFetchFailureReason)
            {
                case Firebase.RemoteConfig.FetchFailureReason.Error:
                    ReportError("Fetch failed for unknown reason");
                    break;
                case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                    ReportError($"Fetch throttled until {info.ThrottledEndTime}");
                    break;
            }
        }

        private void Activate(ConfigInfo info)
        {
            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync().ContinueWithOnMainThread(task =>
            {
                Debug.Log($"Remote data loaded and ready (last fetch time {info.FetchTime}).");
                Completed?.Invoke();
            });
        }

        private void SetStatus(Task fetchTask)
        {
            if (fetchTask.IsCanceled)
            {
                ReportError("Fetch canceled");
            }
            else if (fetchTask.IsFaulted)
            {
                ReportError("Fetch encountered an error.");
            }
            else if (fetchTask.IsCompleted)
            {
                Debug.Log("Fetch completed successfully!");
            }
        }

        private void ReportError(string text)
        {
            _errorText.enabled = true;
            _errorText.text = text;
            Interrupted?.Invoke();
            gameObject.SetActive(false);
        }
    }
}