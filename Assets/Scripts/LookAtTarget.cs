using UnityEngine;

public class LookAtTarget : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private int rotationSpeed = 1;
    [SerializeField] private float deadzoneRadius = 1;

    private void Update() {
        if (target == null) { return; }

        Vector3 selfToTargetDirection = target.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(selfToTargetDirection);

        float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);

        if (angleDifference < deadzoneRadius) { return; }

        transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, Time.deltaTime * rotationSpeed );
    }

    public void SetTarget(Transform lookAtTarget) {
        target = lookAtTarget;
    }
}