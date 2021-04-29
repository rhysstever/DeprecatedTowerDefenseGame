using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // ===== Set in inspector =====
    // Map Button Creation
    public float mapButtonXStart, mapButtonYStart, mapButtonXOffset, mapButtonYOffset;
    public int mapButtonRowCount;

    // MainMenu UI Elements
    [SerializeField]
    GameObject mainMenuParentObj;

    [SerializeField]
    GameObject playButton;

    // Level Select UI Elements
    [SerializeField]
    GameObject levelSelectParentObj;

    [SerializeField]
    GameObject prevMapButton;

    [SerializeField]
    GameObject nextMapButton;

    [SerializeField]
    GameObject selectedMapImage;

    [SerializeField]
    TextMeshProUGUI selectedMapDescription;

    [SerializeField]
    GameObject playMapButton;

    // Game UI Elements
    [SerializeField]
    GameObject gameParentObj;

    [SerializeField]
    TextMeshProUGUI playerStatsText;

    [SerializeField]
    GameObject startWaveButton;

    [SerializeField]
    TextMeshProUGUI waveText;

    [SerializeField]
    GameObject selectedObjectParent;

    [SerializeField]
    TextMeshProUGUI selectedObjectName;

    [SerializeField]
    TextMeshProUGUI selectedObjectDescription;

    [SerializeField]
    GameObject buildTowerButtonsParent;

    [SerializeField]
    GameObject buildAirTowerButton;

    [SerializeField]
    GameObject buildEarthTowerButton;

    [SerializeField]
    GameObject buildFireTowerButton;

    [SerializeField]
    GameObject buildWaterTowerButton;

    [SerializeField]
    GameObject towerButtonsParent;

    [SerializeField]
    GameObject sellTowerButton;

    [SerializeField]
    GameObject upgrade1Button;

    [SerializeField]
    GameObject upgrade2Button;

    // GameOver UI Elements
    [SerializeField]
    GameObject gameOverParentObj;

    [SerializeField]
    TextMeshProUGUI gameOverHeaderText;

    [SerializeField]
    GameObject backToMainMenuButton;

    // ===== Set at Start() ===== 
    private List<GameObject> parentObjs;

    // Start is called before the first frame update
    void Start()
    {
        // MainMenu Elements
        playButton.GetComponent<Button>().onClick.AddListener(() => 
            gameObject.GetComponent<GameManager>().ChangeMenuState(MenuState.levelSelect));

        // LevelSelect Elements
        playMapButton.GetComponent<Button>().onClick.AddListener(() =>
            gameObject.GetComponent<LevelManager>().CreateLevel(gameObject.GetComponent<LevelManager>().selectedMapIndex));

        // Game Elements
        startWaveButton.GetComponent<Button>().onClick.AddListener(() => 
            gameObject.GetComponent<EnemyManager>().StartWave());
        buildAirTowerButton.GetComponent<Button>().onClick.AddListener(() =>
            gameObject.GetComponent<TowerManager>().BuildTower(TowerType.Air));
        buildEarthTowerButton.GetComponent<Button>().onClick.AddListener(() =>
            gameObject.GetComponent<TowerManager>().BuildTower(TowerType.Earth));
        buildFireTowerButton.GetComponent<Button>().onClick.AddListener(() =>
            gameObject.GetComponent<TowerManager>().BuildTower(TowerType.Fire));
        buildWaterTowerButton.GetComponent<Button>().onClick.AddListener(() =>
            gameObject.GetComponent<TowerManager>().BuildTower(TowerType.Water));
        sellTowerButton.GetComponent<Button>().onClick.AddListener(() =>
            gameObject.GetComponent<TowerManager>().SellTower());

        // GameOver Elements
        backToMainMenuButton.GetComponent<Button>().onClick.AddListener(() =>
            gameObject.GetComponent<GameManager>().ChangeMenuState(MenuState.mainMenu));

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
        switch(gameObject.GetComponent<GameManager>().currentMenuState) {
            case MenuState.mainMenu:
                break;
            case MenuState.levelSelect:
                // Reset all onClicks and hide both buttons
                prevMapButton.GetComponent<Button>().onClick.RemoveAllListeners();
                nextMapButton.GetComponent<Button>().onClick.RemoveAllListeners();
                prevMapButton.SetActive(false);
                nextMapButton.SetActive(false);

                int selectedMapIndex = gameObject.GetComponent<LevelManager>().selectedMapIndex;
                // If there is a previous map, show the back arrow button and add a listener to select that previous map
                if(gameObject.GetComponent<LevelManager>().selectedMapIndex > 0) {
                    prevMapButton.GetComponent<Button>().onClick.AddListener(() => SelectMap(selectedMapIndex - 1));
                    prevMapButton.SetActive(true);
                }

                // If there is a next map, show the next arrow button and add a listener to select that next map
                if(selectedMapIndex < gameObject.GetComponent<LevelManager>().maps.Count - 1) {
                    nextMapButton.GetComponent<Button>().onClick.AddListener(() => SelectMap(selectedMapIndex + 1));
                    nextMapButton.SetActive(true);
                }
                break;
            case MenuState.game:
                // Update player stats like health and money
                playerStatsText.text = "Health: " + gameObject.GetComponent<GameManager>().health
                    + "\nCash: " + gameObject.GetComponent<GameManager>().money;
                // Updates wave text to show what and how many enemies are in the wave, and how many are left
                waveText.text = gameObject.GetComponent<EnemyManager>().currentWave.Description()
                    + "\n" + gameObject.GetComponent<EnemyManager>().currentWave.EnemiesLeft + " / "
                    + gameObject.GetComponent<EnemyManager>().currentWave.EnemyCount + " enemies left";
                
                // Updates selected object text
                // If the selected gameObject exists, and is a tower, then its name is displayed
                GameObject selectedGO = gameObject.GetComponent<TowerManager>().currentSelectedGameObject;

                // If the selected gameObject doesnt exist, no text displays
                if(selectedGO == null) {
                    selectedObjectName.text = "";
                    selectedObjectDescription.text = "";
                    buildTowerButtonsParent.SetActive(false);
                    towerButtonsParent.SetActive(false);
                } // If the selected gameObject is a tile, no text displays but build buttons appear
                else if(selectedGO.GetComponent<Tile>() != null) {
                    selectedObjectName.text = "";
                    selectedObjectDescription.text = "";
                    buildTowerButtonsParent.SetActive(true);
                    towerButtonsParent.SetActive(false);
                } // If the selected gameObject is a tower, it displays its stats
                else if(selectedGO.GetComponent<Tower>() != null) {
                    selectedObjectName.text = selectedGO.name;
                    selectedObjectDescription.text =
                        "Damage: " + selectedGO.GetComponent<Tower>().damage
                        + "\nAttack Time: " + (selectedGO.GetComponent<Tower>().attackTime) + "s"
                        + "\nRange: " + selectedGO.GetComponent<Tower>().range;
                    buildTowerButtonsParent.SetActive(false);
                    // only shows the upgrade buttons if the tower can be upgraded
                    towerButtonsParent.SetActive(selectedGO.GetComponent<Tower>().isUpgradable);
                }
                break;
            case MenuState.gameOver:
                break;
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
                    // Auto select the first map
                    SelectMap(0);
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
                    // Edit game over header to reflect if the player won or lost
                    // The player wins if they complete all waves and have health remaining
                    if(gameObject.GetComponent<GameManager>().health > 0)
                        gameOverHeaderText.text = "You Win!";
                    // The player loses if they run out of health
                    else
                        gameOverHeaderText.text = "Game Over!";
                    break;
            }
        }        
	}

    /// <summary>
    /// Changes the selected map and UI elements
    /// </summary>
    /// <param name="mapNum">The selected map by number</param>
    void SelectMap(int mapNum)
	{
        gameObject.GetComponent<LevelManager>().selectedMapIndex = mapNum;
        Map selectedMap = gameObject.GetComponent<LevelManager>().maps[mapNum];
        selectedMapDescription.text = selectedMap.Name + "\n" + selectedMap.Difficulty;
        selectedMapImage.GetComponent<RawImage>().texture = selectedMap.Image;
    }

    /// <summary>
    /// Updates the text and onClicks of the upgrade buttons
    /// </summary>
    /// <param name="upgradeTypes">A tuple of both elements the tower can be upgraded with</param>
    public void UpdateTowerUpgradeButtons((TowerType, TowerType) upgradeTypes)
	{
        // If either type is None, nothing happens
        if(upgradeTypes.Item1 == TowerType.None
            || upgradeTypes.Item2 == TowerType.None)
            return;

        // For each upgrade button
        // 1. Updates the button's text and remove
        // 2. Removes all listeners from the button
        // 3. Adds UpgradeTower (of the right element) to the listener
        upgrade1Button.transform.GetComponentInChildren<Text>().text = "+" + upgradeTypes.Item1;
        upgrade1Button.GetComponent<Button>().onClick.RemoveAllListeners();
        upgrade1Button.GetComponent<Button>().onClick.AddListener(() =>
            gameObject.GetComponent<TowerManager>().UpgradeTower(upgradeTypes.Item1));

        upgrade2Button.transform.GetComponentInChildren<Text>().text = "+" + upgradeTypes.Item2;
        upgrade2Button.GetComponent<Button>().onClick.RemoveAllListeners();
        upgrade2Button.GetComponent<Button>().onClick.AddListener(() =>
            gameObject.GetComponent<TowerManager>().UpgradeTower(upgradeTypes.Item2));
    }
}