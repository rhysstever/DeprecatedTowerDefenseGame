using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	// ===== Set in inspector =====
	// Basic tower prefabs
	public GameObject airTowerPrefab, earthTowerPrefab, fireTowerPrefab, waterTowerPrefab;
	// Advanced tower prefabs
	public GameObject lightningTowerPrefab, iceTowerPrefab, quicksandTowerPrefab, volcanoTowerPrefab;
	public GameObject bulletPrefab;

	// Set at Start()
	private GameObject towers;
	private GameObject bullets;
	public GameObject currentSelectedGameObject;

    // Start is called before the first frame update
    void Start()
    {
		towers = new GameObject("towers");
		bullets = new GameObject("bullets");
		currentSelectedGameObject = null;
	}

	// Update is called once per frame
	void Update()
	{
		if(gameObject.GetComponent<GameManager>().currentMenuState == MenuState.game) {
			if(Input.GetMouseButtonDown(0))
				SelectGameObject();

			if(currentSelectedGameObject != null
				&& currentSelectedGameObject.GetComponent<Tower>() != null)
				FindEligibleUpgradeTypes(currentSelectedGameObject.GetComponent<Tower>().towerType);
		}
	}

	/// <summary>
	/// Selects a gameObject if it is a tile or tower
	/// </summary>
	void SelectGameObject()
	{
		Camera currentCam = Camera.main;

		// Creates ray
		Ray ray = currentCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit rayHit;

		// Creates a layerMask to include all but the Checkpoints layer
		int layerMaskCP = 1 << 8;
		layerMaskCP = ~layerMaskCP;

		// If the ray interects with something in the scene that is not UI nor a checkpoint
		if(Physics.Raycast(ray, out rayHit, Mathf.Infinity, layerMaskCP)) {
			GameObject newSelectedGameObj = FindSelectableGameObject(rayHit.transform.gameObject);
			if(newSelectedGameObj == null)
				return;
			else {
				// If the old selection is a tile, its material is reverted back to normal
				if(currentSelectedGameObject != null
					&& currentSelectedGameObject.tag == "Tile") {
					currentSelectedGameObject.GetComponent<Tile>().SetSelect(false);
				}

				// Sets the new selectedGameObj
				currentSelectedGameObject = newSelectedGameObj;
			}

			// If the new selection is a tile
			if(currentSelectedGameObject != null
				&& currentSelectedGameObject.tag == "Tile") {
				currentSelectedGameObject.GetComponent<Tile>().SetSelect(true);
			}
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
	/// Builds a tower of a given type
	/// </summary>
	/// <param name="element">The type of the tower being built</param>
	public void BuildTower(TowerType element)
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

			// Resets currently selected tile before it changes to the newly built tower
			if(currentSelectedGameObject.tag == "Tile") {
				currentSelectedGameObject.GetComponent<Tile>().SetSelect(false);
			}
			currentSelectedGameObject = newTower;
		}
	}

	/// <summary>
	/// Upgrades a tower with a second element
	/// </summary>
	/// <param name="newElement">The element the tower is being upgraded by</param>
	public void UpgradeTower(TowerType newElement)
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

	/// <summary>
	/// Finds the other 2 elements that can be used to upgrade the current tower
	/// </summary>
	/// <param name="baseElement">The element of the current tower</param>
	void FindEligibleUpgradeTypes(TowerType baseElement)
	{
		(TowerType, TowerType) types = (TowerType.None, TowerType.None);
		foreach(int type in Enum.GetValues(typeof(TowerType))) {
			// Skips the checks if the value is None or the same type
			if(type == 0
				|| type == (int)baseElement)
				continue;

			// Finds the numeric difference between the base element and the type from 
			// the list of values, if a type exists of that numeric difference, it is 
			// added as one of the 2 types to upgrade
			int typeNum = Math.Abs((int)baseElement - type);
			if(Enum.IsDefined(typeof(TowerType), typeNum)) {
				if(types.Item1 == TowerType.None)
					types.Item1 = (TowerType)typeNum;
				else
					types.Item2 = (TowerType)typeNum;
			}

			// Breaks out of the foreach if both types have been found
			if(types.Item1 != TowerType.None
				&& types.Item2 != TowerType.None)
				break;
		}

		// Sends the 2 types to the UI Manager to update the upgrade buttons
		gameObject.GetComponent<UIManager>().UpdateTowerUpgradeButtons(types);
	}
}
