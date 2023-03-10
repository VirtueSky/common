using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Virtuesky
{
    public static class Utility
    {
        public static void Clear(this Transform transform)
        {
            var childs = transform.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                UnityEngine.Object.DestroyImmediate(transform.GetChild(i).gameObject, true);
            }
        }

        public static float GetScreenRatio()
        {
            return (1920f / 1080f) / (Screen.height / (float)Screen.width);
        }

        public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
        {
            Transform t = parent.transform;
            foreach (Transform tr in t)
            {
                if (tr.tag == tag)
                {
                    return tr.GetComponent<T>();
                }
            }

            return null;
        }

        public static int GetNumberInAString(string str)
        {
            try
            {
                var getNumb = Regex.Match(str, @"\d+").Value;
                return Int32.Parse(getNumb);
            }
            catch (Exception e)
            {
                return -1;
            }

            return -1;
        }

        #region GetFile

        public static T GetConfigFromFolder<T>(string path) where T : ScriptableObject
        {
            var fileEntries = Directory.GetFiles(path, ".", SearchOption.AllDirectories);

            foreach (var fileEntry in fileEntries)
                if (fileEntry.EndsWith(".asset"))
                {
                    var item =
                        AssetDatabase.LoadAssetAtPath<T>(fileEntry.Replace("\\", "/"));
                    if (item)
                        return item;
                }

            return null;
        }

        public static List<T> GetConfigsFromFolder<T>(string path) where T : ScriptableObject
        {
            var fileEntries = Directory.GetFiles(path, ".", SearchOption.AllDirectories);
            var result = new List<T>();
            foreach (var fileEntry in fileEntries)
                if (fileEntry.EndsWith(".asset"))
                {
                    var item = AssetDatabase.LoadAssetAtPath<T>(fileEntry.Replace("\\", "/"));

                    if (item)
                        result.Add(item);
                }

            if (result.Count > 0)
                return result;

            return null;
        }

        public static T GetConfigFromResource<T>(string path) where T : ScriptableObject
        {
            T config =
                Resources.Load<T>(path);
            if (config != null)
            {
                return config;
            }

            return null;
        }

        public static T GetPrefabFromFolder<T>(string path) where T : MonoBehaviour
        {
            var fileEntries = Directory.GetFiles(path, ".", SearchOption.AllDirectories);

            foreach (var fileEntry in fileEntries)
                if (fileEntry.EndsWith(".prefab"))
                {
                    var item =
                        AssetDatabase.LoadAssetAtPath<T>(fileEntry.Replace("\\", "/"));
                    if (item)
                        return item;
                }

            return null;
        }

        public static List<T> GetPrefabsFromFolder<T>(string path) where T : MonoBehaviour
        {
            var fileEntries = Directory.GetFiles(path, ".", SearchOption.AllDirectories);
            var result = new List<T>();
            foreach (var fileEntry in fileEntries)
                if (fileEntry.EndsWith(".prefab"))
                {
                    var item = AssetDatabase.LoadAssetAtPath<T>(fileEntry.Replace("\\", "/"));

                    if (item)
                        result.Add(item);
                }

            if (result.Count > 0)
                return result;

            return null;
        }

        #endregion
    }
}