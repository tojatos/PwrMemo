using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button FirstGameButton;
    public Button SecondGameButton;
    public Button ThirdGameButton;
    public Button ExitButton;
    private void Awake()
    {
        FirstGameButton.onClick.AddListener(() =>
        {
            SessionSettings.Instance.tilesHeight = 2;
            SessionSettings.Instance.tilesWidth = 2;
            SceneManager.LoadScene(Scenes.Game);
        });
        SecondGameButton.onClick.AddListener(() =>
        {
            SessionSettings.Instance.tilesHeight = 2;
            SessionSettings.Instance.tilesWidth = 4;
            SceneManager.LoadScene(Scenes.Game);
        });
        ThirdGameButton.onClick.AddListener(() =>
        {
            SessionSettings.Instance.tilesHeight = 4;
            SessionSettings.Instance.tilesWidth = 4;
            SceneManager.LoadScene(Scenes.Game);
        });
        ExitButton.onClick.AddListener(Application.Quit);

        FirstGameButton.Select();
    }
}
