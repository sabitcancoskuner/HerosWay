using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "ScriptableObjects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public GameObject enemyPrefab;
    public int spawnWeight;
}
