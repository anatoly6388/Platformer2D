using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private GoldCounter _counter;

    private float _healItem = 30f;
    private float _maxHealth = 100f;

    public event Action CollectGold;
    public event Action Heal;

    private void Start()
    { 
        _health.SetGoldCounter(_counter);
        _health.SetMaxHealth(_maxHealth);
        _counter.UpdatePlayerHealth(_health.CurrentHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Gold gold))
        {
            gold.Take();
            CollectGold?.Invoke();
            _counter.UpdateCount();
        }

        if(other.TryGetComponent(out Hearth hearth))
        {
            hearth.Take();
            _health.Heal(_healItem);
            Heal?.Invoke();
            _counter.UpdatePlayerHealth(_health.CurrentHealth);
        }
    }
}
