using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectAutoDetector<T> : MonoBehaviour where T : MonoBehaviour {
    
    [Header("Auto Detection")]
    /// <summary>
    /// Entities close to the entity
    /// </summary>
    public List<T> detectedObjects;

    public bool isEmpty => detectedObjects.Count == 0;
    
    /// <summary>
    /// Only entities with this hit tag will be added to detected entities
    /// </summary>
    public string[] tagsToDetect;

    protected HashSet<string> _tagsToDetect = new HashSet<string>();

    private float _manualDetectionDistance = 5f;

    public EventHandler<T> onObjectDetected;
    
    public EventHandler<T> onObjectLost;

    private void Awake() {
        detectedObjects = new List<T>();
        _tagsToDetect = tagsToDetect.ToHashSet();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        // Ignore if collided tag isn't registered
        if (!_tagsToDetect.Contains(other.tag)) {
            return;
        }
        if (other.TryGetComponent(out T detectedObject) && TryAddToDetected(detectedObject)) {
            OnObjectDetected(detectedObject);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if (other.TryGetComponent(out T detectedObject) && TryRemoveFromDetected(detectedObject)) {
            OnObjectLost(detectedObject);
        }
    }

    public bool TryAddToDetected(T item) {
        if (!detectedObjects.Contains(item)) {
            detectedObjects.Add(item);
            return true;
        }
        return false;
    }
    
    public bool TryRemoveFromDetected(T item) {
        if (detectedObjects.Contains(item)) {
            detectedObjects.Remove(item);
            return true;
        }
        return false;
    }

    public virtual void OnObjectDetected(T detectedObject) {
        onObjectDetected?.Invoke(this, detectedObject);
    }

    public virtual void OnObjectLost(T lostObject){
        onObjectLost?.Invoke(this, lostObject);
    }
    
    public T GetClosestObject() {
        if (isEmpty) {
            return null;
        }
        // Order list by closest. WARNING : May be expensive
        T[] orderedObjects = detectedObjects.OrderBy(obj => Vector3.Distance(obj.transform.position, transform.position)).ToArray();
        return orderedObjects.Any() ? orderedObjects.First() : null;
    }
}
