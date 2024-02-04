using UnityEngine;

/// <summary>
/// Déplace le joueur sur le curseur de la souris
/// </summary>
public class MouseMovement : MonoBehaviour
{
    #region Propriétés

    /// <summary>
    /// <see langword="false"/> si on veut immobiliser le joueur
    /// </summary>
    public bool CanMove { get; set; }

    #endregion

    #region Variables Unity

    /// <summary>
    /// Pour adoucir le lerp du mouvement
    /// </summary>
    [SerializeField, Range(0f, .5f)]
    private float _moveSmooth = .2f;

    #endregion

    #region Variables d'instance

    /// <summary>
    /// Le Rigidbody
    /// </summary>
    private Rigidbody2D _rb;

    /// <summary>
    /// La Transform
    /// </summary>
    private Transform _t;

    /// <summary>
    /// La caméra
    /// </summary>
    private Camera _cam;

    #endregion

    #region Fonctions Unity

    // Start is called before the first frame update
    void Start()
    {
        _t = transform;
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
        CanMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!CanMove || !MouseIsInViewport())
            return;

        Vector3 mouseInput = _cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new(mouseInput.x, mouseInput.y);
        Vector2 playerPos = new(_t.position.x, _t.position.y);
        Vector2 distance = mousePos - playerPos;

        if (distance.sqrMagnitude > .01f)
        {
            _rb.MovePosition(_rb.position + _moveSmooth * Vector2.ClampMagnitude(distance, 1f));
        }

    }

    #endregion

    #region Fonctions privées

    /// <summary>
    /// <see langword="true"/> si la souris est dans le rect de la caméra
    /// </summary>
    bool MouseIsInViewport()
    {
        if (!Input.mousePresent)
            return false;

        Vector3 mousePos = _cam.ScreenToViewportPoint(Input.mousePosition);
        return _cam.rect.Contains(mousePos);
    }
    #endregion

}
