using UnityEngine;

/// <summary>
/// Inflige des dégâts au joueur s'il touche une zone possédant ce script
/// </summary>
public class PlayerDamageArea : MonoBehaviour
{
    #region Variables d'instance

    /// <summary>
    /// Les pts de vie du joueur
    /// </summary>
    private PlayerStats _stats;

    #endregion

    #region Fonctions Unty

    private void Start()
    {
        _stats = FindObjectOfType<PlayerStats>();
    }

    /// <summary>
    /// Quand entre en collision avec le joueur
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_stats.IsDead)
        {
            //print("hit");
            _stats.DecreaseHealth();
        }
    }

    #endregion
}
