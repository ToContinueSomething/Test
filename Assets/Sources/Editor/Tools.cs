#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Sources.Editor
{
    public static class Tools
    {
        [MenuItem("Tools/ClearPrefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
#endif