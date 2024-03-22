using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _maxHealth = 50f;
    private float _currentHealth;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _animator.SetTrigger("Death");
        Destroy(gameObject);
    }
}
