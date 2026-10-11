using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {
    public event Action Attack;

    [SerializeField] float attackRate = 1f;
    public bool IsInRange { get; private set; } = false;
    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }

        IsInRange = true;
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag != "Player") { return; }

        IsInRange = false;
    }

    private IEnumerator Start() {
        while (true) {
            if (IsInRange) {
                Attack?.Invoke();

                print("ATTACK");

                yield return new WaitForSeconds(attackRate);
            }
            else {
                yield return null;
            }
        }
    }
}
