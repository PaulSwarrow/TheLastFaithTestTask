using System;
using Components;
using Interfaces;
using Model;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(StatusHandlerComponent))]
public class CharacterEntity : MonoBehaviour, IGameEntity
{
    [SerializeField] //TODO move pick up feature to a dedicated component 
    private bool _canPickUp;

    [SerializeField] //TODO: characters lifecycle system, pools etc
    private bool _destroyOnDeath;

    [SerializeField] private int _team;

    [SerializeField] //TODO move kill rewards to a dedicated component
    private int _killReward;

    public event Action<CharacterEntity> DeathEvent;
    private CharacterStats _stats;
    private StatusHandlerComponent _statusHandler;

    private void Awake()
    {
        _stats = GetComponent<CharacterStats>();
        _statusHandler = GetComponent<StatusHandlerComponent>();
    }

    private void OnEnable()
    {
        _stats.Subscribe(StatId.Health, OnHealthChange);
    }

    private void OnDisable()
    {
        _stats.Unsubscribe(StatId.Health, OnHealthChange);
    }

    public bool IsAlive => _stats.Get(StatId.Health).Value > 0;
    public bool CanPickUp => _canPickUp && IsAlive;

    public int Team => _team;
    public int KillReward => _killReward;

    public void ClaimKill(IGameEntity target)
    {
        if (target.Team != _team)
        {
            //TODO create abstract kill rewards here
            _stats.ChangeValue(StatId.Coins, target.KillReward);
        }
    }

    public void ReceiveDamage(int amount, IGameEntity from)
    {
        _stats.ChangeValue(StatId.Health, -amount);
        if (!IsAlive && from != null)
        {
            //todo: provide info
            from.ClaimKill(this);
        }
    }

    public IEntityStat GetStat(StatId id)
    {
        return _stats.Get(id);
    }

    public void ApplyEffect(IEntityEffect effect)
    {
        effect.Apply(_stats, _statusHandler);
    }

    private void OnHealthChange(StatId id, int oldvalue, int newvalue)
    {
        if (newvalue <= 0)
        {
            DeathEvent?.Invoke(this);
            if (_destroyOnDeath)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}