using UnityEngine;

[System.Serializable]
public class GameState_MainMenu : GameState_Base
{
    public override void OnStateEnter()
    {
        base.OnStateEnter();

        GameStats.ResetStats();
    }
}
