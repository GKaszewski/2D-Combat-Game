using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour
{
    private RangeWeapon _weapon;

    public float speed;
    public float lifeTime;
    public float distance;

    private void Start()
    {
        _weapon = GetComponentInParent<RangeWeapon>();
        distance = _weapon.data.range;
        transform.parent = null;
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, _weapon.data.targetLayer);
        Vector3 bulletDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(bulletDirection * speed * Time.deltaTime);
        Debug.DrawRay(transform.position, transform.up, Color.red);

        if (hit.collider == null) return;
        if (hit.collider.CompareTag("Player"))
        {
            _weapon.CmdWeaponUsed(hit.collider.name);
            var playerHealth = hit.collider.GetComponent<Health>();
            if(playerHealth != null)
                playerHealth.TakeDamage(_weapon.data.damage);
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
