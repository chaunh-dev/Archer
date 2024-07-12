using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] CharacterHealthBar healthBar;
    [SerializeField] int amount;
    [SerializeField] int maxHealth;

    public int MaxHealth { get => maxHealth; }
    public int Amount { get => amount; set => amount = Amount; }

    public void SetHealthBar(CharacterHealthBar _healthBar)
    {
        healthBar = _healthBar;
    }

    public void Init(int maxHealth)
    {
        amount = maxHealth;
        healthBar.SetHealthTxt(amount);
        this.maxHealth = maxHealth;
        healthBar.SetHealthBarVisual((float)amount / maxHealth);
    }

    public void TakeDamage(int damage)
    {
        amount -= damage;
        amount = Mathf.Clamp(amount, 0, MaxHealth);
        healthBar.SetHealthTxt(amount);
        healthBar.SetHealthBarVisual((float)amount / MaxHealth);
    }

    public void HealthRegen(int health, bool revise = false)
    {
        if (revise)
            amount = MaxHealth;
        else
            amount += health;
        amount = Mathf.Clamp(amount, 0, MaxHealth);
        healthBar.SetHealthTxt(amount);
        healthBar.SetHealthBarVisual((float)amount / MaxHealth);
    }

    public bool IsDead()
    {
        return amount <= 0;
    }

    public bool IsFull()
    {
        return amount == MaxHealth;
    }
}
