using UnityEngine;
using System.Collections;

public class RangeWeapon : Weapon
{
    private int _currentAmmo;
    private float _timer;
    private bool _isReloading;

    public float reloadTime;
    public int ammoPerClip = 10;
    public string opponentTag;
    public GameObject projectile;
    public Transform projectileSpawn;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if ((Input.GetButtonDown("Reload") && _currentAmmo < ammoPerClip) || _currentAmmo == 0)
            StartCoroutine(Reload(reloadTime));

        OnSpawn += Initialize;
    }

    private IEnumerator Reload(float duration)
    {
        _isReloading = true;

        if (_isReloading)
        {
            yield return new WaitForSeconds(duration);

            _currentAmmo = ammoPerClip;
            _isReloading = false;
        }
    }

    public override void Use(Vector2 direction)
    {
        if (Input.GetButton("Fire1"))
        {
            if (timer > attackRate)
            {
                var bullet = Instantiate(projectile, projectileSpawn.position, transform.rotation);
                bullet.transform.parent = transform;

                //RaycastHit2D target = CastRay(direction);
                //Debug.Log($"Attacking via {data.name}, attacked {target.transform.name}");

                //if (target.transform != null) 
                    //OnHit(target.transform.gameObject);

                _currentAmmo--;
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
