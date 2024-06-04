using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<PassiveItem> passiveItemsSlots = new List<PassiveItem>(2);
    public int[] passiveItemLevels = new int[3];
    public List<Image> passiveUiSlots = new List<Image>(2);
    public List<WeaponController> weaponSlots = new List<WeaponController>(2);
    public int[] weaponLevel = new int[3];
    public List<Image> weaponUiSlots = new List<Image>(2);

    public void AddPasiveItem(int slotIndex, PassiveItem passiveItem)
    {
        passiveItemsSlots[slotIndex] = passiveItem;
        passiveItemLevels[slotIndex] = passiveItem.passiveItemData.Level;
        passiveUiSlots[slotIndex].enabled = true;
        passiveUiSlots[slotIndex].sprite = passiveItem.passiveItemData.Icon;
    }

    public void LevelUpPassiveItem(int slotIndex)
    {
        Debug.Log("Started");
        if(passiveItemsSlots.Count > slotIndex)
        {
            PassiveItem passiveItem = passiveItemsSlots[slotIndex];
            if (!passiveItem.passiveItemData.NextLevelPrefab)
            {
                Debug.Log("Fail");
                return;
            }
            GameObject upgradedPassiveItem = Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedPassiveItem.transform.SetParent(transform);
            Destroy(passiveItem.gameObject);
            passiveItemsSlots[slotIndex] = upgradedPassiveItem.GetComponent<PassiveItem>();
            passiveItemLevels[slotIndex] = upgradedPassiveItem.GetComponent<PassiveItem>().passiveItemData.Level;
            Debug.Log("Increased");
        }
    }

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevel[slotIndex] = weapon.weaponData.Level;
        weaponUiSlots[slotIndex].enabled = true;
        weaponUiSlots[slotIndex].sprite = weapon.weaponData.Icon;
    }

    public void LevelUpWeapon(int slotIndex)
    {
        if (weaponSlots.Count > slotIndex)
        {
            WeaponController weapon = weaponSlots[slotIndex];
            if (!weapon.weaponData.NextLevelPrefab)
            {
                return;
            }
            GameObject upgradedWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedWeapon.transform.SetParent(transform);
            Destroy(weapon.gameObject);
            weaponSlots[slotIndex] = upgradedWeapon.GetComponent<WeaponController>();
            weaponLevel[slotIndex] = upgradedWeapon.GetComponent<WeaponController>().weaponData.Level;
        }
    }
}
