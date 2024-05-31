using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    private float currentCooldown;
    

    protected MovementController mController;
    protected virtual void Start()
    {
        mController = FindObjectOfType<MovementController>();
        currentCooldown = weaponData.CooldownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f)
        {
            Attack();
        }
    }
    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}
