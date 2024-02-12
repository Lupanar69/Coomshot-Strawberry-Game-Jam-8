using System;
using UnityEngine;

/// <summary>
/// Le comportement des projectiles des ennemis
/// </summary>
public sealed class EnemyBullet : Bullet
{
    #region Variables Unity

    /// <summary>
    /// La vitesse du projectile
    /// </summary>
    [Header("Settings")]
    [SerializeField]
    private float _moveSpeed = 5f;

    #endregion

    #region Variables d'instance

    /// <summary>
    /// La transform du projectile
    /// </summary>
    private Transform _t;

    /// <summary>
    /// Les stats du joueur
    /// </summary>
    private PlayerStats _playerStats;

    #endregion

    #region Fonctions Unity

    /// <summary>
    /// Init
    /// </summary>
    private void Start()
    {
        _t = transform;
        _playerStats = FindObjectOfType<PlayerStats>();
    }

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// Déplace le projectile
    /// </summary>
    public override void Move()
    {
        _rb.MovePosition(_rb.position + _moveSpeed * Time.fixedDeltaTime * (Vector2)_t.up);
    }

    /// <summary>
    /// Envoie la demande désactivation du projectile.
    /// Permet d'appeler son event depuis une autre classe.
    /// </summary>
    public void DisableBullet()
    {
        OnBecomeInvisibleEvent?.Invoke(this, EventArgs.Empty);
    }

    #endregion

    #region Fonctions protégées

    /// <summary>
    /// Déplace le projectile
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnterCallback(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerStats.DecreaseHealth();
            base.OnTriggerEnterCallback(collision);
        }
    }

    #endregion
}
