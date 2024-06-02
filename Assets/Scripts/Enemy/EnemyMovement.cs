using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    MovementController player;
    EnemyStats enemy;
    SpriteRenderer sRenderer;

    public float stopDistance;

    void Start()
    {
        player = FindObjectOfType<MovementController>();
        enemy = GetComponent<EnemyStats>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Calculate the direction vector from the enemy to the player
        Vector2 direction = player.transform.position - transform.position;

        // Calculate the distance to the player
        float distanceToPlayer = direction.magnitude;

        if (distanceToPlayer > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
        }



        if (direction.x < 0)
        {
            sRenderer.flipX = true;
        }
        else if(direction.x  > 0)
        {
            sRenderer.flipX = false;
        }
    }
}
