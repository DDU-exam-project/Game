using System.Collections;
using UnityEngine;

public class EnemyScript : CharacterScript
{
    [SerializeField] float stayAfterDeathTime = 10f;

    EnemyAI aiScript;
    private void Start()
    {
        aiScript = GetComponent<EnemyAI>();
    }

    override
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        WasHit = true;
        if (CurrentHealth <= 0)
        {
            aiScript.animator.SetBool("IsAlive", false);
            StartCoroutine(OnDeathCoroutine());
            return;
        }
        StartCoroutine(HurtCoroutine());
    }

    IEnumerator OnDeathCoroutine()
    {
        yield return new WaitForSeconds(stayAfterDeathTime);
        gameObject.SetActive(false);
    }
}
