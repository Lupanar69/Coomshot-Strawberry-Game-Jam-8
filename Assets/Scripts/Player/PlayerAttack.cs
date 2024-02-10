using System;
using UnityEngine;

/// <summary>
/// Gère les attaques du joueur
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    #region Propriétés

    /// <summary>
    /// Mettre à false pour empêcher le joueur d'attaquer
    /// </summary>
    public bool CanAttack { get; set; }

    /// <summary>
    /// <see langword="true"/> si l'anim de la bombe se joue
    /// </summary>
    public bool BombAnimIsPlaying
    {
        get => _bombAnimator.GetCurrentAnimatorStateInfo(0).IsName("a_bombAttack");
    }

    #endregion

    #region Variables Unity

    /// <summary>
    /// La prefab du projectile du joueur
    /// </summary>
    [Header("Components")]
    [SerializeField]
    private GameObject _playerBulletPrefab;

    /// <summary>
    /// Le conteneur des balles actives
    /// </summary>
    [SerializeField]
    private Transform _activeBulletsParent;

    /// <summary>
    /// Le conteneur des balles inactives
    /// </summary>
    [SerializeField]
    private Transform _inactiveBulletsParent;

    /// <summary>
    /// L'animator de la bombe
    /// </summary>
    [SerializeField]
    private Animator _bombAnimator;

    /// <summary>
    /// La cadence de tir des projectiles
    /// </summary>
    [Header("Settings")]
    [SerializeField]
    private float _fireRate = .2f;

    #endregion

    #region Variables d'instance

    /// <summary>
    /// La transform du joueur
    /// </summary>
    private Transform _t;

    /// <summary>
    /// Les stats du joueur
    /// </summary>
    private PlayerStats _stats;

    /// <summary>
    /// Le timer de la cadence de tir
    /// </summary>
    private float _fireRateTimer;

    #endregion

    #region Fonctions Unity

    // Start is called before the first frame update
    void Start()
    {
        _t = transform;
        _stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_stats.IsDead || !CanAttack || BombAnimIsPlaying)
            return;

        //Si clic droit, lâcher une bombe
        if (Input.GetMouseButtonDown(1))
        {
            UseBomb();
        }

        // Tir continu

        _fireRateTimer += Time.deltaTime;

        if (_fireRateTimer > _fireRate)
        {
            _fireRateTimer = 0f;
            SpawnBullet();
        }
    }

    #endregion

    #region Fonctions privées

    /// <summary>
    /// Instancie un projectile
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void SpawnBullet()
    {
        Transform bullet;

        if (_inactiveBulletsParent.childCount > 0)
        {
            bullet = _inactiveBulletsParent.GetChild(0);
            bullet.gameObject.SetActive(true);
            bullet.SetParent(_activeBulletsParent);
            BulletManager._bullets.Add(bullet.GetComponent<PlayerBullet>());
        }
        else
        {
            bullet = Instantiate(_playerBulletPrefab, _activeBulletsParent).transform;
            PlayerBullet pb = bullet.GetComponent<PlayerBullet>();
            pb.OnBecomeInvisibleEvent += OnBulletBecomeInvisibleCallback;
            pb.OnTriggerEnterEvent += OnBulletTriggerEnterCallback;
            BulletManager._bullets.Add(pb);
        }

        bullet.position = _t.position;
    }

    /// <summary>
    /// Quand la balle quitte l'écran
    /// </summary>
    /// <param name="sender">La balle</param>
    /// <param name="e">Vide</param>
    private void OnBulletBecomeInvisibleCallback(object sender, EventArgs e)
    {
        PlayerBullet pb = sender as PlayerBullet;
        pb.transform.SetParent(_inactiveBulletsParent);
        pb.gameObject.SetActive(false);
        BulletManager._bullets.Remove(pb);
    }

    /// <summary>
    /// Quand la balle touche un ennemi
    /// </summary>
    /// <param name="sender">La balle</param>
    /// <param name="e">Vide</param>
    private void OnBulletTriggerEnterCallback(object sender, Collider2D e)
    {
        PlayerBullet pb = sender as PlayerBullet;
        pb.transform.SetParent(_inactiveBulletsParent);
        pb.gameObject.SetActive(false);
        BulletManager._bullets.Remove(pb);
    }

    /// <summary>
    /// Consomme une bombe et détruit les ennemis et projectiles à l'écran
    /// </summary>
    private void UseBomb()
    {
        if (_stats.NbBombs > 0)
        {
            // TAF : Animation et destruction des ennemis et projectiles

            _bombAnimator.transform.position = _t.position;
            _bombAnimator.Play("a_bombAttack");
            _stats.DecreaseBombs();
        }
    }

    #endregion
}
