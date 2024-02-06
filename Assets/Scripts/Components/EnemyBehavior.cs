using System.Collections;
using Interfaces;
using Managers;
using UnityEngine;

namespace Components
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
                yield return GameManager.Instance.WaitForGameSeconds(Random.Range(1, 5));
                _attack?.Attack();
                yield return GameManager.Instance.WaitForGameSeconds(Random.Range(1, 2));
            }
        }
        
    }
}