using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Pickuper : NetworkBehaviour
{
    public Weapon weapon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var combatSystem = other.GetComponent<CombatSystem>();
        if (combatSystem == null) return;
        if (combatSystem.weapon != null) return;
        if(Input.GetButtonDown("Fire1"))
            Pickup(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var combatSystem = other.GetComponent<CombatSystem>();
        if (combatSystem == null) return;
        if (combatSystem.weapon != null) return;
        if (Input.GetButtonDown("Fire1"))
            Pickup(other.gameObject);
    }


    private void Pickup(GameObject player)
    {
        var combatSystem = player.GetComponent<CombatSystem>();
        if (combatSystem != null)
        { 
            var newWeapon = weapon.SpawnWeapon(combatSystem.weaponHolder);
            combatSystem.weapon = newWeapon.GetComponent<Weapon>();
            Destroy(gameObject);
        }
    }

}
