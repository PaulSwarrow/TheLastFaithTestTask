using System.Collections;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Main game manager.
    /// Implements play|pause behavior and UI calls
    /// TODO: dependency injection
    /// TODO: split behavior to avoid god-object
    /// </summary>
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

        private bool _gameOver;
        private bool _menu;
        public float GameTime { get; private set; }
        public float GameDeltaTime => Pause ? 0 : Time.deltaTime;
        public bool Pause => _menu || _gameOver;

        [SerializeField] private GameConfig _config;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _gameoverScreen;

        [SerializeField] private CharacterEntity _player;

        private void Awake()
        {
            _instance = this;
            UpdateMenuState();
            _gameoverScreen.SetActive(false);
            _player.DeathEvent += OnPlayerDie;
        }

        private void Update()
        {
            if (!Pause)
                GameTime += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Escape) && !_gameOver)
            {
                _menu = !_menu;
                UpdateMenuState();
            }
        }

        private void UpdateMenuState()
        {
            _pauseMenu.SetActive(_menu);
        }

        private void OnPlayerDie(CharacterEntity obj)
        {
            _gameOver = true;
            _gameoverScreen.SetActive(true);
        }

        public int GetUpgradeCost(int currentLevel)
        {
            return _config.BaseLevelCost + currentLevel * _config.LevelCostMultiplier;
        }

        public IEnumerator WaitForGameSeconds(float seconds)
        {
            var a = GameTime;
            yield return new WaitUntil(() => Instance.GameTime - a >= seconds);
        }
    }
}