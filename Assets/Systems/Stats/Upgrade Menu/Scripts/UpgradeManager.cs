using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private UpgradeButton[] buttons;

    [SerializeField] private UpgradeType[] upgradeTypes;

    private void OnEnable()
    {
        RandomiseButtons();

        GameEvents.CALL_UPGRADE_STAT += Handle_CallUpgradeStat;
    }

    private void OnDisable()
    {
        GameEvents.CALL_UPGRADE_STAT -= Handle_CallUpgradeStat;
    }

    private void Handle_CallUpgradeStat(GameStats.STAT_NAME _stat, float _addValue)
    {
        GameStats.UpgradeStat(_stat, _addValue);
        GameStateManager.instance.SetActiveState(GameStateHolder.GameState.SEARCHING_FOR_SIGNAL);
    }

    private void RandomiseButtons()
    {
        foreach (var button in buttons)
        {
            button.SetUpgradeType(upgradeTypes[Random.Range(0, buttons.Length)]);
        }
    }
}
