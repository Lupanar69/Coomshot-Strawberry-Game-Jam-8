using System;
using UnityEngine;

/// <summary>
/// Gère les mouvements des ennemis
/// </summary>
public abstract class EnemyController : MonoBehaviour
{
    #region Evénements

    /// <summary>
    /// Quand la balle sort de l'écran
    /// </summary>
    public EventHandler OnBecomeInvisibleEvent;

    #endregion

    #region Variables Unity

    /// <summary>
    /// Vitesse de mouvement vertical
    /// </summary>
    [SerializeField]
    protected float _verticalMoveSpeed = -1f;

    /// <summary>
    /// Vitesse de mouvement horizontal
    /// </summary>
    [SerializeField]
    protected float _horizontalMoveSpeed = 1f;

    #endregion

    #region Variables d'instance

    /// <summary>
    /// Le Rigidbody
    /// </summary>
    protected Rigidbody2D _rb;

    /// <summary>
    /// La Transform
    /// </summary>
    protected Transform _t;

    /// <summary>
    /// La force résultant des calculs
    /// </summary>
    protected Vector2 _resultForce;

    #endregion

    #region Fonctions Unity

    // Start is called before the first frame update
    void Start()
    {
        _t = transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        ComputeAI();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Quand l'ennemi sort de l'écran
    /// </summary>
    private void OnBecameInvisible()
    {
        OnBecomeInvisibleEvent?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Quand on quitte le niveau
    /// </summary>
    private void OnDestroy()
    {
        // Vide les events pour éviter les fuites de mémoire
        // quand on change de scène

        OnBecomeInvisibleEvent = null;
    }

    #endregion

    #region Fonctions protégées

    /// <summary>
    /// Détermine quand et comment l'agent doit se déplacer
    /// </summary>
    protected abstract void ComputeAI();

    /// <summary>
    /// Déplace l'agent
    /// </summary>
    private void Move()
    {
        _rb.MovePosition(_rb.position + _resultForce * Time.fixedDeltaTime);
    }


    #endregion

}
