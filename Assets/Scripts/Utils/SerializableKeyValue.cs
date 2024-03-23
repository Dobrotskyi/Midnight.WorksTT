using System;
using UnityEngine;

namespace Scripts.Utils
{
    [Serializable]
    public struct SerializableKeyValue<K, V>
    {
        public SerializableKeyValue(K key, V value)
        {
            _key = key;
            _value = value;
        }

        [SerializeField] private K _key;
        [SerializeField] private V _value;

        public K Key => _key;
        public V Value => _value;
    }
}