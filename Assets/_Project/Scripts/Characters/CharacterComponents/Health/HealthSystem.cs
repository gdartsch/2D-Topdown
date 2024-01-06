using System;
using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    public event System.Action<int, int> OnHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        audioSource.PlayOneShot(damageSound);
        StartCoroutine(FlashSprite());
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
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
}