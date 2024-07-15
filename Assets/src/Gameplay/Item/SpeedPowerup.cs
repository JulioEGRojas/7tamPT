using UnityEngine;

public class SpeedPowerup : Pickable {

    [SerializeField] private float speedPercentageIncrease;

    [SerializeField] private float duration;

    /// <summary>
    /// Increases the speed for an amount of time. Will be buggy if used in multiplayer.
    /// </summary>
    public void IncreaseSpeedOfLastPicker() {
        if (lastPicker.TryGetComponent(out EntityController entityController)) {
            entityController.SetSpeedForTime((entityController.Speed / 100f) * (100f + speedPercentageIncrease), duration);
        }
    }
}
