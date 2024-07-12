using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMana : MonoBehaviour
{
    [SerializeField] CharacterManaBar manaBar;
    [SerializeField] int amount;
    [SerializeField] int maxMana;

    public void Init(int _maxMana)
    {
        amount = _maxMana;
        manaBar.SetManaTxt(amount);
        this.maxMana = _maxMana;
        manaBar.SetManaBarVisual((float)amount / _maxMana);
    }

    public void ConsumeMana(int _manaUsed)
    {
        amount -= _manaUsed;
        amount = Mathf.Clamp(amount, 0, maxMana);
        manaBar.SetManaTxt(amount);
        manaBar.SetManaBarVisual((float)amount / maxMana);
    }

    public void ManaRegen(int manaPoint)
    {
        amount += manaPoint;
        amount = Mathf.Clamp(amount, 0, maxMana);
        manaBar.SetManaTxt(amount);
        manaBar.SetManaBarVisual((float)amount / maxMana);
    }

    public bool IsOutMana()
    {
        return amount <= 0;
    }

    public bool IsFull()
    {
        return amount == maxMana;
    }
}
