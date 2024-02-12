using UnityEngine;

/// <summary>
/// Déplace l'agent en zigzig du haut vers le bas
/// </summary>
public sealed class ZigzagMoveEnemyController : EnemyController
{
    #region Variables Unity

    /// <summary>
    /// La vitesse d'oscillation du sinus
    /// </summary>
    [SerializeField]
    private float _sineSpeed = 1f;

    #endregion

    #region Fonctions protégées

    /// <summary>
    /// Détermine quand et comment l'agent doit se déplacer
    /// </summary>
    protected override void ComputeAI()
    {
        float v = _verticalMoveSpeed;
        float h = Mathf.Sin(Time.time * _sineSpeed) * _horizontalMoveSpeed;
        _resultForce = new Vector2(h, v);
    }

    #endregion
}
