
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Player _player;

    private float _itemHeal = 30f;

    public float HealthMax { get; private set; } = 100f;
    public float CurrentHealth { get; private set; }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Heal()
    {
        CurrentHealth += _itemHeal;

        if (CurrentHealth >= HealthMax)
        {
            CurrentHealth = HealthMax;
        }
    }

    private void OnEnable()
    {
        _player.Heal += Heal;
    }

    private void Start()
    {
        CurrentHealth = HealthMax;
    }

    private void OnDisable()
    {
        _player.Heal -= Heal;
    }
}
