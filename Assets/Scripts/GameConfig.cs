using UnityEngine;

[CreateAssetMenu(menuName = "Game/Game Config")]
public class GameConfig : ScriptableObject
{
    public int BaseLevelCost;
    public int LevelCostMultiplier;
}