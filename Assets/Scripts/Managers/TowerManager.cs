using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	// Set in inspector
	public GameObject airTowerPrefab;
	public GameObject earthTowerPrefab;
	public GameObject fireTowerPrefab;
	public GameObject waterTowerPrefab;
	public GameObject lightningTowerPrefab;
	public GameObject iceTowerPrefab;
	public GameObject quicksandTowerPrefab;
	public GameObject volcanoTowerPrefab;

	// Set at Start()
	public GameObject towers;
	Dictionary<KeyCode, TowerType> inputTypeDictionary;

	// Set later in script
	public GameObject currentSelectedGameObject;

    // Start is called before the first frame update
    void Start()
    {
		towers = new GameObject("towers");
		inputTypeDictionary = new Dictionary<KeyCode, TowerType>();

		inputTypeDictionary.Add(KeyCode.Q, TowerType.Air);
		inputTypeDictionary.Add(KeyCode.W, TowerType.Water);
		inputTypeDictionary.Add(KeyCode.E, TowerType.Earth);
		inputTypeDictionary.Add(KeyCode.R, TowerType.Fire);
	}

	// Update is called once per frame
	void Update()
	{
		if(gameObject.GetComponent<StateManager>().currentMenuState == MenuState.game) {
			if(Input.GetMouseButtonDown(0))
				SelectGameObject();
			else if(Input.anyKeyDown)
				Construction();
		}
	}

	/// <summary>
	/// Selects a gameObject if it is a tile or tower
	/// </summary>
	void SelectGameObject()
	{
		// If the old selection is a tile, its material is reverted back to normal
		if(currentSelectedGameObject != null
			&& currentSelectedGameObject.tag == "Tile") {
			currentSelectedGameObject.GetComponent<Tile>().SetSelect(false);
		}

		Camera currentCam = Camera.main;

		// Creates ray
		Ray ray = currentCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit rayHit;

		// Creates a layerMask to include all but the UI layer
		int layerMaskUI = 1 << 5;
		layerMaskUI = ~layerMaskUI;

		// Creates a layerMask to include all but the Checkpoints layer
		int layerMaskCP = 1 << 8;
		layerMaskCP = ~layerMaskCP;

		// If the ray interects with something in the scene that is not UI nor a checkpoint
		if(Physics.Raycast(ray, out rayHit, Mathf.Infinity, layerMaskUI)
			&& Physics.Raycast(ray, out rayHit, Mathf.Infinity, layerMaskCP)) {
			// If the selection is the gameObject that is already selected
			if(currentSelectedGameObject == FindSelectableGameObject(rayHit.transform.gameObject))
				currentSelectedGameObject = null;
			// Otherwise the tower or tile is selected (even if a part of it is selected)
			else
				currentSelectedGameObject = FindSelectableGameObject(rayHit.transform.gameObject);

			// If the new selection is a tile
			if(currentSelectedGameObject != null
				&& currentSelectedGameObject.tag == "Tile") {
				currentSelectedGameObject.GetComponent<Tile>().SetSelect(true);
			}
		}
		// Not clicking on anything will unselect the current gameObject
		else {
			currentSelectedGameObject = null;
		}
	}

	/// <summary>
	/// Given a gameObject, finds if that gameObject is a tile or tower, or if it has a parent that is either
	/// </summary>
	/// <param name="currentSelection">The given gameObject</param>
	/// <returns></returns>
	GameObject FindSelectableGameObject(GameObject currentSelection)
	{
		// Loops while the current selection is not a tower nor a tile
		while(currentSelection.GetComponent<Tile>() == null
				&& currentSelection.GetComponent<Tower>() == null) {
			// If the current selection has a parent, the current selection becomes the parent
			if(currentSelection.transform.parent != null)
				currentSelection = currentSelection.transform.parent.gameObject;
			// If the current selection does not have a parent, null is returned
			else
				return null;
		}

		// If the current selection is a tower or tile object, the object is returned
		return currentSelection;
	}

	/// <summary>
	/// Checks which key is being pressed down and decides if a tower is being built or upgraded
	/// </summary>
	void Construction()
	{
		// Loops thr each key in the dictionary, 
		// passing the tower type of the corresponding pressed key
		TowerType towerType = TowerType.None;
		foreach(KeyCode key in inputTypeDictionary.Keys) {
			if(Input.GetKeyDown(key)) {
				towerType = inputTypeDictionary[key];
			}
		}

		// If no key was pressed, the method is exited
		if(towerType == TowerType.None)
			return;

		// If a tile is selected, a tower is built on it
		if(currentSelectedGameObject.GetComponent<Tile>() != null
			&& currentSelectedGameObject.GetComponent<Tile>().tower == null)
			BuildTower(towerType);
		// If a tower is selected, it is upgraded
		else if(currentSelectedGameObject.GetComponent<Tower>() != null)
			UpgradeTower(towerType);
	}

	/// <summary>
	/// Builds a tower of a given type
	/// </summary>
	/// <param name="element">The type of the tower being built</param>
	void BuildTower(TowerType element)
	{
		GameObject towerPrefab = null;
		switch(element) {
			case TowerType.Air:
				towerPrefab = airTowerPrefab;
				break;
			case TowerType.Earth:
				towerPrefab = earthTowerPrefab;
				break;
			case TowerType.Fire:
				towerPrefab = fireTowerPrefab;
				break;
			case TowerType.Water:
				towerPrefab = waterTowerPrefab;
				break;
			case TowerType.Lightning:
				towerPrefab = lightningTowerPrefab;
				break;
			case TowerType.Ice:
				towerPrefab = iceTowerPrefab;
				break;
			case TowerType.Quicksand:
				towerPrefab = quicksandTowerPrefab;
				break;
			case TowerType.Volcano:
				towerPrefab = volcanoTowerPrefab;
				break;
		}

		if(currentSelectedGameObject != null
			&& currentSelectedGameObject.tag == "Tile") {
			// Checks if the tile already has a tower built on it
			if(currentSelectedGameObject.GetComponent<Tile>().tower != null) {
				Debug.Log("There is already a tower built on this tile!");
				return;
			}

			// Calculates the to be new tower's position based on the tile is being built on
			Vector3 towerPos = currentSelectedGameObject.transform.position;
			towerPos.y += towerPrefab.transform.Find("base").gameObject.GetComponent<BoxCollider>().size.y / 2;
			// Creates a new tower
			GameObject newTower = Instantiate(towerPrefab, towerPos, Quaternion.identity, towers.transform);
			newTower.name = towerPrefab.GetComponent<Tower>().towerType + " tower";
			// Adds the tile to the tower and the tower to the tile
			newTower.GetComponent<Tower>().tile = currentSelectedGameObject;
			currentSelectedGameObject.GetComponent<Tile>().tower = newTower;
		}
	}

	void UpgradeTower(TowerType newElement)
	{
		// Adds together the int values of the tower's element and the new element
		int typeTotal = (int)newElement + (int)currentSelectedGameObject.GetComponent<Tower>().towerType;

		// Checks if the total is a value in the enum
		TowerType elementCombo = TowerType.None;
		if(Enum.IsDefined(typeof(TowerType), typeTotal))
			elementCombo = (TowerType)typeTotal;
		else
			return;

		// Destroys basic tower and sets the selected GO to be the base tile
		GameObject tempGO = currentSelectedGameObject;
		currentSelectedGameObject = currentSelectedGameObject.GetComponent<Tower>().tile;
		Destroy(tempGO);

		// Removes the tile's tower and builds the combination tower 
		currentSelectedGameObject.GetComponent<Tile>().tower = null;
		BuildTower(elementCombo);
	}
}
