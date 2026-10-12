using System;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {
    public event Action Chase;

    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform lineOfSightPoint;

    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float chaseSpeed= 3f;

    public bool IsChasing { get; private set; } = false;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = transform.position;

        target = FindFirstObjectByType<CharacterController>().transform;
    }

    private void Update() {
        Vector3 agentToTargetVector = target.position - transform.position;

        bool isInRange = agentToTargetVector.magnitude <= chaseRange;

        bool hasLineOfSight = Physics.Raycast(
            lineOfSightPoint.position,
            agentToTargetVector.normalized,
            out RaycastHit hit,
            30f            
        ) && hit.transform.tag == "Player";

        if (isInRange && hasLineOfSight) {

            if (!IsChasing) {
                IsChasing = true;

                Chase?.Invoke();
            }

            agent.speed = chaseSpeed;
            agent.destination = target.position;
        }
        else {
            IsChasing = false;   
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
