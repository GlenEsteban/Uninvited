using UnityEngine;

public class FaceCamera : MonoBehaviour {
    private Transform cameraTransform;

    private void Start() {
        cameraTransform = Camera.main.transform;
    }
    private void LateUpdate() {
        transform.rotation = Quaternion.Euler( 0f, cameraTransform.eulerAngles.y, 0f );
    }
}
