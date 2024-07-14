using UnityEngine;

public class PlayerShooter : MonoBehaviour {
    [SerializeField] private Weapon equippedWeapon;

    public void ShootEquippedGun() {
        equippedWeapon.Attack();
    }

    public void EquipWeapon(Weapon weapon) {
        equippedWeapon.gameObject.SetActive(false);
        weapon.gameObject.SetActive(true);
        equippedWeapon = weapon;
    }
}
