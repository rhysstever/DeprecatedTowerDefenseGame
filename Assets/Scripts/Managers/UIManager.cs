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

    // Level Select UI Elements
    [SerializeField]
    GameObject levelSelectParentObj;

    [SerializeField]
    GameObject mapButtonPrefab;

    [SerializeField]
    GameObject selectedMapParent;

    [SerializeField]
    GameObject selectedMapImage;

    [SerializeField]
    GameObject playMapButton;

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
        playMapButton.GetComponent<Button>().onClick.AddListener(() =>
            gameObject.GetComponent<LevelManager>().CreateLevel(gameObject.GetComponent<LevelManager>().selectedMapIndex));

        // Game Elements
        startWaveButton.GetComponent<Button>().onClick.AddListener(() => 
            gameObject.GetComponent<EnemyManager>().StartWave());

        // GameOver Elements

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
        // Updates wave text
        if(gameObject.GetComponent<StateManager>().currentMenuState == MenuState.game) {
            waveText.text = gameObject.GetComponent<EnemyManager>().currentWave.Description();
            enemyCounter.text = gameObject.GetComponent<EnemyManager>().currentWave.EnemiesLeft + " / "
                + gameObject.GetComponent<EnemyManager>().currentWave.EnemyCount + " enemies left";
        }
    }

    /// <summary>
    /// Activates all current menuState UI elements, deactivating all others
    /// </summary>
    /// <param name="menuState">the current menuState</param>
    public void ActivateUI(MenuState menuState)
	{
        for(int i = 0; i < parentObjs.Count; i++) {
            switch(menuState) {
                case MenuState.mainMenu:
                    if(parentObjs[i] == mainMenuParentObj)
                        parentObjs[i].SetActive(true);
                    else
                        parentObjs[i].SetActive(false);
                    break;
                case MenuState.levelSelect:
                    if(parentObjs[i] == levelSelectParentObj)
                        parentObjs[i].SetActive(true);
                    else
                        parentObjs[i].SetActive(false);
                    break;
                case MenuState.game:
                    if(parentObjs[i] == gameParentObj)
                        parentObjs[i].SetActive(true);
                    else
                        parentObjs[i].SetActive(false);
                    break;
                case MenuState.gameOver:
                    if(parentObjs[i] == gameOverParentObj)
                        parentObjs[i].SetActive(true);
                    else
                        parentObjs[i].SetActive(false);
                    break;
            }
        }        
	}

    /// <summary>
    /// Creates a UI Button to select a map, for each map created
    /// </summary>
    /// <param name="mapNum">The map number</param>
    public void CreateMapButton(int mapNum)
	{
        GameObject newMapButton = Instantiate(mapButtonPrefab, levelSelectParentObj.transform);

        int displayNum = mapNum + 1;
        newMapButton.name = "map" + displayNum + "Button";
        newMapButton.transform.GetComponentInChildren<Text>().text = "Map " + displayNum;

        Vector3 position = new Vector3(-1250, 450);  // starting offset
        position += new Vector3((mapNum % 6) * 300, (mapNum / 6) * -200); // offset based on map number
        position.x /= 2;    // adjust position from world to child position
        position.y /= 2;    // adjust position from world to child position
        newMapButton.transform.position += position;

        newMapButton.GetComponent<Button>().onClick.AddListener(() => SelectMap(mapNum));
    }

    void SelectMap(int mapNum)
	{
        gameObject.GetComponent<LevelManager>().selectedMapIndex = mapNum;
        selectedMapImage.GetComponent<RawImage>().texture = gameObject.GetComponent<LevelManager>().mapImages[mapNum];
    }
}
