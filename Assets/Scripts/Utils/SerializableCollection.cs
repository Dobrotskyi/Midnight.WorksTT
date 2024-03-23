using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Utils
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver where TValue : struct
    {
        [SerializeField] private List<TKey> _keys = new();
        [SerializeField] private List<TValue> _values = new();

        public void OnBeforeSerialize()
        {
            _keys.Clear();
            _values.Clear();
            foreach (var pair in this)
            {
                _keys.Add(pair.Key);
                _values.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            Clear();
            for (int i = 0; i < _keys.Count; i++)
                Add(_keys[i], _values[i]);

        }
    }
}