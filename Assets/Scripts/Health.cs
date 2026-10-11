using UnityEngine;
using System;
using System.Collections;

public class Health : MonoBehaviour {
    public event Action<int, int> OnHealthChange;
    public event Action OnDeath;

    [field: SerializeField] public bool IsInvulnerable { get; private set; } = false;

    [field: SerializeField] public int CurrentHealth { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; } = 100;
    [field: SerializeField] public int RegenAmount { get; private set; } = 1;
    [field: SerializeField] public float RegenRate { get; private set; } = 0.5f;
    [field: SerializeField] public int TimeUntilRegen { get; private set; } = 5;

    private Coroutine regenCoroutine;
    private Coroutine invincibilityCoroutine;

    public void EnableInvincibility(int duration) {
        if (invincibilityCoroutine != null) {
            StopCoroutine(invincibilityCoroutine);
        }

        invincibilityCoroutine = StartCoroutine(RunInvincibilityFrames(duration));
    }
    private IEnumerator RunInvincibilityFrames(int duration) {
        IsInvulnerable = true;

        yield return new WaitForSeconds(duration);

        IsInvulnerable = false;
    }

    private void Start() {
        CurrentHealth = PlayerStateManager.Instance.CurrentHealth;
        OnHealthChange?.Invoke(CurrentHealth, MaxHealth);

        print(CurrentHealth);

        regenCoroutine = StartCoroutine(RegenerateHealth());
    }
    public void TakeDamage(int damageAmount) {
        if (IsInvulnerable) { return; }

        CurrentHealth -= damageAmount;

        Mathf.Max(CurrentHealth, 0);

        if (CurrentHealth <= 0) {
            OnDeath?.Invoke();
        }   

        OnHealthChange?.Invoke(CurrentHealth, MaxHealth);

        PlayerStateManager.Instance.UpdateCurrentHealth(CurrentHealth);

        if (regenCoroutine != null) {
            StopCoroutine(regenCoroutine);
        }

        regenCoroutine = StartCoroutine(RegenerateHealth());
    }

    private IEnumerator RegenerateHealth() {
        yield return new WaitForSeconds(TimeUntilRegen);

        while (CurrentHealth < MaxHealth) {
            CurrentHealth = Math.Min(MaxHealth, CurrentHealth + RegenAmount);


            OnHealthChange?.Invoke(CurrentHealth, MaxHealth);

            PlayerStateManager.Instance.UpdateCurrentHealth(CurrentHealth);

            yield return new WaitForSeconds(RegenRate);
        }
    }
}