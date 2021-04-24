using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    mainMenu,
    levelSelect,
    game,
    gameOver
}

public class StateManager : MonoBehaviour
{
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
                for(int i = 0; i < gameObject.GetComponent<LevelManager>().MapCount(); i++)
                    gameObject.GetComponent<UIManager>().CreateMapButton(i);
                break;
            case MenuState.game:
                break;
            case MenuState.gameOver:
                break;
        }
	}
}
