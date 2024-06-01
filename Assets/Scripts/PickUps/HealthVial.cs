using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthVial : MonoBehaviour, ICollectible
{
    public float healAmount;
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.Heal(healAmount);
        Destroy(gameObject);
    }
}
