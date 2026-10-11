using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayer : MonoBehaviour {
    [SerializeField, Tooltip("One or more clips; plays randomly if multiple are assigned")]
    private AudioClip[] audioClips;
    [SerializeField] private bool playOnce = false;
    [SerializeField] private float pitchDeviation = 0.1f;

    [SerializeField] private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }

        PlaySFX();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Player") { return; }

        PlaySFX();
    }

    private void PlaySFX() {
        audioSource.pitch = Random.Range(1 - pitchDeviation, 1 + pitchDeviation);

        if (audioClips == null || audioClips.Length == 0) { return; }

        if (audioClips.Length == 1) {
            AudioManager.Instance.PlayAudioClip(audioClips[0]);
        }
        else {
            PlayRandomAudioClip(audioClips);
        }

        if (playOnce) {
            audioClips = null;
        }
    }

    private void PlayRandomAudioClip(AudioClip[] clips) {
        if (clips == null || clips.Length == 0) { return; }

        int randomIndex = Random.Range(0, clips.Length);

        audioSource.PlayOneShot(clips[randomIndex]);
    }
}