using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    EnemySpawner enemySpawner;
    //Current stats
    public float currentMoveSpeed;
    public float currentHealth;
    public float currentDamage;

    void Awake()
    {
        //Assign the vaiables
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;

        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
        enemySpawner.OnEnemyKilled();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.CompareTag("Player"))
        {
            PlayerStats player = collision.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }
}
