using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : Pickable {

    [SerializeField] private float speedPercentageIncrease;

    [SerializeField] private float duration;

    public void IncreaseSpeedOfLastPicker() {
        if (lastPicker.TryGetComponent(out EntityController entityController)) {
            entityController.SetSpeedForTime((entityController.Speed / 100f) * (100f + speedPercentageIncrease), duration);
        }
    }
}
