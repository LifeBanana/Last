using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    float health;
    float shield;

    Respawn respawn;

    public float maxHealth;
    public float maxShield;

    public TMP_Text healthtext;

    void InitializeHealth()
    {
        string className = SaveManager.Instance.Data.className;

        switch (className)
        {
            case "Assault":
                maxHealth = 100;
                maxShield = 50;
                break;

            case "Heavy":
                maxHealth = 200;
                maxShield = 100;
                break;

            case "Recon":
                maxHealth = 75;
                maxShield = 25;
                break;

            case "Engineer":
                maxHealth = 125;
                maxShield = 50;
                break;

            case "Support":
                maxHealth = 125;
                maxShield = 75;
                break;

            case "Scout":
                maxHealth = 50;
                maxShield = 50;
                break;

            case "Driver":
                maxHealth = 225;
                maxShield = 175;
                break;

            case "Demolitionist":
                maxHealth = 175;
                maxShield = 125;
                break;

            default:
                maxHealth = 100;
                maxShield = 50;
                break;
        }

        health = maxHealth;
        shield = maxShield;

        Debug.Log($"Class: {className} | Health: {maxHealth} | Shield: {maxShield}");
    }

    IEnumerator Start()
    {
        while (string.IsNullOrEmpty(SaveManager.Instance.Data.className))
            yield return null;

        InitializeHealth();

        respawn = GetComponent<Respawn>();

        RefreshHealth();
    }

    private void Update()
    {
        healthtext.text = "Health: " + health + " / " + maxHealth + "\n" + "Shields: " + shield + " / " + maxShield;
    }

    public void TakeDamage(float damage)
    {
        if (shield > 0)
        {
            float absorbed =  Mathf.Min(shield, damage);

            shield -= absorbed;
            damage -= absorbed;
        }

        health -= damage;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        respawn.respawn();
    }

    public void RefreshHealth()
    {
        InitializeHealth();

        health = maxHealth;
        shield = maxShield;
    }
}
