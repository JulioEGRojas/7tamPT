using System;
using UnityEngine;

public class Picker : MonoBehaviour {
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.TryGetComponent(out Pickable pickable)) {
            Pick(pickable);
        }
    }

    public void Pick(Pickable pickable) {
        pickable.Pickup(this);
    }
}
