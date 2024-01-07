using System;
using System.Collections;
using UnityEngine;

public class HealthSystem : BaseStatSystem, IDamageable
{
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioSource audioSource;


    public int MaxHealth => maxStat;
    public int CurrentHealth => currentStat;

    private void Start()
    {
        currentStat = maxStat;
        OnStatChangedEvent();
    }

    public void TakeDamage(int damage)
    {
        currentStat -= damage;
        audioSource.PlayOneShot(damageSound);
        StartCoroutine(FlashSprite());
        OnStatChangedEvent();
    }

    private IEnumerator FlashSprite()
    {
        int flashes = 0;
        while (flashes < 3)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
            flashes++;
        }
    }

    public void AddHealth(int health)
    {
        currentStat += health;
        OnStatChangedEvent();
    }
}