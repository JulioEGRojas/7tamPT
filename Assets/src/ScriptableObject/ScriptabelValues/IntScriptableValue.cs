using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Int", menuName = "Values/Int")]
public class IntScriptableValue : ScriptableValue<int> {
    public void IncreaseByNumber(int increaseValue) {
        SetValue(value+increaseValue);
    }
}

public class IntValueChangedEventArgs : ScriptableValueChangedEvent<int> {
    public IntValueChangedEventArgs(int oldValue, int newValue) : base(oldValue, newValue) {
    }
}