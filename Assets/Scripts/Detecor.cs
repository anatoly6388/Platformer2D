
using UnityEngine;

public class Detecor : MonoBehaviour
{
    [SerializeField] private EnemyMover _enemyMover;

    private Transform _target = null;
    private float _distance;
    private float _attackDistance = 2f;
    private bool _isFollow=false;
    private bool _isPatrol=true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _target = player.transform;
            _distance = Vector2.Distance(transform.position, _target.transform.position);

            if (_distance > _attackDistance)
            {
                _isPatrol = false;
                _isFollow = true;
            }
            else
            {
                _isFollow = false;
                _isPatrol = false;
            }

            _enemyMover.SetDirection(_isFollow, _isPatrol, _target);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _target = null;
            _isFollow = false;
            _isPatrol = true;
            _enemyMover.SetDirection(_isFollow, _isPatrol, _target);
        }
    }
}
