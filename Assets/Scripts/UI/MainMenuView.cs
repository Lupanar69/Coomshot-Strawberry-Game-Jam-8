using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// UI du menu ppal
/// </summary>
public class MainMenuView : MonoBehaviour
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
    /// Lance la scène de sélection du niveau
    /// </summary>
    public void Play()
    {
        SceneManager.LoadSceneAsync("LevelSelectScene", LoadSceneMode.Single);
    }

    /// <summary>
    /// Affiche les paramètres
    /// </summary>
    public void Settings()
    {
        _mainMenuCanvas.alpha = 0f;
        _mainMenuCanvas.interactable = false;
        _mainMenuCanvas.blocksRaycasts = false;

        _settingsCanvas.alpha = 1f;
        _settingsCanvas.interactable = true;
        _settingsCanvas.blocksRaycasts = true;
    }

    /// <summary>
    /// Quitte le jeu si on est sur une version bureau
    /// (sur la version web, penser à cacher le bouton)
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    #endregion
}
