using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serializable dictionary by christophfranke123
/// https://answers.unity.com/questions/460727/how-to-serialize-dictionary-with-unity-serializati.html
/// Modified to use KVP instead of 2 lists
/// </summary>
[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver where TKey : new()
{
    /// <summary>
    /// Unity serialization trick #8422
    /// </summary>
    [Serializable]
    public class Pair<T1, T2>
    {
        [SerializeField]
        public T1 key;
            
        [SerializeField]
        public T2 value;

        public Pair(KeyValuePair<T1, T2> kvp)
        {
            key = kvp.Key;
            value = kvp.Value;
        }
            
        public Pair(T1 t1, T2 t2)
        {
            key = t1;
            value = t2;
        }

        public KeyValuePair<T1, T2> pair => new KeyValuePair<T1, T2>(key, value);
    }

    // private List<TKey> keys = new List<TKey>();
    // private List<TValue> values = new List<TValue>();

    [SerializeField] [ReadOnly]
    private List<Pair<TKey, TValue>> elements;
     
    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        elements ??= new List<Pair<TKey, TValue>>();
            
        // keys.Clear();
        // values.Clear();
        elements.Clear();
        foreach(KeyValuePair<TKey, TValue> pair in this)
        {
            // keys.Add(pair.Key);
            // values.Add(pair.Value);

            elements.Add(new Pair<TKey, TValue>(pair));
        }
    }

    // load dictionary from lists
    public void OnAfterDeserialize()
    {
            
        Clear();
 
        // if(keys.Count != values.Count)
        //     throw new Exception($"there are {keys.Count} keys and {values.Count} values after deserialization.");

        for (int i = 0; i < elements.Count; i++)
        {
            Pair<TKey, TValue> pair = elements[i];

            if (pair.key == null)
            {
                continue;
            }

            Add(pair.key, pair.value);
        }
    }
}