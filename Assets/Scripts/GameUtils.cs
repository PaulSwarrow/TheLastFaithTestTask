using Interfaces;
using UnityEngine;

/// <summary>
/// Accumulates common approaches for game logic operations
/// </summary>
public static class GameUtils
{
    /// <summary>
    /// Retrieves a gameEntity from a collider. Encapsulates objects and scene hierarchy agreement
    /// </summary>
    /// <param name="collider">provided collider</param>
    /// <param name="entity">retrieved entity</param>
    /// <returns>is entity was found</returns>
    public static bool GetEntity(Collider collider, out IGameEntity entity)
    {
        //Search for the effect targert upwards. Supports complex target hierarchies.
        //Assumption: targets are not nested in the game.
        //Otherwise should be som restriction on collider - StatsComponent relations
        entity = collider.GetComponentInParent<IGameEntity>();
        return entity != null;
    }

}