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
    public GameObject wallPrefab;
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
    public GameObject map;
    public float tileLength;
    public float tileWidth;

    // Start is called before the first frame update
    void Start()
    {
        // Set length and width variables, based on the tilePrefab
        tileLength = tilePrefab.GetComponent<BoxCollider>().size.x;
        tileWidth = tilePrefab.GetComponent<BoxCollider>().size.z;

        // Create map
        MapCreation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// A method to hold all map creation
    /// </summary>
    void MapCreation()
	{
        map = new GameObject("map");
        WallCreation(map);
        TileCreation(map);
        CheckpointCreation(map);
    }

    /// <summary>
    /// A method to hold all creation of walls
    /// </summary>
    /// <param name="mapParent">The parent gameObject that is the map to hold all of the walls</param>
    void WallCreation(GameObject mapParent)
	{
        GameObject walls = new GameObject("walls");
        walls.transform.parent = mapParent.transform;

        // Creates a border of walls
        // Corners
        CreateWall(walls, new Vector2(-1, -1));
        CreateWall(walls, new Vector2(-1, columns));
        CreateWall(walls, new Vector2(rows, -1));
        CreateWall(walls, new Vector2(rows, columns));
        // Sides
        for(int i = 0; i < rows; i++) {
            CreateWall(walls, new Vector2(i, -1));
            CreateWall(walls, new Vector2(i, columns));
        }
        for(int i = 0; i < columns; i++) {
            CreateWall(walls, new Vector2(-1, i));
            CreateWall(walls, new Vector2(rows, i));
        }
    }

    /// <summary>
    /// Creates a wall at the given coordinates
    /// </summary>
    /// <param name="parent">The parent gameObject to hold the wall gameObject</param>
    /// <param name="coordinates">The coordinate of the wall</param>
    void CreateWall(GameObject parent, Vector2 coordinates)
	{
        Vector3 position = wallPrefab.transform.position;

        // Finds the x and z position values for each tiles
        position += ConvertCoordToWorldPos(coordinates);

        // Creates the tile at the correct position as a child of the "tiles" GO
        GameObject newWall = Instantiate(wallPrefab, position, Quaternion.identity, parent.transform);
        int wallNum = parent.transform.childCount;
        newWall.name = "wall" + wallNum;
        newWall.GetComponent<MapObject>().coordinates = coordinates;
    }

    /// <summary>
    /// A method to hold all creation of tiles
    /// </summary>
    /// <param name="mapParent">The parent gameObject that is the map to hold all of the tiles</param>
    void TileCreation(GameObject mapParent)
    {
        // Creates an empty, parent GO to hold all of the tiles in the scene
        GameObject tiles = new GameObject("tiles");
        tiles.transform.parent = mapParent.transform;

        // Create a list to hold all of the coordinates of the blank spaces
        List<Vector2> coords = new List<Vector2>();
        coords.Add(new Vector2(2, 2));      // to be entrance
        coords.Add(new Vector2(2, 3));
        coords.Add(new Vector2(2, 4));
        coords.Add(new Vector2(2, 5));
        coords.Add(new Vector2(2, 6));
        coords.Add(new Vector2(2, 7));
        coords.Add(new Vector2(2, 8));
        coords.Add(new Vector2(2, 9));
        coords.Add(new Vector2(2, 10));
        coords.Add(new Vector2(2, 11));
        coords.Add(new Vector2(2, 12));
        coords.Add(new Vector2(2, 13));
        coords.Add(new Vector2(2, 14));
        coords.Add(new Vector2(2, 15));
        coords.Add(new Vector2(2, 16));
        coords.Add(new Vector2(2, 17));     // to be checkpoint 1
        coords.Add(new Vector2(3, 17));     
        coords.Add(new Vector2(4, 17));
        coords.Add(new Vector2(5, 17));     
        coords.Add(new Vector2(6, 17));
        coords.Add(new Vector2(7, 17));     // to be checkpoint 2
        coords.Add(new Vector2(7, 16));
        coords.Add(new Vector2(7, 15));
        coords.Add(new Vector2(7, 14));
        coords.Add(new Vector2(7, 13));
        coords.Add(new Vector2(7, 12));
        coords.Add(new Vector2(7, 11));
        coords.Add(new Vector2(7, 10));
        coords.Add(new Vector2(7, 9));
        coords.Add(new Vector2(7, 8));
        coords.Add(new Vector2(7, 7));
        coords.Add(new Vector2(7, 6));
        coords.Add(new Vector2(7, 5));
        coords.Add(new Vector2(7, 4));
        coords.Add(new Vector2(7, 3));
        coords.Add(new Vector2(7, 2));      // to be checkpoint 3
        coords.Add(new Vector2(8, 2));
        coords.Add(new Vector2(9, 2));
        coords.Add(new Vector2(10, 2));     
        coords.Add(new Vector2(11, 2));     
        coords.Add(new Vector2(12, 2));     // to be checkpoint 4
        coords.Add(new Vector2(12, 3));
        coords.Add(new Vector2(12, 4));
        coords.Add(new Vector2(12, 5));
        coords.Add(new Vector2(12, 6));
        coords.Add(new Vector2(12, 7));
        coords.Add(new Vector2(12, 8));
        coords.Add(new Vector2(12, 9));
        coords.Add(new Vector2(12, 10));
        coords.Add(new Vector2(12, 11));
        coords.Add(new Vector2(12, 12));
        coords.Add(new Vector2(12, 13));
        coords.Add(new Vector2(12, 14));
        coords.Add(new Vector2(12, 15));
        coords.Add(new Vector2(12, 16));
        coords.Add(new Vector2(12, 17));    // to be checkpoint 5
        coords.Add(new Vector2(13, 17));
        coords.Add(new Vector2(14, 17));    
        coords.Add(new Vector2(15, 17));    
        coords.Add(new Vector2(16, 17));    
        coords.Add(new Vector2(17, 17));    // to be checkpoint 6
        coords.Add(new Vector2(17, 16));
        coords.Add(new Vector2(17, 15));
        coords.Add(new Vector2(17, 14));
        coords.Add(new Vector2(17, 13));
        coords.Add(new Vector2(17, 12));
        coords.Add(new Vector2(17, 11));
        coords.Add(new Vector2(17, 10));
        coords.Add(new Vector2(17, 9));
        coords.Add(new Vector2(17, 8));
        coords.Add(new Vector2(17, 7));
        coords.Add(new Vector2(17, 6));
        coords.Add(new Vector2(17, 5));
        coords.Add(new Vector2(17, 4));
        coords.Add(new Vector2(17, 3));
        coords.Add(new Vector2(17, 2));     // to be exit

        // Create tiles objects
        CreateTiles(tiles, coords);
    }

    /// <summary>
    /// Creates a grid of tiles at the origin and without any blanks
    /// </summary>
    /// <param name="parent">The parent gameObject to hold all the tiles</param>
    void CreateTiles(GameObject parent)
    {
        CreateTiles(parent, Vector3.zero, new List<Vector2>());
    }

    /// <summary>
    /// Creates a grid of tiles at a given position without any blanks
    /// </summary>
    /// <param name="parent">The parent gameObject to hold all the tiles</param>
    /// <param name="startingPoint">The starting point of the grid</param>
    void CreateTiles(GameObject parent, Vector3 startingPoint)
	{
        CreateTiles(parent, startingPoint, new List<Vector2>());
    }

    /// <summary>
    /// Creates a grid of tiles at the origin, leaving blanks at any given coordinates
    /// </summary>
    /// <param name="parent">The parent gameObject to hold all the tiles</param>
    /// <param name="emptySpots">A list of row and column coords (vec2) where no tile will spawn</param>
    void CreateTiles(GameObject parent, List<Vector2> emptySpots)
	{
        CreateTiles(parent, Vector3.zero, emptySpots);
	}

    /// <summary>
    /// Creates a grid of tiles at a given position, leaving blanks at any given coordinates
    /// </summary>
    /// <param name="parent">The parent gameObject to hold all the tiles</param>
    /// <param name="startingPoint">The starting point of the grid</param>
    /// <param name="emptySpots">A list of row and column coords (vec2) where no tile will spawn</param>
    void CreateTiles(GameObject parent, Vector3 startingPoint, List<Vector2> emptySpots)
	{
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
                GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity, parent.transform);
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
    /// <param name="mapParent">The parent gameObject that is the map to hold all map objects</param>
    void CheckpointCreation(GameObject mapParent)
	{
        // Create Checkpoints
        // Creates an empty, parent GO to hold all of the checkpoints in the scene
        GameObject checkpoints = new GameObject("checkpoints");
        checkpoints.transform.parent = mapParent.transform;
        
        GameObject exit = CreateCheckpoint(checkpoints, new Vector2(17, 2), CheckpointType.Exit, null);
        GameObject cp6 = CreateCheckpoint(checkpoints, new Vector2(17, 17), exit);
        GameObject cp5 = CreateCheckpoint(checkpoints, new Vector2(12, 17), cp6);
        GameObject cp4 = CreateCheckpoint(checkpoints, new Vector2(12, 2), cp5);
        GameObject cp3 = CreateCheckpoint(checkpoints, new Vector2(7, 2), cp4);
        GameObject cp2 = CreateCheckpoint(checkpoints, new Vector2(7, 17), cp3);
        GameObject cp1 = CreateCheckpoint(checkpoints, new Vector2(2, 17), cp2);
        GameObject entrance = CreateCheckpoint(checkpoints, new Vector2(2, 2), CheckpointType.Entrance, cp1);

        // Disables the mesh renderer of all checkpoints (except the entrance and exit)
        // to make them "invisible" in the scene
        for(int child = 0; child < checkpoints.transform.childCount; child++) {
            if(child > 0 && child < checkpoints.transform.childCount - 1)
                checkpoints.transform.GetChild(child).gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
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
                newCP.name = "checkpoint" + (map.transform.Find("checkpoints").childCount - 1);
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
        newCP.GetComponent<Checkpoint>().num = (map.transform.Find("checkpoints").childCount - 1);
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
