using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Gameplay,
        Pause,
        GameOver,
        LevelUp
    }

    public GameState currentState;
    public GameState previousState;

    [Header("UI")]
    public GameObject pauseScreen;
    public GameObject resultScreen;
    public GameObject levelupScreen;

    [Header("Current Stats")]
    public TextMeshProUGUI currentHealthDisplay;
    public TextMeshProUGUI currentRecoveryDisplay;
    public TextMeshProUGUI currentMoveSpeedDisplay;
    public TextMeshProUGUI currentMightDisplay;
    public TextMeshProUGUI currentProjectileSpeedDisplay;
    public TextMeshProUGUI currentMagnetDisplay;

    [Header("Result Screen Display")]
    public Image chosenCharacterImage;
    public TextMeshProUGUI chosenCharName;
    public TextMeshProUGUI timeSurvived;
    public TextMeshProUGUI levelReached;
    public TextMeshProUGUI enemiesKilled;
    public List<Image> chosenWeaponsUI = new List<Image>(2);
    public List<Image> chosenPassiveItemsUI = new List<Image>(2);

    [Header("StopWatch")]
    public float timeLimit;
    float stopwatchTime;
    public TextMeshProUGUI stopwatchDisplay;

    public int score;
    public Transform playerSpawn;
    public GameObject playerPrefab;


    public bool isGameOver = false;
    public bool isChoosingUpgrade = false;

    public GameObject playerObject;

    private void Awake()
    {
        //playerPrefab = CharacterSelector.Instance.playerPrefab;
        //Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DisableScreens(pauseScreen);
        DisableScreens(resultScreen);
        DisableScreens(levelupScreen);
    }
    private void FixedUpdate()
    {
    }
    private void Update()
    {
        switch(currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                UpdateStopwatch();
                UpdateStopwatchDisplay();
                break;
            case GameState.Pause:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                if(!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f;
                    DisplayResults();
                }
                break;
            case GameState.LevelUp:
                if(!isChoosingUpgrade)
                {
                    isChoosingUpgrade = true;
                    Time.timeScale = 0.05f;
                    levelupScreen.SetActive(true);
                    StartLevelUp();
                }
                break;
            default:
                break;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if (currentState != GameState.Pause)
        {
            previousState = currentState;
            ChangeState(GameState.Pause);
            Time.timeScale = 0f;
            EnableScreens(pauseScreen);
        }
    }

    public void ResumeGame()
    {
        if(currentState == GameState.Pause)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            DisableScreens(pauseScreen);
        }
    }

    void CheckForPauseAndResume()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentState == GameState.Pause)
            {
                ResumeGame();
            }
            else PauseGame();
        }
    }

    void DisableScreens(GameObject screenToDisable)
    {
        screenToDisable.SetActive(false);
    }

    void EnableScreens(GameObject screenToEnable)
    {
        screenToEnable.SetActive(true);
    }

    public void GameOver()
    {
        chosenCharacterImage.preserveAspect = true;
        chosenCharacterImage.SetNativeSize();
        enemiesKilled.text = score.ToString();
        timeSurvived.text = stopwatchDisplay.text;
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        EnableScreens(resultScreen);
    }

    public void AssignChosenCharacterUI(CharacterScrptableObject charData)
    {
        chosenCharacterImage.sprite = charData.Icon;
        chosenCharName.text = charData.CharName;
    }

    public void AssignLevelReached(int level)
    {
        levelReached.text = level.ToString();
    }

    public void AssignWeaponItems(List<Image> chosenWeapons, List<Image> chosenPassiveItems)
    {
        for(int i= 0; i < chosenWeaponsUI.Count; i++)
        {
            if (chosenWeapons[i].sprite) {
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeapons[i].sprite;
            }
            else
            {
                chosenWeaponsUI[i].enabled = false;
            }
            
        }
        for (int i = 0; i < chosenPassiveItemsUI.Count; i++)
        {
            if (chosenPassiveItems[i].sprite)
            {
                chosenPassiveItemsUI[i].enabled = true;
                chosenPassiveItemsUI[i].sprite = chosenPassiveItems[i].sprite;
            }
            else
            {
                chosenPassiveItemsUI[i].enabled = false;
            }

        }
    }

    void UpdateStopwatch()
    {
        stopwatchTime += Time.deltaTime;

        if(stopwatchTime >= timeLimit)
        {
            GameOver();
        }
    }

    void UpdateStopwatchDisplay()
    {
        int minutes = Mathf.FloorToInt(stopwatchTime/60);
        int seconds = Mathf.FloorToInt(stopwatchTime%60);

        stopwatchDisplay.text = string.Format("{0:00}:{1:00}",minutes.ToString(),seconds.ToString());
    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
        playerObject.SendMessage("RemoveAndApplyUpgrades");
    }

    public void EndLevelUp()
    {
        isChoosingUpgrade = false;
        Time.timeScale = 1f;
        levelupScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }
}
