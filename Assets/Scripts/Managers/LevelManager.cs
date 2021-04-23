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
    public GameObject level;
    List<(string[,], int)> maps;
    public float tileLength;
    public float tileWidth;

    // Start is called before the first frame update
    void Start()
    {
        level = new GameObject("map");
        maps = new List<(string[,], int)>();

        // Set length and width variables, based on the tilePrefab
        tileLength = tilePrefab.GetComponent<BoxCollider>().size.x;
        tileWidth = tilePrefab.GetComponent<BoxCollider>().size.z;

        CreateMaps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Create 2d arrays for each map
    /// ---Key---
    /// S : Start
    /// C : Checkpoint
    /// E : Exit
    /// = : Path
    /// </summary>
    void CreateMaps()
	{
        (string[,], int) map1 = (new string[,] {
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " },
            { " ", " ", "S", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "C1", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", "C3", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "C2", " ", " " },
            { " ", " ", "=", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " },
            { " ", " ", "=", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " },
            { " ", " ", "=", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " },
            { " ", " ", "=", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " },
            { " ", " ", "C4", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "C5", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", "E", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "C6", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " },
        }, 6);
        (string[,], int) map2 = (new string[,] {
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " },
            { " ", " ", "S", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "C1", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", "C4", "=", "=", "=", "=", "=", "=", "=", "=", "=", "C5", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", "=", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", "=", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", "=", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", "=", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", "=", " ", " ", " ", " ", "E", "=", "=", "=", "=", "C6", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", "=", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", "=", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", "=", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", "=", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "=", " ", " " },
            { " ", " ", "C3", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "=", "C2", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " },
            { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " },
        }, 6);

        maps.Add(map1);
        maps.Add(map2);
    }

    /// <summary>
    /// Builds a level by creating walls, tiles, and checkpoints
    /// </summary>
    /// <param name="mapNum">The index of the selected map</param>
    public void LevelCreation(int mapNum)
    {
        mapNum--; // Converts from counting num to index num

        // Exits the method if the mapNum is outside the bounds of the maps array
        if(mapNum >= maps.Count) {
            Debug.Log("No map exists at number: " + mapNum);
            return;
		}

        // Finds the map based on the map number
        (string[,], int) selectedMap = maps[mapNum];

        // Creats the walls and builds the level
        WallCreation(level);
        BuildLevel(selectedMap.Item1, selectedMap.Item2, level);

        // After the level is created, change the MenuState to game
        gameObject.GetComponent<StateManager>().ChangeMenuState(MenuState.game);
    }

    /// <summary>
    /// Creates walls all around the map
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
    /// Parses the given map to build a level
    /// </summary>
    /// <param name="selectedMap">The 2D array string of the map</param>
    /// <param name="checkpointCount">The number of checkpoints, EXCLUDING entrance & exit</param>
    /// <param name="levelObj"></param>
    void BuildLevel(string[,] selectedMap, int checkpointCount, GameObject levelObj)
	{
        GameObject checkpoints = new GameObject("checkpoints");
        GameObject tiles = new GameObject("tiles");
        checkpoints.transform.parent = levelObj.transform;
        tiles.transform.parent = levelObj.transform;

        GameObject[] cps = new GameObject[checkpointCount + 2];

        for(int r = 0; r < selectedMap.GetLength(0); r++) {
            for(int c = 0; c < selectedMap.GetLength(1); c++) {
                switch(selectedMap[r, c].Substring(0, 1)) {
                    case "S":
                        // Starting Checkpoint
                        GameObject entrance = CreateCheckpoint(checkpoints, new Vector2(r, c), CheckpointType.Entrance);
                        cps[0] = entrance;
                        break;
                    case "C":
                        // Checkpoint
                        GameObject checkpoint = CreateCheckpoint(checkpoints, new Vector2(r, c));
                        int cpNum = int.Parse(selectedMap[r, c].Substring(1, 1));
                        checkpoint.name = "checkpoint" + cpNum;
                        cps[cpNum] = checkpoint;
                        break;
                    case "E":
                        // Exit Checkpoint
                        GameObject exit = CreateCheckpoint(checkpoints, new Vector2(r, c), CheckpointType.Exit);
                        cps[checkpointCount + 1] = exit;
                        break;
                    case "=":
                        // Path
                        // do nothing for right now
                        break;
                    default:
                        // Tile
                        GameObject tile = CreateTile(tiles, new Vector2(r, c));
                        break;
                }
            }
        }

        // Modify Checkpoints
        for(int child = 0; child < checkpoints.transform.childCount; child++) {
            // Set next checkpoints 
            switch(checkpoints.transform.GetChild(child).gameObject.name) {
                case "entrance":
                    // First checkpoint, next cp is the first "middle" cp
                    checkpoints.transform.GetChild(child).gameObject.GetComponent<Checkpoint>().nextCheckpoint = cps[1];
                    break;
                case "exit":
                    // Last checkpoint, doesnt have a next cp
                    checkpoints.transform.GetChild(child).gameObject.GetComponent<Checkpoint>().nextCheckpoint = null;
                    break;
                default:
                    // Middle checkpoints, next cp is the following "middle" cp (or the exit cp)
                    int cpNum = int.Parse(checkpoints.transform.GetChild(child).gameObject.name.Substring(10));
                    checkpoints.transform.GetChild(child).gameObject.GetComponent<Checkpoint>().nextCheckpoint = cps[cpNum + 1];

                    // Disable all middle checkpoints
                    checkpoints.transform.GetChild(child).gameObject.GetComponent<MeshRenderer>().enabled = false;
                    break;
            }
        }
    }

    /// <summary>
    /// Creates a tile at the given coordinates
    /// </summary>
    /// <param name="parent">The parent object the tile will be a child of</param>
    /// <param name="coordinates">The position the tile will be created at</param>
    /// <returns></returns>
    GameObject CreateTile(GameObject parent, Vector2 coordinates)
	{
        // Calculates the world position of the tile using the given coordinates
        Vector3 position = Vector3.zero;
        position = tilePrefab.transform.position;
        position += ConvertCoordToWorldPos(coordinates);

        GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity, parent.transform);
        newTile.name = "tile" + (level.transform.Find("tiles").childCount - 1);

        return newTile;
	}

    /// <summary>
    /// Creates a middle checkpoint at a given position
    /// </summary>
    /// <param name="parent">The GO that will hold the checkpoint in the scene</param>
    /// <param name="coordinates">The row and column # of the checkpoint</param>
    /// <param name="nextCheckpoint">The checkpoint after the current checkpoint</param>
    /// <returns>The newly created checkpoint</returns>
    GameObject CreateCheckpoint(GameObject parent, Vector2 coordinates)
	{
        return CreateCheckpoint(parent, coordinates, CheckpointType.Checkpoint);
	}

    /// <summary>
    /// Creates a type of checkpoint at the given position
    /// </summary>
    /// <param name="parent">The GO that will hold the checkpoint in the scene</param>
    /// <param name="coordinates">The row and column # of the checkpoint</param>
    /// <param name="nextCheckpoint">The checkpoint after the current checkpoint</param>
    /// <returns>The newly created checkpoint</returns>
    GameObject CreateCheckpoint(GameObject parent, Vector2 coordinates, CheckpointType checkpointType)
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
        newCP.GetComponent<Checkpoint>().num = (level.transform.Find("checkpoints").childCount - 1);

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
