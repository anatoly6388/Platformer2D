using UnityEngine;
using UnityEngine.UI;

public class GoldCounter : MonoBehaviour
{
    [SerializeField] private Text _textTotalGold;
    [SerializeField] private Text _textHealth;
    [SerializeField] private Text _textEnemyHealth;

    private int _totalGold = 0;

    private void Start()
    {
        TranslateToString();
    }

    public void UpdateCount()
    {
        _totalGold++;
        TranslateToString();
    }

    public void UpdatePlayerHealth(float health)
    {
        _textHealth.text = health.ToString();
    }

    public void UpdateEnemyHealth(float health)
    {
        _textEnemyHealth.text = health.ToString();
    }

    private void TranslateToString()
    {
        _textTotalGold.text = _totalGold.ToString();
    }
}
