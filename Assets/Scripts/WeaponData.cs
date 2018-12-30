using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "weaponData", menuName = "Items/Weapon")]
public class WeaponData : ScriptableObject
{
    public string name;
    public GameObject prefab;
    public int damage;
    public float attackRate;
    public float range;
    public LayerMask targetLayer;
    public Vector3 position;
    public Vector3 rotation;
}
