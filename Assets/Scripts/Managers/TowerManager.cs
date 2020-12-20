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
		else if(Input.GetKeyDown(KeyCode.B)) {
			BuildTower(airTowerPrefab);
		}
	}

	/// <summary>
	/// Selects a gameObject if it is a tile or tower
	/// </summary>
	void SelectGameObject()
	{
		// If the left mouse button is clicked
		int layerMask = 1 << 5;
		layerMask = ~layerMask;

		// Gets the current Camera
		Camera currentCam = Camera.main;

		// Creates ray
		Ray ray = currentCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit rayHit;

		// If the ray interects with something in the scene 
		// and that something is a tile or tower
		if(Physics.Raycast(ray, out rayHit, Mathf.Infinity, layerMask)
			&& (rayHit.transform.gameObject.tag == "Tile"
			|| rayHit.transform.gameObject.tag == "Tower")) {
			currentSelectedGameObject = rayHit.transform.gameObject;
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
			// Calculates the to be new tower's position based on the tile is being built on
			Vector3 towerPos = currentSelectedGameObject.transform.position;
			towerPos.y += tower.transform.Find("base").gameObject.GetComponent<BoxCollider>().size.y / 2;
			// Creates a new tower
			GameObject newTower = Instantiate(tower, towerPos, Quaternion.identity, towers.transform);
		}
	}
}
