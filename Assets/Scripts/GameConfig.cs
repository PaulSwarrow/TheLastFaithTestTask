using UnityEngine;

[CreateAssetMenu(menuName = "Game/Game Config")]
public class GameConfig : ScriptableObject
{
    [Tooltip("First Level Upgrade cost in coins")]
    public int BaseLevelCost;
    [Tooltip("Additional upgrade cost per level")]
    public int LevelCostMultiplier;
}