using UnityEngine;

[System.Serializable]
public class GameState_SearchingForSignal : GameState_Base
{
    public override void OnStateEnter()
    {
        base.OnStateEnter();

        GameEvents.ON_TIMER_HIT_0 += Handle_OnTimerHit0;
        GameEvents.ON_SIGNAL_FOUND += Handle_OnSignalFound;
        GameEvents.ON_HIT_BOMB += Handle_OnTimerHit0;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        GameEvents.ON_TIMER_HIT_0 -= Handle_OnTimerHit0;
        GameEvents.ON_SIGNAL_FOUND -= Handle_OnSignalFound;
        GameEvents.ON_HIT_BOMB -= Handle_OnTimerHit0;
    }

    private void Handle_OnTimerHit0()
    {
        GameStateManager.instance.SetActiveState(GameStateHolder.GameState.GAME_OVER);
    }

    private void Handle_OnSignalFound()
    {
        GameStateManager.instance.SetActiveState(GameStateHolder.GameState.FOUND_SIGNAL);
    }
}
