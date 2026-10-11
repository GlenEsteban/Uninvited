using StarterAssets;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemySFX : MonoBehaviour {
    [SerializeField, Tooltip("One or more clips; plays randomly if multiple are assigned")]
    private AudioClip[] idleSFX;
    [SerializeField, Tooltip("One or more clips; plays randomly if multiple are assigned")]
    private AudioClip[] footstepsSFX;
    [SerializeField, Tooltip("One or more clips; plays randomly if multiple are assigned")]
    private AudioClip[] chaseSFX; 
    [SerializeField, Tooltip("One or more clips; plays randomly if multiple are assigned")]
    private AudioClip[] attackSFX;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AIController aiController;
    [SerializeField] private Combat combat;
    [SerializeField] private float movementSFXThreshold = 0.5f;
    [SerializeField] private float idleSFXRate = 2f;
    [SerializeField] private float chaseSFXRate = 2f;
    [SerializeField] private float runSpeedSFXRate = 0.25f;

    private void OnEnable() {
        combat.Attack += PlayAttackSFX;
        aiController.Chase += PlayChaseSFX;
    }
    private void OnDisable() {
        combat.Attack -= PlayAttackSFX;
        aiController.Chase -= PlayChaseSFX;
    }

    private void Start() {
        StartCoroutine(PlayFootstepsSFX());
    }

    private IEnumerator PlayFootstepsSFX() {
        while (true) {
            if (aiController.IsChasing) {
                bool isIdle = agent.velocity.sqrMagnitude < movementSFXThreshold;

                if (!isIdle) {
                    audioSource.pitch = 1f;

                    PlayRandomAudioClip(footstepsSFX);

                    yield return new WaitForSeconds(runSpeedSFXRate);
                }
                else {
                    PlayRandomAudioClip(null);

                    yield return null;
                }

            }
            else {
                PlayRandomAudioClip(null);

                yield return null;
            }

            yield return null;
        }
    }

    private void PlayAttackSFX() {
        audioSource.pitch = 1f;

        PlayRandomAudioClip(attackSFX);
    }
    private void PlayChaseSFX() {
        if (audioSource.isPlaying) { return; }

        audioSource.pitch = 1f;

        PlayRandomAudioClip(chaseSFX);
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
