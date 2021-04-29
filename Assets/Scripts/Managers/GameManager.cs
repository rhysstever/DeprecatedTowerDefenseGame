using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    mainMenu,
    levelSelect,
    game,
    pause,
    gameOver
}

public class GameManager : MonoBehaviour
{
    // ===== Set in inspector =====
    public int health;
    public int money;

    // ===== Set at Start() =====
    public MenuState currentMenuState;

    // Start is called before the first frame update
    void Start()
    {
        ChangeMenuState(MenuState.mainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        // Recurring logic that runs dependant on the current MenuState
        switch(currentMenuState) {
            case MenuState.mainMenu:
                break;
            case MenuState.levelSelect:
                break;
            case MenuState.game:
                // ESC key pauses the game
                if(Input.GetKeyDown(KeyCode.Escape))
                    ChangeMenuState(MenuState.pause);
                // Changes the menu state to Game Over if the player loses all health
                if(health <= 0)
                    ChangeMenuState(MenuState.gameOver);
                break;
            case MenuState.pause:
                // ESC key unpauses the game
                if(Input.GetKeyDown(KeyCode.Escape))
                    ChangeMenuState(MenuState.game);
                break;
            case MenuState.gameOver:
                break;
        }
    }

    /// <summary>
    /// Initial, one-time logic that happen when the MenuState first changed
    /// </summary>
    /// <param name="newMenuState">The new state of the game</param>
	public void ChangeMenuState(MenuState newMenuState)
    {
        currentMenuState = newMenuState;
        gameObject.GetComponent<UIManager>().ActivateUI(newMenuState);

        switch(newMenuState) {
            case MenuState.mainMenu:
                break;
            case MenuState.levelSelect:
                break;
            case MenuState.game:
                break;
            case MenuState.pause:
                break;
            case MenuState.gameOver:
                break;
        }
	}
}
