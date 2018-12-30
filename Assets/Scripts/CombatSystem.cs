using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CombatSystem : NetworkBehaviour
{
    private Vector2 _direction;
    private PlayerController _player;

    public Weapon weapon;
    public Transform weaponHolder;

    private void Awake()
    {
        if(isLocalPlayer)
            _player = GetComponent<PlayerController>();
    }


    private void Update()
    {
        if(isLocalPlayer)
            _direction = _player.isFacingRight ? Vector2.right : Vector2.left;

        if (weapon != null)
        {
            weapon.CoolDown();
            weapon.Use(_direction);
        }

        if (Input.GetButtonDown("Fire3"))
        {
            weapon?.Throw(_direction);
            weapon = null;
        }
    }
}
