using UnityEngine;

public class ShootableAutoDetector : ObjectAutoDetector<Shootable> {
    public override void OnObjectDetected(Shootable detectedObject) {
        base.OnObjectDetected(detectedObject);
        // If shootable is 'invisible', don't add it as target
        if (detectedObject.invisible) {
            TryRemoveFromDetected(detectedObject);
        }
    }

    public override void OnObjectLost(Shootable lostObject) {
        base.OnObjectLost(lostObject);
    }
}
