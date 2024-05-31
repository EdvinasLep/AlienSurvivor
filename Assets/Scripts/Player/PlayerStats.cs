using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScrptableObject charData;

    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 0;
    public int experienceCap;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;


    //Invincibility frames
    [Header("IFrames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;
    private void Start()
    { 
        experienceCap = levelRanges[0].experienceCapIncrease;
    }
    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }
    }

    public void IncreaseExperience(int value)
    {
        experience += value;
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if(experience >= experienceCap)
        {
            level++;
            int experienceCapIncrease = 0;
            
            foreach(LevelRange range in levelRanges)
            {
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float dmg)
    {
        if(!isInvincible)
        {
            currentHealth -= dmg;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (currentHealth <= 0)
            {
                Kill();
            }
        }
        
    }

    void Kill()
    {
        Debug.Log("dead");
    }
    void Awake()
    {
        currentHealth = charData.MaxHealth;
        currentRecovery = charData.Recovery;
        currentMoveSpeed = charData.MoveSpeed;
        currentMight = charData.Might;
        currentProjectileSpeed = charData.ProjectileSpeed;
    }

    // Update is called once per frame
    
}
