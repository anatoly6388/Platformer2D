using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private AreaPatrol _prefab;
    [SerializeField] private Player _player;
    [SerializeField] Transform _wayPoint;
    
    private float _delay = 10f;
    private WaitForSeconds _wait;
    private bool _isLive = false;
    private Enemy _enemy;

    private void Start()
    {
        _wait=new WaitForSeconds(_delay);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            _isLive = (_enemy != null);

            if (_isLive==false)
            {
                _isLive = true;
                AreaPatrol enemy = Instantiate(_prefab, transform.position, Quaternion.identity);
                enemy.SetPlayer(_player, _wayPoint);
                _enemy=enemy.GetComponent<Enemy>();
            }

            yield return _wait;
        }
    }

    
}
