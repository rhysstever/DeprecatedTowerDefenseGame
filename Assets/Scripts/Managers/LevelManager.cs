using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject tile;
    public int rows;
    public int columns;
    public float rowGap;
    public float colGap;

    // Start is called before the first frame update
    void Start()
    {
        List<int> list = new List<int>();
        list.Add(2);
        list.Add(3);
        list.Add(8);

        PlaceTiles(list);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Creates a grid of tiles
    /// </summary>
    void PlaceTiles()
    {
        PlaceTiles(Vector3.zero, new List<int>());
    }

    /// <summary>
    /// Creates a grid of tiles
    /// </summary>
    /// <param name="startingPoint">The starting point of the grid</param>
    void PlaceTiles(Vector3 startingPoint)
	{
        PlaceTiles(startingPoint, new List<int>());
    }

    /// <summary>
    /// Creates a grid of tiles
    /// </summary>
    /// <param name="emptySpots">The tile number(s) that will not have tiles on them (will be blank)</param>
    void PlaceTiles(List<int> emptySpots)
	{
        PlaceTiles(Vector3.zero, emptySpots);
	}

    /// <summary>
    /// Creates a grid of tiles
    /// </summary>
    /// <param name="startingPoint">The starting point of the grid</param>
    /// <param name="emptySpots">The tile number(s) that will not have tiles on them (will be blank)</param>
    void PlaceTiles(Vector3 startingPoint, List<int> emptySpots)
	{
        // Creates an empty, parent GO 
        GameObject tiles = new GameObject("tiles");

        // Finds the size of the tile prefab
        float length = tile.GetComponent<BoxCollider>().size.x;
        float width = tile.GetComponent<BoxCollider>().size.z;

        // Loops through the rows and columns
        int tileNumTotal = 0;
        int tileNumPlaced = 0;
        for(int col = 0; col < columns; col++) {
            for(int row = 0; row < rows; row++) {
                tileNumTotal++;
                if(emptySpots.Contains(tileNumTotal))
                    continue;

                tileNumPlaced++;
                Vector3 position = startingPoint;
                position += tile.transform.position;

                // Finds the x and z position values for each tiles
                position.x += (row) * (length + rowGap) + rowGap;
                position.z += (col) * (width + colGap) + colGap;
                // Creates the tile at the correct position as a child of the "tiles" GO
                GameObject newTile = Instantiate(tile, position, Quaternion.identity, tiles.transform);
                newTile.name = "tile" + tileNumPlaced;
                newTile.tag = "Tile";
            }
        }
    }
}
