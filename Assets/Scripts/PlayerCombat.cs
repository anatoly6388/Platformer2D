
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public readonly int Attack = Animator.StringToHash(nameof(Attack));

    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private Transform _attackPoint;

    private Animator _animator;
    private float _attackRange = 0.4f;
    private float _attackPower = 10f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Fight();
        }
    }

    private void Fight()
    {
        _animator.SetTrigger(Attack);
        Collider2D[] enemys = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        foreach (Collider2D enemy in enemys)
        {
            enemy.GetComponent<Health>().TakeDamage(_attackPower);
        }
    }
}
