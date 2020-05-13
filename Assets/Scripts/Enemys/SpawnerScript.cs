using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] float spawnChance = .5f;
    [SerializeField] GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.value <= spawnChance)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyAI>().target = PlayerScript.player.transform;
        }
    }

    
}
