using StarterAssets;
using System.Collections;
using UnityEngine;

public class PlayerSFX : MonoBehaviour {
    [SerializeField, Tooltip("One or more clips; plays randomly if multiple are assigned")]
    private AudioClip[] footstepsSFX;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private StarterAssetsInputs playerInput;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float movementSFXThreshold = 0.5f;
    [SerializeField] private float walkSpeedSFXRate = 0.5f;
    [SerializeField] private float runSpeedSFXRate = 0.25f;

    private bool wasGrounded = true;
    private bool isAirborne = false;

    private IEnumerator Start() {
        while(true) {
            bool isIdle = playerInput.move.sqrMagnitude < 0.01f;

            if (isIdle) {
                yield return null;
                continue;
            }

            if (!characterController.isGrounded) {

                yield return null;
                continue;
            }

            if (playerInput.sprint) {
                audioSource.pitch = 1.1f;

                PlayRandomAudioClip(footstepsSFX);

                yield return new WaitForSeconds(runSpeedSFXRate);
            }
            else {
                audioSource.pitch = 1;
                if (characterController.velocity.magnitude > movementSFXThreshold) {
                    PlayRandomAudioClip(footstepsSFX);
                }

                yield return new WaitForSeconds(walkSpeedSFXRate);
            }
        }
    }

    private void Update() {
        bool isGrounded = characterController.isGrounded;

        if (wasGrounded && !isGrounded) {
            isAirborne = true;

            if (characterController.velocity.y > 0f) {
                audioSource.pitch = 1.3f;

                PlayRandomAudioClip(footstepsSFX);
            }
        }

        if (isAirborne && !wasGrounded && isGrounded) {
            isAirborne = false;

            audioSource.pitch = 0.7f;

            PlayRandomAudioClip(footstepsSFX);
        }

        wasGrounded = isGrounded;
    }

    private void PlayRandomAudioClip(AudioClip[] clips) {
        if (footstepsSFX == null || footstepsSFX.Length == 0) { return; }

        if (footstepsSFX.Length == 1) {
            audioSource.PlayOneShot(footstepsSFX[0]);
        }
        else {
            if (clips == null || clips.Length == 0) { return; }

            int randomIndex = Random.Range(0, clips.Length);

            audioSource.PlayOneShot(clips[randomIndex]);
        }
    }
}