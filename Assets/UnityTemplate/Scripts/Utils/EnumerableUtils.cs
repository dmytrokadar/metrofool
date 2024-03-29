using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// A collection of extension methods for better random handling
/// </summary>
public static class EnumerableUtils
{
    /// <summary>
    /// Wrapper for the Contains method of IEnumerables.
    /// </summary>
    [Pure]
    public static bool In<T>(this T element, IEnumerable<T> elements)
    {
        return elements.Contains(element);
    }
    
    /// <summary>
    /// Choose a random element from a given list. Equal probabilities.
    /// </summary>
    [Pure]
    public static T ChooseRandom<T>(this IEnumerable<T> collection)
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        List<T> list = collection.ToList();

        if (!list.Any())
            throw new InvalidOperationException("The collection is empty.");

        int index = UnityEngine.Random.Range(0, list.Count);
        return list[index];
    }
    
    
        
    /// <summary>
    /// Selects one item from the dictionary based on weighted probability, with the float value as weight.
    /// </summary>
    /// <param name="candidates">A Dictionary of elements and their weight.</param>
    /// <param name="zeroChoice">If true, a Dictionary consisting of only one or more zero values will be
    /// treated as a valid one. A random element with equal weights will be selected.</param>
    /// <typeparam name="T">Typically TileData but can be anything else</typeparam>
    /// <exception cref="InvalidOperationException">Thrown if no option was compatible</exception>
    /// <returns>A randomly chosen element. The higher its weight, the higher the chance.</returns>
    [Pure]
    public static T WeightedRandomChoice<T>(this Dictionary<T, float> candidates, bool zeroChoice = false)
    {
        if (candidates.Count == 0)
            throw new InvalidOperationException("No element is compatible");

        float sum = 0;
        
        // toList because otherwise collection edited within foreach loop
        foreach ((T tile, float weight) in candidates.ToList())
        {
            if (weight is < 0 or float.NaN)
            {
                candidates[tile] = 0;
                continue;
            }

            if (float.IsPositiveInfinity(weight))
            {
                return tile;
            }

            sum += candidates[tile];
        }

        if (sum == 0 && zeroChoice)
        {
            return candidates.Keys.ToList().ChooseRandom();
        }
        
        float randomPoint = UnityEngine.Random.Range(0f, 1f) * sum;
        
        float i = 0;
        foreach ((var tile, float weight) in candidates)
        {
            if (weight == 0)
            {
                continue;
            }
        
            i += weight;

            if (i >= randomPoint)
            {
                return tile;
            }
        }
    
        // only gets here if foreach is empty
        throw new InvalidOperationException("Nothing can be chosen");
    }
    
    /// <summary>
    /// Extens the WeightedRandomChoice method from this class to two IEnumerables.
    /// </summary>
    [Pure]
    public static T WeightedRandomChoice<T>(this IEnumerable<T> choices, IEnumerable<float> chances, bool zeroChoice = false)
    {
        Dictionary<T, float> candidates = choices.ZipToDictionary(chances);
        return candidates.WeightedRandomChoice(zeroChoice);
    }

    /// <summary>
    /// Zip two enumerables into one dictionary
    /// </summary>
    private static Dictionary<T1, T2> ZipToDictionary<T1, T2>(this IEnumerable<T1> choices, IEnumerable<T2> chances)
    {
        return choices.Zip(chances, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
    }


    /// <summary>
    /// Remove all elements for which a predicate is satisfied
    /// </summary>
    /// <param name="collection">The filtered collection</param>
    /// <param name="predicate">A boolean function, that returns true for each value to be removed from the collection</param>
    /// <typeparam name="T">Any type</typeparam>
    public static void RemoveAll<T>(this ICollection<T> collection, Func<T, bool> predicate)
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        // If the collection directly implements ICollection<T>, remove elements directly
        List<T> elementsToRemove = collection.Where(predicate).ToList();
        foreach (var element in elementsToRemove)
        {
            collection.Remove(element);
        }
    }

    /// <summary>
    /// Retrieves value by key from dictionary. If it doesn't exist, a default value is saved into the dictionary.
    /// </summary>
    /// <param name="dict">The dictionary</param>
    /// <param name="key">The key which to access</param>
    /// <param name="defaultVal">Which value to write if the key is not present</param>
    /// <typeparam name="T1">Key type</typeparam>
    /// <typeparam name="T2">Value type</typeparam>
    /// <returns>Value if key is present, else returns the default value</returns>
    [Pure]
    public static T2 GetValue<T1, T2>(this Dictionary<T1, T2> dict, T1 key, T2 defaultVal)
    {
        if (dict.TryGetValue(key, out T2 x))
        {
            return x;
        }

        dict[key] = defaultVal;
        return defaultVal;


    }
    
    /// <summary>
    /// Verifies that for each two objects, if they are equal in one field/anything, they are equal in another as well.
    /// </summary>
    [Pure]
    public static bool VerifyIDUniqueness<TSource, TR1, TR2>(this IEnumerable<TSource> objects, 
        Func<TSource, TR1> selector1, Func<TSource, TR2> selector2)
    {
        var idByName = new Dictionary<TR1, TR2>();

        foreach (TSource obj in objects)
        {
            if (idByName.TryGetValue(selector1(obj), out TR2 id))
            {
                if (EqualityComparer<TR2>.Default.Equals(id, selector2(obj)))
                {
                    return false;
                }
            }
            else
            {
                idByName.Add(selector1(obj), selector2(obj));
            }
        }

        return true;
    }
    
    
    /// <summary>
    /// Rotate array "clockwise".  Element 0 will be 1, 1 -> 2, etc.
    /// </summary>
    /// <param name="array">The array to rotate.</param>
    /// <param name="amount">How many elements to rotate the array by.</param>
    public static void RotateArray<T>(this T[] array, int amount = 1)
    {
        if (array.Length == 0)
            return;

        // positive rotation rotates clockwise
        // -z [0] -> -x [1]
        // -x [1] -> +z [2]
        // +z [2] -> +x [3]
        // +x [3] -> -z [0]
            
        // arr[n] -> arr[(n+1) % len]
            
        amount.Mod(array.Length);

        for (int i = 0; i < amount; i++)
        {
            int length = array.Length;
            T movedVal = array[length - 1];
            
            Array.Copy(array, 0, 
                array, 1, 
                length - 1);

            array[0] = movedVal;
        }
    }
    
    
        /// <summary>
        /// Element-wise multiplication of two dictionaries.
        /// If the dictionarie's keys are not equal, their intersection will be used,
        /// e.g. only keys that are in both will have their values multiplied.
        /// Symmetrical.
        /// </summary>
        [Pure]
        public static Dictionary<T, float> MultiplyWeights<T>(this Dictionary<T, float> d1, Dictionary<T, float> d2)
        {
            return d1.Keys.Intersect(d2.Keys).ToDictionary(key => key, key => d1[key] * d2[key]);
        }
        
        /// <summary>
        /// Finds the highest value that is common for both dictionaries.
        /// </summary>
        /// <example>
        /// d1: {      b: 2, c: 3} <br />
        /// d2: {a: 4, b: 2, c: 1} <br />
        /// Value a is not in both; c is in both, but its minimum value is 1; b is thus the "winner".
        /// Returns 2.
        /// </example>
        /// <param name="d1">A dictionary of float values</param>
        /// <param name="d2">A dictionary of float values</param>
        /// <returns>The largest key value that is present in both dictionaries. 0 if keys do not overlap.</returns>
        [Pure]
        public static float MaximumCommonFactor<T>(this Dictionary<T, float> d1, Dictionary<T, float> d2)
        {
            IEnumerable<T> commonKeys = d1.Keys.Intersect(d2.Keys);
            IEnumerable<float> commonValues = commonKeys.Select((T key) => (Mathf.Min(d1[key], d2[key])));

            return commonValues.DefaultIfEmpty(0f).Max();
        }

        /// <summary>
        /// Returns the key for which the dictionary has the largest value.
        /// </summary>
        /// <param name="dict">A non-empty dictionary</param>
        [Pure]
        public static T MaxKey<T>(this Dictionary<T, float> dict)
        {
            return dict.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        }

        /// <summary>
        /// Removes all instances of a value from a dictionary. Modifies the original.
        /// </summary>
        /// <param name="dict">A dictionary</param>
        /// <param name="val">A value</param>
        /// <returns>The dictionary</returns>
        public static Dictionary<T1, T2> RemoveValue<T1, T2>(this Dictionary<T1, T2> dict, T2 val)
        {
            foreach(var (key, _) in dict.Where(kvp => Equals(kvp.Value, val)).ToList())
            {
                dict.Remove(key);
            }

            return dict;
        }

        /// <summary>
        /// Like .Where() but returns an actual dictionary
        /// Like how is this not a thing??
        /// </summary>
        [Pure]
        public static Dictionary<T1, T2> Where<T1, T2>(this Dictionary<T1, T2> source,
            Func<KeyValuePair<T1, T2>, bool> predicate)
        {
            return Enumerable.Where(source, predicate).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        /// <summary>
        /// Basically a default parameterless version of ToDictionary()
        /// </summary>
        [Pure]
        public static Dictionary<T1, T2> ToDictionary<T1, T2>(this Dictionary<T1, T2> dictionary)
        {
            return dictionary.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        /// <summary>
        /// The cooler ToString
        /// </summary>
        public static string ToString2<T>(this IEnumerable<T> ienumerable)
        {
            return ienumerable.Aggregate("", (current, element) => current + (element.ToString() + "\n"));
        }

}