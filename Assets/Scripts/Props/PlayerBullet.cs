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

    #region Fonctions publiques

    /// <summary>
    /// Déplace le projectile
    /// </summary>
    public override void Move()
    {
        _rb.MovePosition(_rb.position + _moveSpeed * Time.fixedDeltaTime * (Vector2)_t.up);
    }

    #endregion

    #region Fonctions protégées

    /// <summary>
    /// Déplace le projectile
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnterCallback(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyStats>().DecreaseHealth();
            base.OnTriggerEnterCallback(collision);
        }
    }

    #endregion
}
