using UnityEngine;
using System.Collections;
using System;

public class Entity : MonoBehaviour, IDamageable {

    public float startingHealth;
    public float startingDamage;

    protected float health;
    protected float damage;
    protected bool isDead;

    public event System.Action OnDeath;
    public event System.Action OnHit;

    protected virtual void Start()
    {
        health = startingHealth;
        damage = startingDamage;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    protected void Die()
    {
        isDead = true;

        if(OnDeath != null){
            OnDeath();
        }

        GameObject.Destroy(gameObject);
    }
}
