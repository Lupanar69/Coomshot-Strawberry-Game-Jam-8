using UnityEngine;

/// <summary>
/// G�re la logique de la bombe du joueur
/// </summary>
public class Bomb : MonoBehaviour
{
    #region Variables d'instance

    /// <summary>
    /// Le collider de la bombe, activ� par l'Animator
    /// </summary>
    private Collider2D _col;

    #endregion

    #region Fonctions Unity

    // Start is called before the first frame update
    void Start()
    {
        _col = GetComponent<Collider2D>();
        _col.enabled = false;
    }

    /// <summary>
    /// Quand on entre en collision avec un ennemi,
    /// on le d�truit.
    /// Les projectiles g�rent eux-m�mes leur collision avec la bombe
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Enemy":
                collision.GetComponent<EnemyStats>().DecreaseHealth();
                return;

            case "Bullet":
                collision.GetComponent<Bullet>().DisableBullet();
                return;
        }
    }

    #endregion
}
