using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    GameObject mapButtonPrefab;

    [SerializeField]
    GameObject selectedMapParent;

    [SerializeField]
    GameObject selectedMapImage;

    [SerializeField]
    TextMeshProUGUI selectedMapName;

    [SerializeField]
    TextMeshProUGUI selectedMapDifficulty;

    [SerializeField]
    GameObject playMapButton;

    // Game UI Elements
    [SerializeField]
    GameObject gameParentObj;

    [SerializeField]
    TextMeshProUGUI healthText;

    [SerializeField]
    TextMeshProUGUI moneyText;

    [SerializeField]
    GameObject startWaveButton;

    [SerializeField]
    TextMeshProUGUI waveText;

    [SerializeField]
    TextMeshProUGUI enemyCounter;

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
            case MenuState.levelSelect:
                // If the player has selected a map, the selected map ui elements are shown
                selectedMapParent.SetActive(gameObject.GetComponent<LevelManager>().selectedMapIndex >= 0);
                break;
            case MenuState.game:
                healthText.text = "Health: " + gameObject.GetComponent<GameManager>().health;
                moneyText.text = "Cash: " + gameObject.GetComponent<GameManager>().money;
                // Updates current wave text
                waveText.text = gameObject.GetComponent<EnemyManager>().currentWave.Description();
                enemyCounter.text = gameObject.GetComponent<EnemyManager>().currentWave.EnemiesLeft + " / "
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

        Vector3 position = new Vector3(mapButtonXStart, mapButtonYStart);   // starting offset
        position += new Vector3(                                            // offset based on map number
            (mapNum % mapButtonRowCount) * mapButtonXOffset, 
            (mapNum / mapButtonRowCount) * mapButtonYOffset); 
        position /= 2;    // adjust position since this is a child object
        newMapButton.transform.position += position;

        newMapButton.GetComponent<Button>().onClick.AddListener(() => SelectMap(mapNum));
    }

    /// <summary>
    /// Changes the selected map and UI elements
    /// </summary>
    /// <param name="mapNum">The selected map by number</param>
    void SelectMap(int mapNum)
	{
        gameObject.GetComponent<LevelManager>().selectedMapIndex = mapNum;
        Map selectedMap = gameObject.GetComponent<LevelManager>().maps[mapNum];
        selectedMapName.text = selectedMap.Name;
        selectedMapDifficulty.text = selectedMap.Difficulty.ToString();
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
        upgrade1Button.transform.GetComponentInChildren<Text>().text = upgradeTypes.Item1.ToString();
        upgrade1Button.GetComponent<Button>().onClick.RemoveAllListeners();
        upgrade1Button.GetComponent<Button>().onClick.AddListener(() =>
            gameObject.GetComponent<TowerManager>().UpgradeTower(upgradeTypes.Item1));

        upgrade2Button.transform.GetComponentInChildren<Text>().text = upgradeTypes.Item2.ToString();
        upgrade2Button.GetComponent<Button>().onClick.RemoveAllListeners();
        upgrade2Button.GetComponent<Button>().onClick.AddListener(() =>
            gameObject.GetComponent<TowerManager>().UpgradeTower(upgradeTypes.Item2));
    }
}
