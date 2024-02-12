using System;
using UnityEngine;

/// <summary>
/// Le comportement des projectiles
/// </summary>
public abstract class Bullet : MonoBehaviour
{
    #region Evénements

    /// <summary>
    /// Quand la balle sort de l'écran
    /// </summary>
    public EventHandler OnBecomeInvisibleEvent;

    /// <summary>
    /// Quand la balle touche une entité
    /// </summary>
    public EventHandler<Collider2D> OnTriggerEnterEvent;

    #endregion

    #region Variables d'instance

    /// <summary>
    /// La transform du projectile
    /// </summary>
    protected Transform _t;

    /// <summary>
    /// Le Rigidbody
    /// </summary>
    protected Rigidbody2D _rb;

    #endregion

    #region Fonctions Unity

    // Start is called before the first frame update
    void Start()
    {
        _t = transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Quand le projectile sort de l'écran
    /// </summary>
    private void OnBecameInvisible()
    {
        OnBecomeInvisibleEvent?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Quand entre en collision avec une entité
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnterCallback(collision);
    }

    /// <summary>
    /// Quand on quitte le niveau
    /// </summary>
    private void OnDestroy()
    {
        // Vide les events pour éviter les fuites de mémoire
        // quand on change de scène

        OnBecomeInvisibleEvent = null;
        OnTriggerEnterEvent = null;
    }

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// Envoie la demande désactivation du projectile.
    /// Permet d'appeler son event depuis une autre classe.
    /// </summary>
    public void DisableBullet()
    {
        OnBecomeInvisibleEvent?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Déplace le projectile
    /// </summary>
    public abstract void Move();

    #endregion

    #region Fonctions protégées

    /// <summary>
    /// Déplace le projectile
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnterCallback(Collider2D collision)
    {
        OnTriggerEnterEvent?.Invoke(this, collision);
    }

    #endregion
}
