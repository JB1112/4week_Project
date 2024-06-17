using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    public int health;
    public event Action OnDie;
    public event Action OnDamage;
    public event Action OnHeal;

    public bool IsDead => health == 0;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (health == 0) return;

        health = Mathf.Max(health - damage, 0);

        if (health == 0) OnDie?.Invoke();

        Debug.Log(health);
    }

    public void Heal(int heal)
    {
        if (health == 0) return;

        health = Mathf.Min(health + heal, maxHealth);
    }
}