using UnityEngine;

/// <summary>
///  Syst�me ppal du jeu
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Variables Unity

    [Space(10)]
    [Header("Components")]
    [Space(10)]

    /// <summary>
    /// Texte Ready
    /// </summary>
    [SerializeField]
    private GameObject _ready;

    /// <summary>
    /// Texte Go
    /// </summary>
    [SerializeField]
    private GameObject _go;

    /// <summary>
    /// Texte Victory
    /// </summary>
    [SerializeField]
    private GameObject _victory;

    [Space(10)]
    [Header("Settings")]
    [Space(10)]

    /// <summary>
    /// Temps d'attente avant le d�but du jeu
    /// </summary>
    [SerializeField]
    private float _countdownBeforeStart = 3f;

    /// <summary>
    /// Temps d'attente apr�s le d�but du jeu
    /// </summary>
    [SerializeField]
    private float _countdownAfterGo = .5f;

    /// <summary>
    /// Temps d'attente apr�s le d�but du jeu
    /// </summary>
    [SerializeField]
    private float _countdownAfterVictory = 5f;

    /// <summary>
    /// La sc�ne � charger apr�s une victoire
    /// </summary>
    [SerializeField]
    private string _sceneToLoadAfterVictory;

    #endregion

    #region Variables d'instance

    /// <summary>
    /// Les stats du joueur
    /// </summary>
    private PlayerStats _stats;

    /// <summary>
    /// Le contr�leur du joueur
    /// </summary>
    private PlayerController _controller;

    /// <summary>
    /// Le component d'attaque du joueur
    /// </summary>
    private PlayerAttack _attack;

    /// <summary>
    /// Temps d'attente avant le d�but du jeu
    /// </summary>
    private float _countdownReadyTimer;

    /// <summary>
    /// Temps d'attente apr�s le d�but du jeu
    /// </summary>
    private float _countdownGoTimer;

    /// <summary>
    /// Temps d'attente apr�s la victoire
    /// </summary>
    private float _countdownVictoryTimer;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _stats = FindObjectOfType<PlayerStats>();
        _controller = FindObjectOfType<PlayerController>();
        _attack = FindObjectOfType<PlayerAttack>();
        _stats.IsInvincible = true;
        _controller.CanMove = false;
        _ready.SetActive(true);
        _go.SetActive(false);
        _victory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_ready.activeSelf)
        {
            _countdownReadyTimer += Time.deltaTime;
        }

        if (_countdownReadyTimer > _countdownBeforeStart)
        {
            _stats.IsInvincible = false;
            _controller.CanMove = true;
            _attack.CanAttack = true;
            _ready.SetActive(false);
            _go.SetActive(true);
        }

        if (_go.activeSelf)
        {
            _countdownGoTimer += Time.deltaTime;
        }

        if (_countdownGoTimer > _countdownAfterGo)
        {
            _go.SetActive(false);
        }

        if (_victory.activeSelf)
        {
            _countdownVictoryTimer += Time.deltaTime;
        }

        if (_countdownVictoryTimer > _countdownAfterVictory)
        {
            _victory.SetActive(false);

            // TAF : Faire une transition
            //SceneManager.LoadScene(_sceneToLoadAfterVictory);
        }
    }

    /// <summary>
    /// Annonce la victoire du joueur
    /// </summary>
    public void Victory()
    {
        _victory.SetActive(true);
        _stats.IsInvincible = true;
        _controller.CanMove = false;
        _attack.CanAttack = false;
    }
}
