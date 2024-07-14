using System;
using UnityEngine;
using UnityEngine.Events;

public class Pickable : MonoBehaviour {

    public EventHandler<Picker> onPickedUp;
    public UnityEvent onPickEvent;

    /// <summary>
    /// Last entity that picked up this object
    /// </summary>
    protected Picker lastPicker;

    private void Awake() {
        onPickedUp += OnPick;
    }

    private void OnDestroy() {
        onPickedUp -= OnPick;
    }

    public void Pickup(Picker picker) {
        lastPicker = picker;
        onPickedUp?.Invoke(this, picker);
    }

    private void OnPick(object sender, Picker picker) {
        onPickEvent.Invoke();
    }
}
