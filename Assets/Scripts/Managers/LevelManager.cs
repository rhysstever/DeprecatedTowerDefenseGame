using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CheckpointType
{
    Entrance, 
    Checkpoint,
    Exit
}

public class LevelManager : MonoBehaviour
{
    // Set in inspector
    public GameObject tilePrefab;
    public GameObject entrancePrefab;
    public GameObject checkpointPrefab;
    public GameObject exitPrefab;
    public int rows;
    public int columns;
    public float rowOffset;
    public float colOffset;
    public float rowGap;
    public float colGap;

    // Set at Start()
    public GameObject checkpoints;
    public float tileLength;
    public float tileWidth;

    // Start is called before the first frame update
    void Start()
    {
        // Set length and width variables, based on the tilePrefab
        tileLength = tilePrefab.GetComponent<BoxCollider>().size.x;
        tileWidth = tilePrefab.GetComponent<BoxCollider>().size.z;

        // Create tiles and checkpoints
        TileCreation();
        CheckpointCreation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// A method to hold all creation of tiles
    /// </summary>
    void TileCreation()
	{
        // Create a list to hold all of the coordinates of the blank spaces
        List<Vector2> coords = new List<Vector2>();
        coords.Add(new Vector2(4, 2));      // to be entrance
        coords.Add(new Vector2(4, 3));
        coords.Add(new Vector2(4, 4));
        coords.Add(new Vector2(4, 5));
        coords.Add(new Vector2(4, 6));
        coords.Add(new Vector2(4, 7));
        coords.Add(new Vector2(4, 8));
        coords.Add(new Vector2(4, 9));
        coords.Add(new Vector2(4, 10));     // to be checkpoint 1
        coords.Add(new Vector2(5, 10));
        coords.Add(new Vector2(6, 10));
        coords.Add(new Vector2(7, 10));
        coords.Add(new Vector2(8, 10));
        coords.Add(new Vector2(9, 10));
        coords.Add(new Vector2(10, 10));    // to be checkpoint 2
        coords.Add(new Vector2(10, 9));
        coords.Add(new Vector2(10, 8));
        coords.Add(new Vector2(10, 7));
        coords.Add(new Vector2(10, 6));
        coords.Add(new Vector2(10, 5));
        coords.Add(new Vector2(10, 4));
        coords.Add(new Vector2(10, 3));
        coords.Add(new Vector2(10, 2));     // to be exit

        // Create tiles objects
        CreateTiles(coords);
    }

    /// <summary>
    /// Creates a grid of tiles at the origin and without any blanks
    /// </summary>
    void CreateTiles()
    {
        CreateTiles(Vector3.zero, new List<Vector2>());
    }

    /// <summary>
    /// Creates a grid of tiles at a given position without any blanks
    /// </summary>
    /// <param name="startingPoint">The starting point of the grid</param>
    void CreateTiles(Vector3 startingPoint)
	{
        CreateTiles(startingPoint, new List<Vector2>());
    }

    /// <summary>
    /// Creates a grid of tiles at the origin, leaving blanks at any given coordinates
    /// </summary>
    /// <param name="emptySpots">A list of row and column coords (vec2) where no tile will spawn</param>
    void CreateTiles(List<Vector2> emptySpots)
	{
        CreateTiles(Vector3.zero, emptySpots);
	}

    /// <summary>
    /// Creates a grid of tiles at a given position, leaving blanks at any given coordinates
    /// </summary>
    /// <param name="startingPoint">The starting point of the grid</param>
    /// <param name="emptySpots">A list of row and column coords (vec2) where no tile will spawn</param>
    void CreateTiles(Vector3 startingPoint, List<Vector2> emptySpots)
	{
        // Creates an empty, parent GO to hold all of the tiles in the scene
        GameObject tiles = new GameObject("tiles");

        // Loops through the rows and columns
        for(int row = 0; row < rows; row++) {
            for(int col = 0; col < columns; col++) {
                Vector2 tileCoord = new Vector2(row, col);
                if(emptySpots.Contains(tileCoord))
                    continue;

                Vector3 position = startingPoint;
                position += tilePrefab.transform.position;

                // Finds the x and z position values for each tiles
                position += ConvertCoordToWorldPos(new Vector2(row, col));
                
                // Creates the tile at the correct position as a child of the "tiles" GO
                GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity, tiles.transform);
                int tileNum = (row * rows) + col + 1;
                newTile.name = "tile" + tileNum;
                newTile.GetComponent<MapObject>().coordinates = new Vector2(row, col);
                newTile.GetComponent<Tile>().number = tileNum;
            }
        }
    }

