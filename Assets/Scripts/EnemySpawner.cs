using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyMover _prefab;
    [SerializeField] Transform _wayPoint;
    [SerializeField] private GoldCounter _counter;

    private Health _health;
    private float _maxHealth = 80f;
    private float _delay = 10f;
    private WaitForSeconds _wait;
    private bool _isDeath = true;

    private void Start()
    {
        _wait=new WaitForSeconds(_delay);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            _isDeath = _health == null;

            if (_isDeath)
            {
                _isDeath = false;
                EnemyMover enemy = Instantiate(_prefab, transform.position, Quaternion.identity);
                enemy.SetWaypoint(_wayPoint);
                _health = enemy.GetComponent<Health>();
                _health.SetGoldCounter(_counter);
                _health.SetMaxHealth(_maxHealth);
                _counter.UpdateEnemyHealth(_health.CurrentHealth);
            }

            yield return _wait;
        }
    }    
}
