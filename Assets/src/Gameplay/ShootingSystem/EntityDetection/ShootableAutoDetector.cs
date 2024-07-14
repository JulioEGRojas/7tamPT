using UnityEngine;

public class ShootableAutoDetector : ObjectAutoDetector<Shootable> {
    public override void OnObjectDetected(Shootable detectedObject) {
        base.OnObjectDetected(detectedObject);
    }

    public override void OnObjectLost(Shootable lostObject) {
        base.OnObjectLost(lostObject);
    }
}
