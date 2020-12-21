using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int health;
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		// Changes the menu state to Game Over if the player loses all health
		if(health <= 0)
			gameObject.GetComponent<StateManager>().ChangeMenuState(MenuState.gameOver);
    }

	/// <summary>
	/// Deals damage to the player
	/// </summary>
	/// <param name="damage">The damage the player is taking</param>
	public void TakeDamage(int damage) {
		health -= damage;
	}
}
