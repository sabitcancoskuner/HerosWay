using System.Collections;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI instance;

    public GameObject inGameUI;
    public GameObject skillSelectUI;
    public GameObject characterUI;
    public GameObject settingsUI;
    public GameObject pauseGameUI;
    
    [Header("End Game")]
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private GameObject endScreenText;
    [SerializeField] private GameObject reloadSceneButton;

    private GameObject currentUI;

    private void Awake() {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else 
        {
            instance = this;
        }
    }
    
    void Start()
    {
        currentUI = inGameUI;

        skillSelectUI.GetComponent<UI_SkillSelect>().onAllCardsSelected += LoadInGameUI;
    }

    void Update()
    {
        if (currentUI == skillSelectUI)
        {
            return;
        }

        if (endScreenText.activeSelf)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (characterUI.activeSelf)
            {
                LoadUI(inGameUI);
            }
            else
            {
                LoadUI(characterUI);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentUI != inGameUI)
            {
                LoadUI(inGameUI);
                return;
            }

            if (currentUI == inGameUI)
            {
                LoadUI(pauseGameUI);
                return;
            }
        }
    }

    public void LoadUI(GameObject _uiToLoad)
    {
        if (_uiToLoad == inGameUI)
        {
            Time.timeScale = 1;
            AudioManager.instance.PlaySfx(11); // play unpause sfx
        }
        else
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                AudioManager.instance.PlaySfx(10); // play pause sfx
            }
        }

        if (currentUI == _uiToLoad)
        {
            return;
        }
        currentUI.SetActive(false);
        currentUI = _uiToLoad;
        currentUI.SetActive(true);
    }

    public void LoadInGameUI()
    {
        LoadUI(inGameUI);
    }

    public void LoadEndScreen()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeIn();
        StartCoroutine("RestartCurrentScene");
    }

    private IEnumerator RestartCurrentScene()
    {
        yield return new WaitForSeconds(1f);
        endScreenText.SetActive(true);

        yield return new WaitForSeconds(1f);
        reloadSceneButton.SetActive(true);
    }

    public void RestartGameButton()
    {
        GameManager.instance.RestartScene();
    }
}
