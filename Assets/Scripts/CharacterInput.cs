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
        }
    }
}