using UnityEngine;

/// <summary>
/// D�place l'agent en zigzig du haut vers le bas
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

    #region Fonctions prot�g�es

    /// <summary>
    /// D�termine quand et comment l'agent doit se d�placer
    /// </summary>
    protected override void ComputeAI()
    {
        float v = _verticalMoveSpeed;
        float h = Mathf.Sin(Time.time * _sineSpeed) * _horizontalMoveSpeed;
        _resultForce = new Vector2(h, v);
    }

    #endregion
}
