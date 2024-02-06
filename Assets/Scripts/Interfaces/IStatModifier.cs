using UnityEngine;

namespace Interfaces
{
    public interface IStatModifier
    {
        int GetModificator(int baseValue);

        Color Color { get; }
    }
}