using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;
    float maxHealth = 100;
    public float mana;
    float maxMana = 100;
    [SerializeField] Bar healthBar;
    [SerializeField] Bar manaBar;

    void Start()
    {
        health = maxHealth;
        mana = maxMana;
    }

    void Update()
    {
        healthBar.UpdateBar(health, maxHealth);
        manaBar.UpdateBar(mana, maxMana);
    }
}
