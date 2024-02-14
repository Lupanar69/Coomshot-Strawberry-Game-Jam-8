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

    #region Propriétés

    /// <summary>
    /// TRUE si l'ennemi vient d'apparaître à l'écran
    /// </summary>
    public bool HasAppeared { get; set; } = false;

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
        OnStart();
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
        // La vérif nous permet de spawner les ennemis
        // hors de l'écran sans que cet event ne soit appelé
        if (this.HasAppeared)
        {
            OnBecomeInvisibleEvent?.Invoke(this, EventArgs.Empty);
            this.HasAppeared = false;
        }
    }

    /// <summary>
    /// Quand l'ennemi apparaît = l'écran
    /// </summary>
    private void OnBecameVisible()
    {
        this.HasAppeared = true;
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

    #region Fonctions publiques

    /// <summary>
    /// Appelée quand l'ennemi est spawné.
    /// Permet de réinitialiser ses variables si besoin.
    /// </summary>
    public virtual void OnSpawned()
    {

    }

    #endregion

    #region Fonctions protégées

    /// <summary>
    /// Pour initialiser les scripts enfants si besoin
    /// </summary>
    protected abstract void OnStart();

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
