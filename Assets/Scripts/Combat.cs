using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {
    public event Action Attack;

    [SerializeField] float attackRate = 1f;
    public bool IsInRange { get; private set; } = false;

    private GameObject target;
    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }

        target = other.gameObject;

        print(target.name);

        IsInRange = true;
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag != "Player") { return; }

        target = null;

        IsInRange = false;
    }

    private IEnumerator Start() {
        while (true) {
            if (IsInRange) {
                Attack?.Invoke();

                Health targetHealth = target.GetComponentInParent<Health>();
                if (targetHealth == null) { continue; }

                int attackAmount = 15 + PlayerStateManager.Instance.Level;

                targetHealth.TakeDamage(attackAmount);

                yield return new WaitForSeconds(attackRate);
            }
            else {
                yield return null;
            }
        }
    }
}
