using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public CharacterScrptableObject charData;


    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentMagnet;

    public int enemiesKilled = 0;

    public List<GameObject> spawnedWeapons;

    
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 0;
    public int experienceCap;

    [Header("UI")]
    public UnityEngine.UI.Image healthBar;
    public UnityEngine.UI.Image expBar;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    #region Current Stats 
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
            }
        }
    }
    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
            }
        }
    }
    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
            }
        }
    }
    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
            }
        }
    }
    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
            }
        }
    }
    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if (currentMagnet != value)
            {
                currentMagnet = value;
            }
        }
    }
    #endregion

    public List<LevelRange> levelRanges;
    InventoryManager inventory;
    public int passiveItemIndex = 0;
    public int weaponIndex = 0;

    //Invincibility frames
    [Header("IFrames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    void Awake()
    {
        //CharacterSelector.Instance.DestroySingleton();
        inventory = GetComponent<InventoryManager>();
        CurrentHealth = charData.MaxHealth;
        CurrentRecovery = charData.Recovery;
        CurrentMoveSpeed = charData.MoveSpeed;
        CurrentMight = charData.Might;
        CurrentProjectileSpeed = charData.ProjectileSpeed;
        CurrentMagnet = charData.Magnet;
        SpawnWeapon(charData.StartingWeapon);
        Debug.Log(inventory.passiveItemsSlots.Count - 1 );
    }
    private void Start()
    { 
        experienceCap = levelRanges[0].experienceCapIncrease;

        GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile speed: " + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "Magnet radius: " + currentMagnet;

        GameManager.instance.AssignChosenCharacterUI(charData);
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
        UpdateExpBar();

    }

    public void Heal(float value)
    {
        if (CurrentHealth < charData.MaxHealth)
        {
            CurrentHealth += value;
            if (CurrentHealth > charData.MaxHealth)
            {
                CurrentHealth = charData.MaxHealth;
            }
        }
        UpdateHealthBar();
    }

    void Recover()
    {
        if (CurrentHealth < charData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;
            if (CurrentHealth > charData.MaxHealth)
            {
                CurrentHealth = charData.MaxHealth;
            }
            UpdateHealthBar();
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if (weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            return;
        }
        weaponIndex++;
        GameObject spawnedWeapon = Instantiate(weapon,transform.position,Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());
        
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.passiveItemsSlots.Count - 1)
        {
            return;
        }
        passiveItemIndex++;
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPasiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());
        
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
            GameManager.instance.StartLevelUp();
        }
    }

    public void TakeDamage(float dmg)
    {
        if(!isInvincible)
        {
            CurrentHealth -= dmg;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            UpdateHealthBar();
            if (CurrentHealth <= 0)
            {
                Kill();
            }
        }
        
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / charData.MaxHealth;
    }

    void UpdateExpBar()
    {
        expBar.fillAmount = experienceCap != 0 ? (float)experience / experienceCap : 0;
    }

    void Kill()
    {
        if(!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReached(level);
            GameManager.instance.AssignWeaponItems(inventory.weaponUiSlots, inventory.passiveUiSlots);
            GameManager.instance.GameOver();
        }
    }


    // Update is called once per frame
    
}
