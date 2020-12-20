using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerTier
{
	Basic, 
	Advanced, 
	Expert
}

public enum TowerType
{
	Air,
	Water, 
	Earth,
	Fire
}

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
		int layerMask = 1 << 5;
		layerMask = ~layerMask;
		Camera currentCam = Camera.main;

		// Creates ray
		Ray ray = currentCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit rayHit;

		// If the ray interects with something in the scene 
		// and that something is a tile or tower
		if(Physics.Raycast(ray, out rayHit, Mathf.Infinity, layerMask)) {
			// If the old selection is a tile, its material is reverted back to normal
			if(currentSelectedGameObject != null
				&& currentSelectedGameObject.tag == "Tile") {
				currentSelectedGameObject.GetComponent<Tile>().SetSelect(false);
			}

			// Selects the tower or tile accordingly, if 
			if(rayHit.transform.gameObject.tag == "Tower"
				|| rayHit.transform.gameObject.tag == "Tile")
				currentSelectedGameObject = rayHit.transform.gameObject;
			else if(rayHit.transform.parent != null
				&& (rayHit.transform.parent.gameObject.tag == "Tile"
					|| rayHit.transform.parent.gameObject.tag == "Tower"))
				currentSelectedGameObject = rayHit.transform.parent.gameObject;

			// If the new selection is a tile
			if(currentSelectedGameObject.tag == "Tile") {
				currentSelectedGameObject.GetComponent<Tile>().SetSelect(true);
			}
		}
		// Not clicking on anything will unselect the current gameObject
		else {
			currentSelectedGameObject = null;
		}
	}

	void BuildTower(GameObject tower)
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
			towerPos.y += tower.transform.Find("base").gameObject.GetComponent<BoxCollider>().size.y / 2;
			// Creates a new tower and adds it to the tile its being built on
			GameObject newTower = Instantiate(tower, towerPos, Quaternion.identity, towers.transform);
			newTower.name = "tower " + towers.transform.childCount + " - " + tower.GetComponent<Tower>().towerTypes[0];
			currentSelectedGameObject.GetComponent<Tile>().tower = newTower;
		}
	}

	
}
