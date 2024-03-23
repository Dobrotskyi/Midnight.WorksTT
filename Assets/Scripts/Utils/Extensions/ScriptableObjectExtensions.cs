using UnityEditor;
using UnityEngine;

namespace Scripts.Utils.Extensions
{
    public static class ScriptableObjectExtensions<T> where T : ScriptableObject
    {
        public static T[] GetAllInstances()
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            T[] a = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
            }

            return a;
        }
    }
}