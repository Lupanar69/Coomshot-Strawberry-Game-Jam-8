using System;
using UnityEngine;

/// <summary>
/// Le comportement des projectiles
/// </summary>
public abstract class Bullet : MonoBehaviour
{
    #region Ev�nements

    /// <summary>
    /// Quand la balle sort de l'�cran
    /// </summary>
    public EventHandler OnBecomeInvisibleEvent;

    /// <summary>
    /// Quand la balle touche une entit�
    /// </summary>
    public EventHandler<Collider2D> OnTriggerEnterEvent;

    #endregion

    #region Variables d'instance

    /// <summary>
    /// Le Rigidbody
    /// </summary>
    protected Rigidbody2D _rb;

    #endregion

    #region Fonctions Unity

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Quand le projectile sort de l'�cran
    /// </summary>
    private void OnBecameInvisible()
    {
        OnBecomeInvisibleEvent?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Quand entre en collision avec une entit�
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
        // Vide les events pour �viter les fuites de m�moire
        // quand on change de sc�ne

        OnBecomeInvisibleEvent = null;
        OnTriggerEnterEvent = null;
    }

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// D�place le projectile
    /// </summary>
    public abstract void Move();

    #endregion

    #region Fonctions prot�g�es

    /// <summary>
    /// D�place le projectile
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnterCallback(Collider2D collision)
    {
        OnTriggerEnterEvent?.Invoke(this, collision);
    }

    #endregion
}
