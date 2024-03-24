
using UnityEngine;
[RequireComponent (typeof(Animator))]
public class EnemyAttacker : MonoBehaviour
{
    public readonly int Attack = Animator.StringToHash(nameof(Attack));

    [SerializeField] private LayerMask _playerLayers;
    [SerializeField] private Transform _attackPoint;

    private Animator _animator;
    private float _attackRange = 0.5f;
    private float _attackPower = 10f;
    private float _delay = 1f;
    private float _repeatRate = 1f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        InvokeRepeating(nameof(Fight), _delay, _repeatRate);
    }


    private void Fight()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _playerLayers);

        foreach (Collider2D player in players)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(_attackPower);
            _animator.SetTrigger(Attack);
        }
    }
}
