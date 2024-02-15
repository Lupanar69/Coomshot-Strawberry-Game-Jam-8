using UnityEngine;

/// <summary>
/// Fait apparaître l'ennemi en haut ou en bas de l'écran,
/// suit la position du joueur pdt qques temps,
/// puis fonce vers l'autre côté de l'écran
/// </summary>
public class KamikazeEnemyController : EnemyController
{
    #region Variables Unity

    /// <summary>
    /// La vitesse d'oscillation du sinus
    /// </summary>
    [SerializeField]
    private float _zigzagAmplitude = 1f;

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

    /// <summary>
    /// La vitesse de charge
    /// </summary>
    [SerializeField]
    private float _verticalChargeSpeed = 5f;

    /// <summary>
    /// La vitesse de charge
    /// </summary>
    [SerializeField]
    private float _horizontalChargeSpeed = 5f;

    /// <summary>
    /// La durée du sinus avant la phase d'attente
    /// </summary>
    [SerializeField]
    private Vector2 _delayBeforeWaitInterval = new(3f, 5f);

    /// <summary>
    /// Le temps de pause avant la charge
    /// </summary>
    [SerializeField]
    private float _delayBeforeCharge = 1f;

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
    /// TRUE si l'ennemi doit suivre le joueur
    /// </summary>
    private bool _phaseTwo = false;

    /// <summary>
    /// TRUE si l'ennemi doit attendre
    /// </summary>
    private bool _phaseThree = false;

    /// <summary>
    /// TRUE si l'ennemi doit charger
    /// </summary>
    private bool _phaseFour = false;

    /// <summary>
    /// Le coef du lerp
    /// </summary>
    private float _lerpT;

    /// <summary>
    /// Le coef du sinus de suivi
    /// </summary>
    private float _followT;

    /// <summary>
    /// Le temps d'attente avant charge
    /// </summary>
    private float _waitT;

    /// <summary>
    /// La durée du sinus avant la phase d'attente
    /// </summary>
    private float _delayBeforeWait = 3f;

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// Appelée quand l'ennemi est spawné.
    /// Permet de réinitialiser ses variables si besoin.
    /// </summary>
    public override void OnSpawned()
    {
        _lerpDst = new Vector2(0f, _t.position.y - _lerpDistance);
        _phaseOne = true;
        _phaseTwo = false;
        _phaseThree = false;
        _phaseFour = false;
        _lerpT = 0f;
        _followT = 0f;
        _waitT = 0f;
        _delayBeforeWait = Random.Range(_delayBeforeWaitInterval.x, _delayBeforeWaitInterval.y);
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
                _phaseTwo = true;
            }
        }

        if (_phaseTwo)
        {
            _followT += Time.fixedDeltaTime;
            float v = _verticalMoveSpeed;
            float h = Mathf.PingPong(Time.time * _horizontalMoveSpeed, _zigzagAmplitude * 2f) - _zigzagAmplitude;
            _resultForce = new Vector2(h, v);

            if (_followT > _delayBeforeWait)
            {
                _phaseTwo = false;
                _phaseThree = true;
            }
        }

        if (_phaseThree)
        {
            _waitT += Time.fixedDeltaTime;
            _resultForce = Vector2.zero;

            if (_waitT > _delayBeforeCharge)
            {
                _phaseThree = false;
                _phaseFour = true;
            }
        }

        if (_phaseFour)
        {
            float v = _verticalChargeSpeed;
            float h = _horizontalChargeSpeed;
            _resultForce = new Vector2(h, v);
        }
    }

    /// <summary>
    /// Appelée dans la Start
    /// </summary>
    protected override void OnStart()
    {
        _lerpDst = new Vector2(0f, _t.position.y - _lerpDistance);
        _delayBeforeWait = Random.Range(_delayBeforeWaitInterval.x, _delayBeforeWaitInterval.y);
    }

    #endregion
}
