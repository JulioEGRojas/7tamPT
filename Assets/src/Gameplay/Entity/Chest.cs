using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Shootable {

    [SerializeField] private GameObject[] lootWhenShot;

    private bool _looted = false;

    public void InstantiateLoot() {
        if (_looted) {
            return;
        }

        _looted = true;
        foreach (GameObject lootPrefab in lootWhenShot) {
            GameObject instance = Instantiate(lootPrefab.gameObject, transform.position, Quaternion.identity);
            instance.SetActive(true);
        }
    }
}
