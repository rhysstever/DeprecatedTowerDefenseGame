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

	// Set at Start()
	public GameObject towers;

	// Set later in script
	public GameObject currentSelectedGameObject;

    // Start is called before the first frame update
    void Start()
    {
		towers = new GameObject("towers");
    }

	// Update is called once per frame
	void Update()
	{
		if(Input.GetMouseButtonDown(0)) {
			SelectGameObject();
		}
		// Building Towers
		else if(Input.GetKeyDown(KeyCode.Q)) {
			BuildTower(airTowerPrefab);
		}
		else if(Input.GetKeyDown(KeyCode.W)) {
			BuildTower(waterTowerPrefab);
		}
		else if(Input.GetKeyDown(KeyCode.E)) {
			BuildTower(earthTowerPrefab);
		}
		else if(Input.GetKeyDown(KeyCode.R)) {
			BuildTower(fireTowerPrefab);
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
	/// Builds a tower of a given prefab
	/// </summary>
	/// <param name="towerPrefab">The prefab of the tower being built</param>
	void BuildTower(GameObject towerPrefab)
	{
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
			// Creates a new tower and adds it to the tile its being built on
			GameObject newTower = Instantiate(towerPrefab, towerPos, Quaternion.identity, towers.transform);
			newTower.name = "tower " + towers.transform.childCount + " - " + towerPrefab.GetComponent<Tower>().towerTypes[0];
			currentSelectedGameObject.GetComponent<Tile>().tower = newTower;
		}
	}
}
