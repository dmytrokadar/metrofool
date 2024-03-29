using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Am I a queue? Am I a linked list? These are legitimate questions.
/// </summary>
public class LinkedListQueue<T> : LinkedList<T>
{
    /// <summary>
    /// Adds item to queue.
    /// </summary>
    public void Enqueue(T item)
    {
        AddLast(item);
    }

    /// <summary>
    /// Removes item from queue.
    /// </summary>
    public T Dequeue()
    {
        T first = First.Value;
        RemoveFirst();
        return first;
    }
}
