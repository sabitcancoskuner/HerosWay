using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    public bool canPlayBgm;

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

    private void Start() {
        if (SceneManager.GetActiveScene().buildIndex == 0) // if it is main menu play the main menu sound
        {
            Invoke("PlayMainMenuMusic", 0.4f);
        }
        else
        {
            Invoke("PlayBattleTheme", 0.4f);
        }
    }
    
    public void PlaySfx(int _sfxIndex)
    {
        sfx[_sfxIndex].Play();
    }

    public void PlayBgm(int _bgmIndex)
    {
        if (canPlayBgm == false)
        {
            return;
        }

        bgm[_bgmIndex].Play();
    }

    private void PlayMainMenuMusic()
    {
        bgm[0].Play();
    }

    private void PlayBattleTheme()
    {
        bgm[2].Play();
    }
}
