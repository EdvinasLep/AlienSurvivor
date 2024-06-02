using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    CharacterScrptableObject charData;


    public float currentHealth;
    public float currentRecovery;
    public float currentMoveSpeed;
    public float currentMight;
    public float currentProjectileSpeed;
    public float currentMagnet;

    public List<GameObject> spawnedWeapons;

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

        Recover();
    }

    public void IncreaseExperience(int value)
    {
        experience += value;
        LevelUpChecker();
    }

    public void Heal(float value)
    {
        if (currentHealth < charData.MaxHealth)
        {
            currentHealth += value;
            if (currentHealth > charData.MaxHealth)
            {
                currentHealth = charData.MaxHealth;
            }
        } 
    }

    void Recover()
    {
        if (currentHealth < charData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;
            if (currentHealth > charData.MaxHealth)
            {
                currentHealth = charData.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon,transform.position,Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        spawnedWeapons.Add(spawnedWeapon);
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
        charData = CharSelector.GetData();
        CharSelector.Instance.DestroySingleton();

        
        currentHealth = charData.MaxHealth;
        currentRecovery = charData.Recovery;
        currentMoveSpeed = charData.MoveSpeed;
        currentMight = charData.Might;
        currentProjectileSpeed = charData.ProjectileSpeed;
        currentMagnet = charData.Magnet;
        SpawnWeapon(charData.StartingWeapon);
    }

    // Update is called once per frame
    
}
