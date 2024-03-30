
using UnityEngine;

public class Health : MonoBehaviour
{
    readonly private string Player = "Player";

    private float _maxHealth;   
    private GoldCounter _counter;

    public float CurrentHealth { get; private set; }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (gameObject.tag == Player)
        {
            _counter.UpdatePlayerHealth(CurrentHealth);
        }
        else
        {
            _counter.UpdateEnemyHealth(CurrentHealth);
        }

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(float healItem)
    {
        CurrentHealth += healItem;

        if (CurrentHealth > _maxHealth)
        {
            CurrentHealth = _maxHealth;
        }
    }

    public void SetGoldCounter(GoldCounter counter)
    {
        _counter = counter;
    }

    public void SetMaxHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
        CurrentHealth = _maxHealth;
    }
}
