using UnityEngine;

[System.Serializable]
public struct UpgradeType
{
    public GameStats.STAT_NAME statName;
    public int increaseValueMin;
    public int increaseValueMax;

    [HideInInspector] public int increaseValue;

    public void RandomiseIncreaseValue()
    {
        increaseValue = Random.Range(increaseValueMin, increaseValueMax);
    }
}
