using System;
using UnityEngine;

public class Picker : MonoBehaviour {
    
    /// <summary>
    /// Tries to pick anything it touches. Could be optimized using layers.
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.TryGetComponent(out Pickable pickable)) {
            Pick(pickable);
        }
    }

    public void Pick(Pickable pickable) {
        pickable.Pickup(this);
    }
}
