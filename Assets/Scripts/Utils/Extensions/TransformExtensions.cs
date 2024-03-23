using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Utils.Extensions
{
    public static class TransformExtensions
    {
        public static List<Transform> GetChildren(this Transform transform, bool onlyActive = false)
        {
            List<Transform> children = new List<Transform>();
            foreach (Transform child in transform)
            {
                if (onlyActive)
                {
                    if (child.gameObject.activeSelf)
                        children.Add(child);
                }
                else
                    children.Add(child);
            }
            return children;
        }
    }
}