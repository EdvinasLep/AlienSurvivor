using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelector : MonoBehaviour
{
    public static CharSelector Instance;
    public CharacterScrptableObject charData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public static CharacterScrptableObject GetData()
    {
        return Instance.charData;
    }

    public void SelectCharacter(CharacterScrptableObject character)
    {
        charData = character;
    }

    public void DestroySingleton()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
