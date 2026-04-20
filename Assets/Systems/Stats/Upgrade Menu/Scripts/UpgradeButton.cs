using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private TMP_Text statNameText;
    [SerializeField] private TMP_Text statValueText;

    private UpgradeType upgradeType;

    public void BuyUpgrade()
    {
        GameEvents.CALL_UPGRADE_STAT?.Invoke(upgradeType.statName, upgradeType.increaseValue);
    }

    public void SetUpgradeType(UpgradeType _upgradeType)
    {
        upgradeType = _upgradeType;
        upgradeType.RandomiseIncreaseValue();

        SetText();
    }

    private void SetText()
    {
        statNameText.text = upgradeType.statName.ToString().Replace("_", " ");

        float currentValue = GameStats.GetStat(upgradeType.statName);
        statValueText.text = $"{currentValue}% > {currentValue + upgradeType.increaseValue}%";
    }
}
