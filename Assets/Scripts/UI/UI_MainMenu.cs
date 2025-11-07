using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject settings;

    private GameObject currentUI;

    private void Start() {
        currentUI = mainScreen;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && currentUI != mainScreen)
        {
            LoadUI(mainScreen);
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadUI(GameObject _uiToLoad)
    {
        currentUI.gameObject.SetActive(false);
        currentUI = _uiToLoad;
        currentUI.gameObject.SetActive(true);
    }
}
