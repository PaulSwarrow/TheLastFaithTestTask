using DefaultNamespace;
using UI;
using UnityEngine;
using UnityEngine.Assertions;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        //TODO remove singleton, use Dependency Injection instead
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameConfig _config;
        private void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
        }
        

        public int GetUpgradeCost(int currentLevel)
        {
            return _config.BaseLevelCost + currentLevel * _config.LevelCostMultiplier;
        }
    }
}