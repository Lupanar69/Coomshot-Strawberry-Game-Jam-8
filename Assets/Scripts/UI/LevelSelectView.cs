using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// UI du menu de sélection des niveaux
/// </summary>
public class LevelSelectView : MonoBehaviour
{
    #region Variables Unity

    /// <summary>
    /// Les scènes
    /// </summary>
    [SerializeField]
    private string[] _scenes;

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// Lance le niveau sélectionné
    /// </summary>
    public void Play(int index)
    {
        SceneManager.LoadSceneAsync(_scenes[index], LoadSceneMode.Single);
    }

    /// <summary>
    /// Lance la scène de sélection du niveau
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single);
    }

    #endregion
}
