using System;
using System.Collections;
using DefaultNamespace.Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CharacterMovement))]
    public class EnemyBehavior : MonoBehaviour
    {
        private CharacterMovement _move;
        private IAttackBehavior _attack;

        private void Awake()
        {
            _move = GetComponent<CharacterMovement>();
            _attack = GetComponent<IAttackBehavior>();
            StartCoroutine(Behavior());
        }

        private void Update()
        {
            
        }

        private IEnumerator Behavior()
        {
            while (true)
            { 
                float angle = Random.Range(0f, Mathf.PI * 2);
                Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                _move.LookDirection(direction);
                yield return new WaitForSeconds(Random.Range(1, 5));
                _attack?.Attack();
                yield return new WaitForSeconds(Random.Range(1, 2));

            }
        }
    }
}