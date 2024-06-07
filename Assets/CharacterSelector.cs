using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] players = new GameObject[2];
    public Image charImage;
    public static CharacterSelector Instance;
    public CharacterScrptableObject charData;
    public GameObject playerPrefab;

    public int selectedOption = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateCharacter(selectedOption);
    }

    // Update is called once per frame
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

    public void NextOption()
    {
        selectedOption++;
        Debug.Log("pressed");
        if(selectedOption > players.Length - 1)
        {
            selectedOption = 0;
        }
        UpdateCharacter(selectedOption);
    }

    public void LastOption()
    {
        selectedOption--;
        if(selectedOption < 0)
        {
            selectedOption = players.Length - 1;
        }
        Debug.Log("pressed");
        UpdateCharacter(selectedOption);
    }
    private void UpdateCharacter(int option)
    {
        playerPrefab = players[option];
        charImage.sprite = playerPrefab.GetComponent<PlayerStats>().charData.Icon;
        charImage.preserveAspect = true;
        charImage.SetNativeSize();
        SelectCharacter(players[option].GetComponent<PlayerStats>().charData);
        
        Debug.Log(players[option].GetComponent<PlayerStats>().charData.CharName);
    }
}
