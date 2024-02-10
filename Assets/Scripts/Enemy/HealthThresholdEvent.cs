using UnityEngine.Events;

/// <summary>
/// Repr�sente un seuil de sant� � partir duquel
/// un �v�n�ment doit se d�clencher
/// </summary>
[System.Serializable]
public class HealthThresholdEvent
{
    #region Propri�t�s

    /// <summary>
    /// Le seuil de sant� � partir duquel
    /// l'�v�n�ment doit se d�clencher
    /// </summary>
    public int NbLives;

    /// <summary>
    /// Appel� quand l'ennemi atteint un certain seuil de PVs.
    /// Ca nous permet par exemple pour les boss de changer de
    /// comportement quand leur vie atteint la moiti�
    /// </summary>
    public UnityEvent OnHealthThresholdReachedEvent;

    #endregion
}
