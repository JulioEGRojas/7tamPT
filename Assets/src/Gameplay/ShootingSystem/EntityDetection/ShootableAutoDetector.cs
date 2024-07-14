using UnityEngine;

public class ShootableAutoDetector : ObjectAutoDetector<Shootable> {
    public override void OnObjectDetected(Shootable detectedObject) {
        base.OnObjectDetected(detectedObject);
        Debug.Log(detectedObject.name + " detected.");
    }

    public override void OnObjectLost(Shootable lostObject) {
        base.OnObjectLost(lostObject);
        Debug.Log(lostObject.name + " lost.");
    }
}
