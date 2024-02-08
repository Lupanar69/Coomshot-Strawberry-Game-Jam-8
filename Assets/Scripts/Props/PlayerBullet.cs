using UnityEngine;

/// <summary>
/// Le comportement des projectiles du joueur
/// </summary>
public sealed class PlayerBullet : Bullet
{
    #region Variables Unity

    /// <summary>
    /// La vitesse du projectile
    /// </summary>
    [Header("Settings")]
    [SerializeField]
    private float _moveSpeed = 5f;

    #endregion

    #region Fonctions prot�g�es

    /// <summary>
    /// D�place le projectile
    /// </summary>
    protected override void Move()
    {
        _rb.MovePosition(_rb.position + _moveSpeed * Time.fixedDeltaTime * Vector2.up);
    }

    /// <summary>
    /// D�place le projectile
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnterCallback(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // TAF : Infliger des d�g�ts

            base.OnTriggerEnterCallback(collision);
        }
    }

    #endregion
}
