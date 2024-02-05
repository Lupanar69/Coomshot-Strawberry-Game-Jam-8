using UnityEngine;

/// <summary>
/// Gère les attaques du joueur
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    #region Variables d'instance

    /// <summary>
    /// Les stats du joueur
    /// </summary>
    private PlayerStats _stats;

    #endregion

    #region Fonctions Unity

    // Start is called before the first frame update
    void Start()
    {
        _stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_stats.IsDead)
            return;

        //Si clic droit, lâcher une bombe
        if (Input.GetMouseButtonDown(1))
        {
            UseBomb();
        }
    }

    #endregion

    #region Fonctions privées

    private void UseBomb()
    {
        // TAF : Animation et destruction des ennemis et projectiles

        _stats.DecreaseBombs();
    }

    #endregion
}
