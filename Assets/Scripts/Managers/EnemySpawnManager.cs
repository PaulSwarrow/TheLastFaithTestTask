using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [SerializeField] private CharacterEntity _prefab;
        [SerializeField] private int _maxEnemies;
        [SerializeField] private Bounds _bounds;

        private int _enemyCount;
        
        private void Start()
        {
            StartCoroutine(SpawnCoroutine());


        }

        private IEnumerator SpawnCoroutine()
        {
            for (int i = 0; i < _maxEnemies; i++)
            {
                Spawn();
            }
            while (true)
            {
                if (_enemyCount < _maxEnemies)
                {
                    Spawn();
                }
                yield return GameManager.Instance.WaitForGameSeconds(Random.Range(1, 2));
            }
            
        }

        private void Spawn()
        {
            var npc = Instantiate(_prefab);
            var x = Random.Range(-_bounds.extents.x, _bounds.extents.x) + _bounds.center.x; 
            var z = Random.Range(-_bounds.extents.z, _bounds.extents.z) + _bounds.center.z;
            npc.transform.position = new Vector3(x, 0, z);
            npc.transform.rotation = Quaternion.Euler(0,Random.value * 360, 0);
            _enemyCount++;
            npc.DeathEvent += OnEnemyDied;

        }

        private void OnEnemyDied(CharacterEntity character)
        {
            character.DeathEvent -= OnEnemyDied;
            _enemyCount--;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(_bounds.center, _bounds.extents * 2);
        }
    }
}