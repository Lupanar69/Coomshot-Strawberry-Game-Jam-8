using UnityEngine;

/// <summary>
/// UI du menu ppal
/// </summary>
public class SettingsView : MonoBehaviour
{
    #region Variables Unity

    /// <summary>
    /// Le canvas du menu ppal
    /// </summary>
    [SerializeField]
    private CanvasGroup _mainMenuCanvas;

    /// <summary>
    /// Le canvas des paramètres
    /// </summary>
    [SerializeField]
    private CanvasGroup _settingsCanvas;

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// Affiche le menu ppal
    /// </summary>
    public void MainMenu()
    {
        _mainMenuCanvas.alpha = 1f;
        _mainMenuCanvas.interactable = true;
        _mainMenuCanvas.blocksRaycasts = true;

        _settingsCanvas.alpha = 0f;
        _settingsCanvas.interactable = false;
        _settingsCanvas.blocksRaycasts = false;
    }

    #endregion
}
