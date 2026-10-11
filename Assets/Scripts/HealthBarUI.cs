using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour {
    [SerializeField] private Health health;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image damageBar;
    [SerializeField] private float damageBarDelayBeforeLerp = 0.1f;
    [SerializeField] private float damageBarLerpDuration = 0.1f;

    private Coroutine lerpingDamageBarFillAmount;

    private float targetAmount;

    private void OnEnable() {
        health.OnHealthChange += UpdateHealthBarFill;
    }
    private void OnDisable() {
        health.OnHealthChange -= UpdateHealthBarFill;
    }
    private void Start() {
        damageBar.fillAmount = healthBar.fillAmount;
    }
    private void UpdateHealthBarFill(int currentHealth, int maxHealth) {
        if (lerpingDamageBarFillAmount != null) {
            StopCoroutine(lerpingDamageBarFillAmount);
        }

        targetAmount = (float) currentHealth / maxHealth;

        healthBar.fillAmount = targetAmount;

        lerpingDamageBarFillAmount = StartCoroutine(LerpDamageBarFillAmount(targetAmount));
    }
    private IEnumerator LerpDamageBarFillAmount(float targetAmount) {
        float startAmount = damageBar.fillAmount;
        float elapsedTime = 0f;

        yield return new WaitForSeconds(damageBarDelayBeforeLerp);

        while (elapsedTime < damageBarLerpDuration) {
            elapsedTime += Time.deltaTime;

            damageBar.fillAmount = Mathf.Lerp(startAmount, targetAmount, elapsedTime / damageBarLerpDuration);

            yield return null;
        }

        damageBar.fillAmount = targetAmount;
    }
}
