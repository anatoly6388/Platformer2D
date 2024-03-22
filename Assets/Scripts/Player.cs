using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action CollectGold;
    public event Action Heal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Gold gold))
        {
            gold.Take();
            CollectGold?.Invoke();
        }

        if(other.TryGetComponent(out Hearth hearth))
        {
            hearth.Take();
            Heal?.Invoke();
        }
    }
}
