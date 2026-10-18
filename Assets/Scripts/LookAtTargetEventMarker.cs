using Febucci.TextAnimatorCore.Typing;
using Febucci.TextAnimatorForUnity;
using UnityEngine;

[RequireComponent(typeof(LookAtTarget))]
public class LookAtTargetEventMarker : MonoBehaviour {
    [SerializeField] private TypewriterComponent typewriter;
    private LookAtTarget lookAtTargetComponent;

    private void Awake() {
        lookAtTargetComponent = GetComponent<LookAtTarget>();
    }

    // adds and removes callbacks
    void OnEnable() => typewriter.onMessage.AddListener(OnMessage);
    void OnDisable() => typewriter.onMessage.RemoveListener(OnMessage);

    void OnMessage(EventMarker marker) {
        switch (marker.name) {
            case "LookAt":

                Transform target = DialogueManager.Instance.CharacterSpeaking;

                print("Look at: " + target.name);

                lookAtTargetComponent.SetTarget(target);

                break;
        }
    }
}