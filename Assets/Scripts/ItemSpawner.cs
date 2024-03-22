
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Gold _goldPrefab;
    [SerializeField] private Hearth _hearthPrefab;
    [SerializeField] private Transform _itemSpawnPoints;
    [SerializeField] private Player _player;

    private Transform[] _spawnPoints;
    private int _items = 0;

    private int _itemVarieble = 4;

    private void Start()
    {
        _spawnPoints = new Transform[_itemSpawnPoints.childCount];

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i]= _itemSpawnPoints.GetChild(i).transform;
        }
    }
    private void OnEnable()
    {
        _player.Heal += TakeItem;
        _player.CollectGold += TakeItem;
    }

    private void OnDisable()
    {
        _player.Heal -= TakeItem;
        _player.CollectGold -= TakeItem;
    }

    private void TakeItem()
    {
        _items--;
    }

    private void Update()
    {
        if (_items==0)
            SpawnItem();
    }

    private void SpawnItem()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            int index = Random.Range(0, _itemVarieble);

            if (index == 0)
                Instantiate(_hearthPrefab, _spawnPoints[i].position, _spawnPoints[i].rotation);
            else
                Instantiate(_goldPrefab, _spawnPoints[i].position, _spawnPoints[i].rotation);
        }

        _items = _spawnPoints.Length;
    }
}
