using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CharacterMovement))]
    public class CharacterInput : MonoBehaviour
    {
        private CharacterMovement _movement;

        private void Awake()
        {
            _movement = GetComponent<CharacterMovement>();
        }

        private void Update()
        {
            var move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _movement.MoveInput(move);

            var cam = Camera.main;
            var mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = cam.transform.position.y;
            var mouseWorldPosition = cam.ScreenToWorldPoint(mouseScreenPosition);
            var direction = mouseWorldPosition - transform.position;
            
            _movement.LookDirection(new Vector2(direction.x, direction.z));
            
            
        }
    }
}