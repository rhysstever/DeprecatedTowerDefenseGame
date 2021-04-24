using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
	#region Fields
	private string name;
	private string[,] layout;
	private int checkpointCount;
	private Texture image;
	#endregion

	#region Properties
	public string Name { get { return name; } }
	public string[,] Layout { get { return layout; } }
	public int CheckpointCount { get { return checkpointCount; } }
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
	public Map(string name, string[,] layout, int checkpointCount, Texture image) : this(name, layout, checkpointCount)
	{
		this.image = image;
	}

	/// <summary>
	/// A map that the player can select and play
	/// </summary>
	/// <param name="name">The name of the map</param>
	/// <param name="layout">The 2D array of strings that acts as the "blueprint" of the map</param>
	/// <param name="checkpointCount">The number of checkpoints in the map (EXCLUDING the entrance & exit)</param>
	public Map(string name, string[,] layout, int checkpointCount)
	{
		this.name = name;
		this.layout = layout;
		this.checkpointCount = checkpointCount;
	}
	#endregion

	#region Methods
	#endregion
}
