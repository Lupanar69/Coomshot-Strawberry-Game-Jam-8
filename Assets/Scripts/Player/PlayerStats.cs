using System;
using UnityEngine;

/// <summary>
/// Modifie la vie du joueur
/// </summary>
public class PlayerStats : MonoBehaviour
{
    #region Ev�nements

    /// <summary>
    /// Appel� quand l'UI doit �tre repaint
    /// </summary>
    public EventHandler OnRequestUIRepaintEvent = delegate { };

    #endregion

    #region Propri�t�s

    /// <summary>
    /// <see langword="true"/> si le joueur n'a plus de vies
    /// </summary>
    public bool IsDead { get => NbLives == 0; }

    /// <summary>
    /// <see langword="true"/> si le joueur a des bombes
    /// </summary>
    public bool HasBombs { get => NbBombs > 0; }

    /// <summary>
    /// Le nombre de vies restantes au joueur
    /// </summary>
    public int NbLives { get; private set; }

    /// <summary>
    /// Le nombre de bombes restantes au joueur
    /// </summary>
    public int NbBombs { get; private set; }

    /// <summary>
    /// Indique si le joueur a �t� touch�
    /// </summary>
    public bool CanTakeDamage { get; private set; }

    /// <summary>
    /// <see langword="false"/> si on veut rendre le joueur invincible
    /// </summary>
    public bool IsInvincible { get; set; }

    #endregion

    #region Variables Unity

    /// <summary>
    /// Le nombre de vies restantes au joueur
    /// </summary>
    [SerializeField]
    private int _defaultNbLives = 3;

    /// <summary>
    /// Le nombre de bombes restantes au joueur
    /// </summary>
    [SerializeField]
    private int _defaultNbBombs = 3;

    /// <summary>
    /// La dur�e de l'invincibilit� apr�s un coup re�u
    /// </summary>
    [SerializeField]
    private float _invincibilityAfterHitDuration = 5f;

    #endregion

    #region Variables d'instance

    /// <summary>
    /// Le d�compte de l'invincibilit�
    /// </summary>
    private float _invincibilityTimer;

    /// <summary>
    /// L'animator du joueur
    /// </summary>
    private Animator _playerAnimator;

    #endregion

    #region Fonctions Unity

    /// <summary>
    /// Init
    /// </summary>
    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        NbLives = _defaultNbLives;
        NbBombs = _defaultNbBombs;
        CanTakeDamage = true;
    }

    /// <summary>
    /// Quand on change de sc�ne
    /// </summary>
    private void OnDestroy()
    {
        // Vide les events pour �viter les fuites de m�moire
        // quand on change de sc�ne

        OnRequestUIRepaintEvent = null;
    }

    /// <summary>
    /// M�j � chaque frame
    /// </summary>
    private void Update()
    {
        if (IsDead)
        {
            //TAF : Notifier le jeu qu'on a perdu
            //print("dead");
            return;
        }

        if (!CanTakeDamage)
        {
            CheckInvincibility();
        }
    }

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// Augmente la vie du joueur
    /// </summary>
    public void IncreaseHealth()
    {
        NbLives++;
        OnRequestUIRepaintEvent?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Ajoute une bombe au joueur
    /// </summary>
    public void IncreaseBombs()
    {
        NbBombs++;
        OnRequestUIRepaintEvent?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Notifie d'un coup re�u par le joueur
    /// </summary>
    public void DecreaseHealth()
    {
        if (IsInvincible || !CanTakeDamage || IsDead)
            return;

        CanTakeDamage = false;
        NbLives--;
        _invincibilityTimer = 0f;
        _playerAnimator.Play("a_playerHit");
        OnRequestUIRepaintEvent?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Enl�ve une bombe au joueur
    /// </summary>
    public void DecreaseBombs()
    {
        NbBombs--;
        OnRequestUIRepaintEvent?.Invoke(this, EventArgs.Empty);
    }

    #endregion

    #region Fonctions priv�es

    /// <summary>
    /// D�compte le temps d'invincibilit�
    /// et notifie quand elle est termin�
    /// </summary>
    private void CheckInvincibility()
    {
        _invincibilityTimer += Time.deltaTime;

        if (_invincibilityTimer > _invincibilityAfterHitDuration)
        {
            CanTakeDamage = true;
            _playerAnimator.Play("a_playerIdle");
        }
    }

    #endregion
}
