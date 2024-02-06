using Interfaces;
using Managers;
using Model;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CharacterStats))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private StatId _speedStat;
        [SerializeField] private float _speedMultiplier = 0.01f;
        [SerializeField] private float _acceleration = 10;
        [SerializeField] private float _angularAcceleration = 10;
    
        [SerializeField]
        private Vector2 _input;
        private Rigidbody _body;
        private Vector3 _targetVelocity;
        private CharacterStats _stats;
        private IEntityStat _speed;
        private Vector2 _direction;
        private Vector3 _velocity;

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _stats = GetComponent<CharacterStats>();
            var forward = transform.forward;
            _direction = new Vector2(forward.x, forward.z);
        }

        private void Start()
        {
            _speed = _stats.Get(_speedStat);
        }

        public void MoveInput(Vector2 value)
        {
            _input = value;
        }

        public void LookDirection(Vector2 value)
        {
            _direction = value;
        
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.Pause)
            {
                _body.velocity = Vector3.zero;//TODO better pause handling or different character movement approach
                _input = Vector2.zero;
                return;
            }
            _targetVelocity = new Vector3(_input.x, 0, _input.y) * (_speed.Value * _speedMultiplier);
            _input = Vector2.zero;
        
            _velocity = Vector3.Lerp(_velocity, _targetVelocity, _acceleration * Time.fixedTime);
            _body.velocity = _velocity;
        
            var targetRotation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.y), Vector3.up);
            _body.rotation = Quaternion.Slerp(_body.rotation, targetRotation, _angularAcceleration * Time.fixedTime);
        }
    }
}
