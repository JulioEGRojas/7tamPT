using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class MonoBehaviourObjectPool<T> where T: Component{

    /// <summary>
    /// Sample of the object to have in the pool
    /// </summary>
    [SerializeField] public T sample;

    protected Queue<T> availableObjects = new Queue<T>();
    
    protected Queue<T> occupiedObjects = new Queue<T>();

    public void CreateInstances(int instanceNumber) {
        for (int i = 0; i < instanceNumber; i++) {
            availableObjects.Enqueue(CreateNewObject());
        }
    }

    public List<T> GetAvailableObjects() {
        return availableObjects.ToList();
    }

    public T OccupyOne() {
        if (availableObjects.Count <= 0) {
            availableObjects.Enqueue(CreateNewObject());
        }
        T newObject = availableObjects.Dequeue();
        occupiedObjects.Enqueue(newObject);
        return newObject;
    }

    public void ReturnToPool(T elementToDisOccupy) {
        if (occupiedObjects.Peek() == elementToDisOccupy) {
            occupiedObjects.Dequeue();
        }
        availableObjects.Enqueue(elementToDisOccupy);
    }

    public void ReturnAllToPool() {
        while (occupiedObjects.Count>0) {
            availableObjects.Enqueue(occupiedObjects.Dequeue());
        }
    }

    public T CreateNewObject() {
        return GameObject.Instantiate(sample.gameObject).GetComponent<T>();
    }
    
}
