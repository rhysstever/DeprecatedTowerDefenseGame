using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapDifficulty
{
	Easy,
	Medium,
	Hard
}

public class Map
{
	#region Fields
	private string name;
	private string[,] layout;
	private int checkpointCount;
	private MapDifficulty difficulty;
	private Texture image;
	#endregion

	#region Properties
	public string Name { get { return name; } }
	public string[,] Layout { get { return layout; } }
	public int CheckpointCount { get { return checkpointCount; } }
	public MapDifficulty Difficulty { get { return difficulty; } }
	public Texture Image { get { return image; } }
	#endregion

	#region Contructors
	/// <summary>
	/// A map that the player can select and play
	/// </summary>
	/// <param name="name">The name of the map</param>
	/// <param name="layout">The 2D array of strings that acts as the "blueprint" of the map</param>
	/// <param name="checkpointCount">The number of checkpoints in the map (EXCLUDING the entrance & exit)</param>
	/// <param name="image">A visual of the map</param>
	/// <param name="difficulty">How tough the map is to beat</param>
	public Map(string name, string[,] layout, int checkpointCount, MapDifficulty difficulty, Texture image) : this(name, layout, checkpointCount, difficulty)
	{
		this.image = image;
	}

	/// <summary>
	/// A map that the player can select and play
	/// </summary>
	/// <param name="name">The name of the map</param>
	/// <param name="layout">The 2D array of strings that acts as the "blueprint" of the map</param>
	/// <param name="checkpointCount">The number of checkpoints in the map (EXCLUDING the entrance & exit)</param>
	/// <param name="difficulty">How tough the map is to beat</param>
	public Map(string name, string[,] layout, int checkpointCount, MapDifficulty difficulty)
	{
		this.name = name;
		this.layout = layout;
		this.checkpointCount = checkpointCount;
		this.difficulty = difficulty;
	}
	#endregion

	#region Methods
	#endregion
}
