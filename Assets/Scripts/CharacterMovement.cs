using DefaultNamespace;
using DefaultNamespace.Model;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterStats))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _baseSpeed = 10;
    [SerializeField] private float _dexterityMultiplier = 1;
    [SerializeField] private float _acceleration = 10;
    [SerializeField] private float _angularAcceleration = 10;
    
    [SerializeField]
    private Vector2 _input;
    private Rigidbody _body;
    private Vector3 _targetVelocity;
    private CharacterStats _stats;
    private ICharacterStat _dexterity;
    private Vector2 _direction;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _stats = GetComponent<CharacterStats>();
        var forward = transform.forward;
        _direction = new Vector2(forward.x, forward.z);
    }

    private void Start()
    {
        _dexterity = _stats.Get(StatId.Dexterity);
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
        _targetVelocity = new Vector3(_input.x, 0, _input.y) * (_baseSpeed + _dexterityMultiplier * _dexterity.CurrentValue);
        _input = Vector2.zero;
        _body.velocity = Vector3.Lerp(_body.velocity, _targetVelocity, _acceleration * Time.fixedTime);
        
        var targetRotation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.y), Vector3.up);
        _body.rotation = Quaternion.Slerp(_body.rotation, targetRotation, _angularAcceleration * Time.fixedTime);
    }
}
