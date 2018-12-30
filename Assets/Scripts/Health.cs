using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    private PlayerControllerMultiplayer _player;
    private Rigidbody2D _rb;
    private Vector2 _direction;
    [SyncVar]
    private int _beatenUpValue;
    [SyncVar]
    private bool _canTakeDamage = true;

    public float multiplier = 1.27f;
    public float criticalMultiplier = 2.5f;

    private void Awake()
    {
        _player = GetComponent<PlayerControllerMultiplayer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _canTakeDamage = !_player.isDashing;
        _direction = _player.isFacingRight ? Vector2.right : Vector2.left;

    }

    public void TakeDamage(int damage)
    {
        if (_canTakeDamage)
        {
            _beatenUpValue += damage;
            var knockbackStrength = _beatenUpValue * multiplier * 10f;
            AddKnockback(knockbackStrength,_direction);

            if (_beatenUpValue >= 140f)
            {
                AddKnockback(100f * criticalMultiplier, _direction);
                criticalMultiplier += 10;
            }
        }

    }

    public void AddKnockback(float strength, Vector2 direction)
    {
         if(direction == Vector2.left)
            _rb.AddForce(Vector2.left * 10f * strength, ForceMode2D.Force);

         if(direction == Vector2.right)
             _rb.AddForce(Vector2.right * 10f * strength, ForceMode2D.Force);
    }
}
