using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// Modifier for some stat. May be added and removed safely
    /// </summary>
    public interface IStatModifier
    {
        /// <summary>
        /// Calculate additive value based on base stat.
        /// Modifiers don't interact with eachother
        /// </summary>
        /// <param name="baseValue">original stat value</param>
        /// <returns>amount to add</returns>
        int GetModificator(int baseValue);

        /// <summary>
        /// Color to mark modifier presence (for UI)
        /// White if not needed
        /// </summary>
        Color Color { get; }
    }
}