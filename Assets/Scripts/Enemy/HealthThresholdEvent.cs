using UnityEngine.Events;

/// <summary>
/// Représente un seuil de santé à partir duquel
/// un événément doit se déclencher
/// </summary>
[System.Serializable]
public class HealthThresholdEvent
{
    #region Propriétés

    /// <summary>
    /// Le seuil de santé à partir duquel
    /// l'événément doit se déclencher
    /// </summary>
    public int NbLives;

    /// <summary>
    /// Appelé quand l'ennemi atteint un certain seuil de PVs.
    /// Ca nous permet par exemple pour les boss de changer de
    /// comportement quand leur vie atteint la moitié
    /// </summary>
    public UnityEvent OnHealthThresholdReachedEvent;

    #endregion
}
