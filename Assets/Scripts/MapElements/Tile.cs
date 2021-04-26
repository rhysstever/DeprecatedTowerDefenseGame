using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	// Set in inspector
	public Material unselectedMat;
	public Material selectedMat;

	// Set on map load
    public int number;

	// Set at Start()
	public GameObject tower;
    
    // Start is called before the first frame update
    void Start()
    {
		tower = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetSelect(bool isSelected)
	{
		if(isSelected) {
			gameObject.GetComponent<MeshRenderer>().material = selectedMat;
		}
		else {
			gameObject.GetComponent<MeshRenderer>().material = unselectedMat;
		}
	}
}
