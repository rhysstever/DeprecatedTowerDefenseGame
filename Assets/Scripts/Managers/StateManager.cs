using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    mainMenu,
    game,
    gameOver
}

public class StateManager : MonoBehaviour
{
    public MenuState currentMenuState;
    
    // Start is called before the first frame update
    void Start()
    {
        ChangeMenuState(MenuState.game);
    }

    // Update is called once per frame
    void Update()
    {
        // Recurring logic that runs dependant on the current MenuState
        switch(currentMenuState) {
            case MenuState.mainMenu:
                break;
            case MenuState.game:
                break;
            case MenuState.gameOver:
                break;
        }
    }

    // Initial, one-time logic that happen when the MenuState first changed
	public void ChangeMenuState(MenuState newMenuState)
    {
        currentMenuState = newMenuState;

        switch(newMenuState) {
            case MenuState.mainMenu:
                break;
            case MenuState.game:
                break;
            case MenuState.gameOver:
                break;
        }
	}
}
