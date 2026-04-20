using System.Collections.Generic;

[System.Serializable]
public class GameStateHolder
{
    public enum GameState
    {
        NULL,
        SEARCHING_FOR_SIGNAL,
        FOUND_SIGNAL,
        UPGRADING,
        MAIN_MENU,
        INTRO,
        GAME_OVER
    }

    public Dictionary<GameState, GameState_Base> stateDictionary = new Dictionary<GameState, GameState_Base>();

    public GameState_SearchingForSignal state_searchingForSignal = new GameState_SearchingForSignal();
    public GameState_FoundSignal state_foundSignal = new GameState_FoundSignal();
    public GameState_Upgrading state_Upgrading = new GameState_Upgrading();
    public GameState_MainMenu state_mainMenu = new GameState_MainMenu();
    public GameState_Intro state_Intro = new GameState_Intro();
    public GameState_GameOver state_gameOver = new GameState_GameOver();

    public GameStateHolder()
    {
        stateDictionary[GameState.SEARCHING_FOR_SIGNAL] = state_searchingForSignal;
        stateDictionary[GameState.FOUND_SIGNAL] = state_foundSignal;
        stateDictionary[GameState.UPGRADING] = state_Upgrading;
        stateDictionary[GameState.MAIN_MENU] = state_mainMenu;
        stateDictionary[GameState.INTRO] = state_Intro;
        stateDictionary[GameState.GAME_OVER] = state_gameOver;
    }
}
