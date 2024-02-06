using System;
using DefaultNamespace.Model;
using Managers;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CharacterMovement))]
    public class CharacterInput : MonoBehaviour
    {
        private CharacterMovement _movement;
        private IAttackBehavior _attack;

        private void Awake()
        {
            _movement = GetComponent<CharacterMovement>();
            _attack = GetComponent<IAttackBehavior>();
        }

        private void Update()
        {
            if(GameManager.Instance.Pause) return;
            var move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _movement.MoveInput(move);

            var cam = Camera.main;
            var mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = cam.transform.position.y;
            var mouseWorldPosition = cam.ScreenToWorldPoint(mouseScreenPosition);
            var direction = mouseWorldPosition - transform.position;
            _movement.LookDirection(new Vector2(direction.x, direction.z));

            if (Input.GetButton("Fire1"))
            {
                _attack?.Attack();
            }
            
        }
    }
}