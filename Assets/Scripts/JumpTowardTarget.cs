using UnityEngine;


public class JumpTowardTarget : MonoBehaviour {
    [SerializeField] private float jumpForce = 1;
    [SerializeField] private Transform jumpToTarget;
    [SerializeField] private bool playOnce = true;
    private Rigidbody rb;

    bool hasJumped = false;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }

        JumpIntooCamera();
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Player") { return; }

        JumpIntooCamera();
    }
    private void JumpIntooCamera() {
        if (playOnce && hasJumped) { return; }

        Vector3 dirToCamera = jumpToTarget.position - transform.position;

        rb.AddForce(dirToCamera * jumpForce);

        hasJumped = true;
    }
}
