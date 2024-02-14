using UnityEngine;

/// <summary>
/// Fait apparaître l'ennemi depuis le haut de l'écran,
/// puis exécute un mouvement en zigzag
/// </summary>
public class BossMoveEnemyController : EnemyController
{
    #region Variables Unity

    /// <summary>
    /// La vitesse d'oscillation du sinus
    /// </summary>
    [SerializeField]
    private float _sineSpeed = 1f;

    /// <summary>
    /// La distance à parcourir pour son arrivée
    /// </summary>
    [SerializeField]
    private float _lerpDistance = 5f;

    /// <summary>
    /// La vitesse du lerp pour son arrivée
    /// </summary>
    [SerializeField]
    private float _lerpSpeed = 1f;

    #endregion

    #region Variables d'instance

    /// <summary>
    /// La distance à parcourir pour son arrivée
    /// </summary>
    private Vector2 _lerpDst;

    /// <summary>
    /// TRUE si l'ennemi doit apparaître
    /// </summary>
    private bool _phaseOne = true;

    /// <summary>
    /// Le coef du lerp
    /// </summary>
    private float _lerpT;

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// Appelée quand l'ennemi est spawné.
    /// Permet de réinitialiser ses variables si besoin.
    /// </summary>
    public override void OnSpawned()
    {
        _phaseOne = true;
        _lerpT = 0f;
    }

    #endregion

    #region Fonctions protégées

    /// <summary>
    /// Détermine quand et comment l'agent doit se déplacer
    /// </summary>
    protected override void ComputeAI()
    {
        if (_phaseOne)
        {
            _lerpT += Time.fixedDeltaTime * _lerpSpeed;
            _t.position = Vector2.Lerp(_t.position, _lerpDst, _lerpT);

            if (_lerpT > 1f)
            {
                _phaseOne = false;
            }
        }
        else
        {
            float v = _verticalMoveSpeed;
            float h = Mathf.Sin(Time.time * _sineSpeed) * _horizontalMoveSpeed;
            _resultForce = new Vector2(h, v);
        }
    }

    /// <summary>
    /// Appelée dans la Start
    /// </summary>
    protected override void OnStart()
    {
        _lerpDst = new Vector2(0f, _t.position.y - _lerpDistance);
    }

    #endregion
}
