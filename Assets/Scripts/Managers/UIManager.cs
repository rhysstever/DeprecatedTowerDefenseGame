using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private List<GameObject> parentObjs;

    // MainMenu UI Elements
    [SerializeField]
    GameObject mainMenuParentObj;

    [SerializeField]
    GameObject playButton;

    // Game UI Elements
    [SerializeField]
    GameObject levelSelectParentObj;

    [SerializeField]
    GameObject map1Button;

    [SerializeField]
    GameObject map2Button;

    // Game UI Elements
    [SerializeField]
    GameObject gameParentObj;

    [SerializeField]
    GameObject startWaveButton;

    [SerializeField]
    TextMeshProUGUI waveText;

    [SerializeField]
    TextMeshProUGUI enemyCounter;

    // GameOver UI Elements
    [SerializeField]
    GameObject gameOverParentObj;

    [SerializeField]
    GameObject backToMainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        // MainMenu Elements
        playButton.GetComponent<Button>().onClick.AddListener(() => 
            gameObject.GetComponent<StateManager>().ChangeMenuState(MenuState.levelSelect));

        // LevelSelect Elements
        map1Button.GetComponent<Button>().onClick.AddListener(() =>
            gameObject.GetComponent<LevelManager>().LevelCreation(1));

        map2Button.GetComponent<Button>().onClick.AddListener(() =>
            gameObject.GetComponent<LevelManager>().LevelCreation(3));

        // Game Elements
        startWaveButton.GetComponent<Button>().onClick.AddListener(() => 
            gameObject.GetComponent<EnemyManager>().StartWave());

        // Parent UI Elements
        parentObjs = new List<GameObject>();
        parentObjs.Add(mainMenuParentObj);
        parentObjs.Add(levelSelectParentObj);
        parentObjs.Add(gameParentObj);
        parentObjs.Add(gameOverParentObj);
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = gameObject.GetComponent<EnemyManager>().currentWave.Description();
        enemyCounter.text = gameObject.GetComponent<EnemyManager>().currentWave.EnemiesLeft + " / "
            + gameObject.GetComponent<EnemyManager>().currentWave.EnemyCount + " enemies left";
    }

    /// <summary>
    /// Sets certain elements based on the current menuState
    /// </summary>
    /// <param name="menuState">the current menuState</param>
    public void ActivateUI(MenuState menuState)
	{
        // Deactivate all ui elements
        for(int i = 0; i < parentObjs.Count; i++)
            parentObjs[i].SetActive(false);

        switch(menuState) {
            case MenuState.mainMenu:
                mainMenuParentObj.SetActive(true);
                break;
            case MenuState.levelSelect:
                levelSelectParentObj.SetActive(true);
                break;
            case MenuState.game:
                gameParentObj.SetActive(true);
                break;
            case MenuState.gameOver:
                gameOverParentObj.SetActive(true);
                break;
        }
	}
}
