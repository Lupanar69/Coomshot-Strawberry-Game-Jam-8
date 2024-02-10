using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Les stats de l'ennemi
/// </summary>
public class EnemyStats : MonoBehaviour
{
    #region Evénements

    /// <summary>
    /// Appelé quand l'ennemi est détruits
    /// </summary>
    public EventHandler OnEnemyDestroyedEvent = delegate { };

    #endregion

    #region Propriétés

    /// <summary>
    /// <see langword="true"/> si l'ennemi n'a plus de vies
    /// </summary>
    public bool IsDead { get => NbLives == 0; }

    /// <summary>
    /// Le nombre de vies restantes à l'ennemi
    /// </summary>
    public int NbLives { get; private set; }

    /// <summary>
    /// <see langword="false"/> si on veut rendre l'ennemi invincible
    /// </summary>
    public bool IsInvincible { get; set; }

    #endregion

    #region Variables Unity

    /// <summary>
    /// Le nombre de vies restantes à l'ennemi
    /// </summary>
    [SerializeField]
    private int _defaultNbLives = 1;

    /// <summary>
    /// Les événements déclenchés lorsque la vie de l'ennemi
    /// atteint un certain seuil
    /// </summary>
    [SerializeField]
    private List<HealthThresholdEvent> _healthThresholdEvents;

    #endregion

    #region Variables d'instance

    /// <summary>
    /// L'animator de l'ennemi
    /// </summary>
    private Animator _enemyAnimator;

    #endregion

    #region Fonctions Unity

    /// <summary>
    /// Init
    /// </summary>
    private void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
        NbLives = _defaultNbLives;
    }

    /// <summary>
    /// Quand on change de scène
    /// </summary>
    private void OnDestroy()
    {
        // Vide les events pour éviter les fuites de mémoire
        // quand on change de scène

        OnEnemyDestroyedEvent = null;
    }

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// Vide entièrement la vie d'un ennemi et
    /// appelle ses événéments.
    /// Utile par exemple pour un boss composé de plusieurs ennemis
    /// </summary>
    public void Kill()
    {
        NbLives = 0;
        OnEnemyDestroyedEvent?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Notifie d'un coup reçu par le joueur
    /// </summary>
    public void DecreaseHealth()
    {
        if (IsInvincible)
            return;

        NbLives--;
        _enemyAnimator.Play("a_enemyHit");

        /// Indique au système si l'ennemi doit être détruit
        if (NbLives == 0)
        {
            OnEnemyDestroyedEvent?.Invoke(this, EventArgs.Empty);
            return;
        }

        /// Si on atteint un certain seuil de NbLives,
        /// on joue l'événement correspondant s'il y en a
        if (_healthThresholdEvents.Count > 0 && _healthThresholdEvents[0].NbLives == NbLives)
        {
            _healthThresholdEvents[0].OnHealthThresholdReachedEvent?.Invoke();
            _healthThresholdEvents.RemoveAt(0);
        }
    }

    #endregion
}
