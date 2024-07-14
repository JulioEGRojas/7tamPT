using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
   
    [Header("Coin UI")]
    [SerializeField] private IntScriptableValue coinsValue;

    [SerializeField] private TextMeshProUGUI coinsText;

    private void Awake() {
        coinsValue.OnValueChangedEvent += UpdateCoinText;
    }

    private void OnDestroy() {
        coinsValue.OnValueChangedEvent -= UpdateCoinText;
    }

    private void UpdateCoinText(object sender, ScriptableValueChangedEvent<int> e) {
        Debug.Log("WUt");
        coinsText.text = "" + e.newValue;
    }
}
