using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectDetector<T> : MonoBehaviour where T : MonoBehaviour {
    
    [Header("Auto Detection")]
    [SerializeField] private bool autoDetect = true;
    
    /// <summary>
    /// Entities close to the entity
    /// </summary>
    public List<T> detectedObjects;
    
    /// <summary>
    /// Only entities with this hit tag will be added to detected entities
    /// </summary>
    public string[] tagsToDetect;

    protected HashSet<string> _tagsToDetect = new HashSet<string>();

    private float _manualDetectionDistance = 5f;
    
    private Collider _detectionCollider;

    private void Awake() {
        detectedObjects = new List<T>();
        _tagsToDetect = tagsToDetect.ToHashSet();
        // Start manual detection if set to manually detect
        if (!autoDetect) {
            foreach (Collider c in GetComponents<Collider>()) {
                c.enabled = false;
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other) {
        // Ignore if collided tag isn't registered
        if (!_tagsToDetect.Contains(other.tag)) {
            return;
        }
        if (other.TryGetComponent(out T detectedObject) && TryAddToDetected(detectedObject)) {
            OnObjectDetected(detectedObject);
        }
    }

    protected virtual void OnTriggerExit(Collider other) {
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

    public abstract void OnObjectDetected(T detectedObject);

    public abstract void OnObjectLost(T detectedObject);
    
    public T GetClosestObject() {
        // Order list by closest. WARNING : May be expensive
        T[] orderedObjects = detectedObjects.OrderBy(obj => Vector3.Distance(obj.transform.position, transform.position)).ToArray();
        return orderedObjects.Any() ? orderedObjects.First() : null;
    }
}
