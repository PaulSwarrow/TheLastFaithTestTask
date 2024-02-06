using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

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

        private bool _pause;
        public float GameTime { get; private set; }
        public float GameDeltaTime => _pause ? 0 : Time.deltaTime;
        public bool Pause => _pause;

        [SerializeField] private GameConfig _config;
        [SerializeField] private GameObject _pauseMenu;

        private void Awake()
        {
            _instance = this;
            UpdatePauseState();
        }

        private void Update()
        {
            if (!_pause)
                GameTime += Time.deltaTime;
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _pause = !_pause;
                UpdatePauseState();
            }
        }

        private void UpdatePauseState()
        {
            _pauseMenu.SetActive(_pause);
        }

        public int GetUpgradeCost(int currentLevel)
        {
            return _config.BaseLevelCost + currentLevel * _config.LevelCostMultiplier;
        }

        public IEnumerator WaitForGameSeconds(float seconds)
        {
            var a = GameTime;
            yield return new WaitUntil(() => GameManager.Instance.GameTime - a >= seconds);
        }
    }
}