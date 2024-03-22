using UnityEngine;
using UnityEngine.UI;

public class GoldCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _textTotalGold;
    [SerializeField] private Text _textHealth;

    private int _TotalGold = 0;
    private float _health = 0;

    private void OnEnable()
    {
        _player.CollectGold += UpdateCount;
    }

    private void Start()
    {
        TranslateToString();
    }

    private void Update()
    {
        _health=_player.GetComponent<PlayerHealth>().CurrentHealth;
        _textHealth.text = _health.ToString();
    }

    private void OnDisable()
    {
        _player.CollectGold -= UpdateCount;
    }

    private void AddOneGold()
    {
        _TotalGold++;
    }

    private void TranslateToString()
    {
        _textTotalGold.text = _TotalGold.ToString();
    }

    private void UpdateCount()
    {
        AddOneGold();
        TranslateToString();
    }
}
