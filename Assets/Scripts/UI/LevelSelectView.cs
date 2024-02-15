using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// UI du menu de s�lection des niveaux
/// </summary>
public class LevelSelectView : MonoBehaviour
{
    #region Variables Unity

    /// <summary>
    /// Les sc�nes
    /// </summary>
    [SerializeField]
    private string[] _scenes;

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// Lance le niveau s�lectionn�
    /// </summary>
    public void Play(int index)
    {
        SceneManager.LoadSceneAsync(_scenes[index], LoadSceneMode.Single);
    }

    /// <summary>
    /// Lance la sc�ne de s�lection du niveau
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single);
    }

    #endregion
}
