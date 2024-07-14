using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Shootable {

    [SerializeField] private GameObject[] lootWhenShot;

    public void InstantiateLoot() {
        foreach (GameObject lootPrefab in lootWhenShot) {
            GameObject instance = Instantiate(lootPrefab.gameObject, transform.position, Quaternion.identity);
            instance.SetActive(true);
        }
    }
}
