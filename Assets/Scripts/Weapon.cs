using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Networking;


public abstract class Weapon : NetworkBehaviour
{
    private Rigidbody2D _rb;

    protected delegate void OnSpawnDelegate();
    protected event OnSpawnDelegate OnSpawn;
    protected float range;
    protected float attackRate;
    protected float timer;
    protected int damage;
    protected LayerMask targetLayer;

    public WeaponData data;
    public float throwSpeed = 4f;

    protected void Initialize()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Static;
        _rb.simulated = false;
        attackRate = data.attackRate;
        damage = data.damage;
        range = data.range;
        targetLayer = data.targetLayer;
        timer = attackRate;
    }

    protected RaycastHit2D CastRay(Vector2 direction)
    {
        Debug.DrawRay(transform.position, direction, Color.green);
        return Physics2D.Raycast(transform.position, direction, range, targetLayer);
    }

    [Command]
    public void CmdWeaponUsed(string id)
    {
        Debug.Log($"{id} was attacked.");
        var player = GameManager.GetPlayer(id);
        player.GetComponent<Health>()?.TakeDamage(data.damage);
    }

    public void CoolDown() => timer += Time.deltaTime;

    public void Throw(Vector2 direction)
    {
        transform.parent = null;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.simulated = true;
        _rb.velocity = direction * throwSpeed;
        Destroy(gameObject, 1.5f);
    }

    public GameObject SpawnWeapon(Transform weaponSpawn)
    {
        var newWeapon = Instantiate(data.prefab, weaponSpawn.position, Quaternion.identity);
        newWeapon.transform.parent = weaponSpawn;
        OnSpawn?.Invoke();
        AssignPosition(newWeapon);
        return newWeapon;
    }

    private void AssignPosition(GameObject weapon)
    {
        weapon.transform.localPosition = data.position;
        weapon.transform.localRotation = Quaternion.Euler(data.rotation);
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    [Client]
    public abstract void Use(Vector2 direction);

    public abstract void OnHit(GameObject target);
}
