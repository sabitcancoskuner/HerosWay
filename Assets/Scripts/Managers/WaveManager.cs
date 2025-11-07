using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public static WaveManager instance;

    [SerializeField] private UI ui;

    private SpawnManager spawner;
    private Player player;

    public bool isWaveCleared = false;

    public int currentWave;

    private void Awake() {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else {
            instance = this;
        }

        UI.instance.skillSelectUI.GetComponent<UI_SkillSelect>().onAllCardsSelected += StartNextWave;
    }

    private void Start() {
        spawner = SpawnManager.instance;
        player = PlayerManager.instance.player;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F))
        {
            spawner.EnableSpawner();
        }

        if (isWaveCleared)
        {
            spawner.DisableSpawner();
            return;
        }
    }

    public IEnumerator WaveCleared(bool _cleared)
    {
        player.DisableAllSkills();
        player.SetAttackState(false);
        isWaveCleared = _cleared;
        yield return new WaitForSeconds(3f);
        player.DestroyAllSkillObjects();
        ui.LoadUI(ui.skillSelectUI);
    }

    public void StartNextWave()
    {
        currentWave++;
        isWaveCleared = false;
        spawner.SetupSpawner();
        player.SetAttackState(true);
        player.EnableAllSkills();
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}
