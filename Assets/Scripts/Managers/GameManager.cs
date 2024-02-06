using System;
using DefaultNamespace;
using UI;
using UnityEngine;
using UnityEngine.Assertions;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        //TODO remove singleton, use Dependency Injection instead
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                }

                return _instance;
            }
        }

        [SerializeField] private GameConfig _config;
        [SerializeField] private GameObject _pauseMenu;
        private void Awake()
        {
            _instance = this;
            _pauseMenu.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _pauseMenu.SetActive(!_pauseMenu.activeSelf);
            }
        }

        public int GetUpgradeCost(int currentLevel)
        {
            return _config.BaseLevelCost + currentLevel * _config.LevelCostMultiplier;
        }
    }
}