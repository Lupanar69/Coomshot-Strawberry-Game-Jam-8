using System;
using UnityEngine;

/// <summary>
/// G�re les mouvements des ennemis
/// </summary>
public abstract class EnemyController : MonoBehaviour
{
    #region Ev�nements

    /// <summary>
    /// Quand la balle sort de l'�cran
    /// </summary>
    public EventHandler OnBecomeInvisibleEvent;

    #endregion

    #region Propri�t�s

    /// <summary>
    /// TRUE si l'ennemi vient d'appara�tre � l'�cran
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
    /// La force r�sultant des calculs
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
    /// Quand l'ennemi sort de l'�cran
    /// </summary>
    private void OnBecameInvisible()
    {
        // La v�rif nous permet de spawner les ennemis
        // hors de l'�cran sans que cet event ne soit appel�
        if (this.HasAppeared)
        {
            OnBecomeInvisibleEvent?.Invoke(this, EventArgs.Empty);
            this.HasAppeared = false;
        }
    }

    /// <summary>
    /// Quand l'ennemi appara�t = l'�cran
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
        // Vide les events pour �viter les fuites de m�moire
        // quand on change de sc�ne

        OnBecomeInvisibleEvent = null;
    }

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// Appel�e quand l'ennemi est spawn�.
    /// Permet de r�initialiser ses variables si besoin.
    /// </summary>
    public virtual void OnSpawned()
    {

    }

    #endregion

    #region Fonctions prot�g�es

    /// <summary>
    /// Pour initialiser les scripts enfants si besoin
    /// </summary>
    protected abstract void OnStart();

    /// <summary>
    /// D�termine quand et comment l'agent doit se d�placer
    /// </summary>
    protected abstract void ComputeAI();

    /// <summary>
    /// D�place l'agent
    /// </summary>
    private void Move()
    {
        _rb.MovePosition(_rb.position + _resultForce * Time.fixedDeltaTime);
    }


    #endregion

}
