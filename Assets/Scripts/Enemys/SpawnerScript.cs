using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] float spawnChance = .5f;
    [SerializeField] GameObject enemyPrefab;
    bool alreadySpawned = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (Random.value <= spawnChance && !alreadySpawned)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity,transform.parent);
            enemy.GetComponent<EnemyAI>().target = PlayerScript.player.transform;
            alreadySpawned = true;

        }
    }

    
}
