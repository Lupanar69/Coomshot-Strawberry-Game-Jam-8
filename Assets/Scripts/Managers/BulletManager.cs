using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Déplace toutes les balles depuis ici
///  pour de meilleures perfs
/// </summary>
public class BulletManager : MonoBehaviour
{
    #region Variables statiques

    public static List<Bullet> _bullets;

    #endregion

    #region Fonctions Unity

    private void Start()
    {
        _bullets = new List<Bullet>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            _bullets[i].Move();
        }
    }

    #endregion
}
