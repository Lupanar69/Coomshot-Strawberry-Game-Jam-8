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
    private PlayerHealth _playerHealth;

    #endregion

    #region Fonctions Unty

    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    /// <summary>
    /// Quand entre en collision avec le joueur
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("hit");
            _playerHealth.DecreaseHealth();
        }
    }

    #endregion
}
