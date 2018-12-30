using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon
{
    public string opponentTag;

    private void OnDrawGizmosSelected() => Gizmos.DrawWireSphere(transform.position, range);

    private void Start()
    {
        Initialize();
    }

    private void Update() => OnSpawn += Initialize;

    public override void Use(Vector2 direction)
    {
        if (Input.GetButton("Fire1"))
        {
            if (timer > attackRate)
            {
                Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, range, targetLayer);
                foreach (var target in targets)
                    OnHit(target.gameObject);
                timer = 0;
            }
        }
    }

    public override void OnHit(GameObject target)
    {
        if (target == null) return;
        if (target.CompareTag(opponentTag))
            DoDamage(target);
    }

    public void DoDamage(GameObject target)
    {
        var targetHealth = target.GetComponent<Health>();

        if (targetHealth != null)
            targetHealth.TakeDamage(damage);
    }
}
