using System;
using UnityEngine;

/// <summary>
/// Affiche les stats du joueur à l'écran
/// </summary>
public class StatsView : MonoBehaviour
{
    #region Variables Unity

    /// <summary>
    /// Le conteneur des icones des vies
    /// </summary>
    [Header("Components")]
    [SerializeField]
    private Transform _activeLivesParent;

    /// <summary>
    /// Le conteneur des icones des vies inactives
    /// </summary>
    [SerializeField]
    private Transform _inactiveLivesParent;

    /// <summary>
    /// Le conteneur des icones des bombes
    /// </summary>
    [SerializeField]
    private Transform _activeBombsParent;

    /// <summary>
    /// Le conteneur des icones des bombes inactives
    /// </summary>
    [SerializeField]
    private Transform _inactiveBombsParent;

    /// <summary>
    /// La prefab de l'icône de vie
    /// </summary>
    [Space(10)]
    [Header("Prefabs")]
    [SerializeField]
    private GameObject _livePrefab;

    /// <summary>
    /// La prefab de l'icône de bombe
    /// </summary>
    [SerializeField]
    private GameObject _bombPrefab;

    #endregion

    #region Variables d'instance

    /// <summary>
    /// Les stats du joueur
    /// </summary>
    private PlayerStats _stats;

    #endregion

    #region Fonctins Unity

    // Start is called before the first frame update
    void Start()
    {
        _stats = FindObjectOfType<PlayerStats>();
        _stats.OnRequestUIRepaintEvent += OnRequestUIRepaintCallback;
        UpdateUI();
    }

    #endregion

    #region Fonctions privées

    /// <summary>
    /// Appelée quand l'UI doit être màj
    /// </summary>
    private void OnRequestUIRepaintCallback(object sender, EventArgs e)
    {
        UpdateUI();
    }

    /// <summary>
    /// Màj l'ui
    /// </summary>
    private void UpdateUI()
    {
        while (_activeLivesParent.childCount > 0)
        {
            Transform child = _activeLivesParent.GetChild(0);
            child.SetParent(_inactiveLivesParent);
            child.gameObject.SetActive(false);
        }

        while (_activeBombsParent.childCount > 0)
        {
            Transform child = _activeBombsParent.GetChild(0);
            child.SetParent(_inactiveBombsParent);
            child.gameObject.SetActive(false);
        }

        for (int i = 0; i < _stats.NbLives; i++)
        {
            if (_inactiveLivesParent.childCount > 0)
            {
                Transform child = _inactiveLivesParent.GetChild(0);
                child.SetParent(_activeLivesParent);
                child.gameObject.SetActive(true);
            }
            else
            {
                Instantiate(_livePrefab, _activeLivesParent);
            }
        }

        for (int i = 0; i < _stats.NbBombs; i++)
        {
            if (_inactiveBombsParent.childCount > 0)
            {
                Transform child = _inactiveBombsParent.GetChild(0);
                child.SetParent(_activeBombsParent);
                child.gameObject.SetActive(true);
            }
            else
            {
                Instantiate(_bombPrefab, _activeBombsParent);
            }
        }
    }

    #endregion
}
