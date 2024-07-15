using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Equippable : MonoBehaviour {

    [SerializeField] public UnityEvent onEquipped;

    public void OnEquipped() {
        onEquipped.Invoke();
    }
}
