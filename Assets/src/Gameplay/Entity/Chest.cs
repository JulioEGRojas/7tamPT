using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Shootable {

    [SerializeField] private GameObject[] lootWhenShot;

    private bool _looted = false;

    /// <summary>
    /// Instantiates all the loot on the lootWhenShot list
    /// </summary>
    public void InstantiateLoot() {
        if (_looted) {
            return;
        }

        _looted = true;
        foreach (GameObject lootPrefab in lootWhenShot) {
            GameObject instance = Instantiate(lootPrefab.gameObject, transform.position, Quaternion.identity);
            instance.SetActive(true);
            // Moves a bit the items so they scatter around
            instance.transform.Translate(new Vector3(Random.Range(0,0.25f), Random.Range(0,0.25f), 0));
        }
    }
}