    /// <summary>
    /// A method to hold all creation of checkpoints
    /// </summary>
    void CheckpointCreation()
	{
        // Create Checkpoints
        // Creates an empty, parent GO to hold all of the checkpoints in the scene
        checkpoints = new GameObject("checkpoints");
        
        GameObject exit = CreateCheckpoint(checkpoints, new Vector2(10, 2), CheckpointType.Exit, null);
        GameObject cp2 = CreateCheckpoint(checkpoints, new Vector2(10, 10), exit);
        GameObject cp1 = CreateCheckpoint(checkpoints, new Vector2(4, 10), cp2);
        GameObject entrance = CreateCheckpoint(checkpoints, new Vector2(4, 2), CheckpointType.Entrance, cp1);
    }

    /// <summary>
    /// Creates a middle checkpoint at a given position
    /// </summary>
    /// <param name="parent">The GO that will hold the checkpoint in the scene</param>
    /// <param name="coordinates">The row and column # of the checkpoint</param>
    /// <param name="nextCheckpoint">The checkpoint after the current checkpoint</param>
    /// <returns>The newly created checkpoint</returns>
    GameObject CreateCheckpoint(GameObject parent, Vector2 coordinates, GameObject nextCheckpoint)
	{
        return CreateCheckpoint(parent, coordinates, CheckpointType.Checkpoint, nextCheckpoint);
	}

    /// <summary>
    /// Creates a type of checkpoint at the given position
    /// </summary>
    /// <param name="parent">The GO that will hold the checkpoint in the scene</param>
    /// <param name="coordinates">The row and column # of the checkpoint</param>
    /// <param name="nextCheckpoint">The checkpoint after the current checkpoint</param>
    /// <returns>The newly created checkpoint</returns>
    GameObject CreateCheckpoint(GameObject parent, Vector2 coordinates, CheckpointType checkpointType, GameObject nextCheckpoint)
	{
        // Calculates the position of the checkpoint based on the row and column numbers given
        Vector3 position = Vector3.zero;
        GameObject newCP = null;

		switch(checkpointType) {
            case CheckpointType.Entrance:
                position = entrancePrefab.transform.position;
                position += ConvertCoordToWorldPos(coordinates);
                newCP = Instantiate(entrancePrefab, position, Quaternion.identity, parent.transform);
                newCP.name = "entrance";
                break;
            case CheckpointType.Checkpoint:
                position = checkpointPrefab.transform.position;
                position += ConvertCoordToWorldPos(coordinates);
                newCP = Instantiate(checkpointPrefab, position, Quaternion.identity, parent.transform);
                newCP.name = "checkpoint" + (checkpoints.transform.childCount - 1);
                break;
            case CheckpointType.Exit:
                position = exitPrefab.transform.position;
                position += ConvertCoordToWorldPos(coordinates);
                newCP = Instantiate(exitPrefab, position, Quaternion.identity, parent.transform);
                newCP.name = "exit";
                break;
        }

        // Returns the new checkpoint early if it is null
        if(newCP == null)
            return newCP;

        newCP.GetComponent<Checkpoint>().type = checkpointType;
        newCP.GetComponent<MapObject>().coordinates = coordinates;
        newCP.GetComponent<Checkpoint>().num = (checkpoints.transform.childCount - 1);
        newCP.GetComponent<Checkpoint>().nextCheckpoint = nextCheckpoint;

        return newCP;
    }

    /// <summary>
    /// Converts coordinates to a world position
    /// </summary>
    /// <param name="coordinates">The row (x) and column (y) coordinates</param>
    /// <returns>A Vector3 of the world position (no y-value)</returns>
    Vector3 ConvertCoordToWorldPos(Vector2 coordinates)
	{
        Vector3 position = new Vector3();
        position.x = coordinates.x * (tileLength + rowGap) + rowGap + rowOffset;
        position.z = coordinates.y * (tileWidth + colGap) + colGap + colOffset;
        return position;
	}
}
