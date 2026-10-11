using UnityEngine;

/// <summary>
/// Central manager responsible for playing sound effects.
/// Other objects should use this class to request audio playback
/// instead of managing AudioSources directly.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }

    [Header("Main BGM")]
    [SerializeField] private AudioClip mainBGM;
    [SerializeField] private bool loopMainBGM = true;

    [Header("Win BGM")]
    [SerializeField] private AudioClip gameWinBGM;
    [SerializeField] private bool loopGameWinBGM = true;

    private AudioSource audioSource;

    private void Awake() {
        // Ensure only one instance exists
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        PlayMainBGM();
    }

    /// <summary>
    /// Plays background music using the manager.
    /// </summary>
    /// <param name="isLooping">Determines whether audio clip should loop</param>
    public void PlayMusic(AudioClip clip, bool isLooping) {
        if (clip == null) { return; }

        // Prevents replaying same audio clip
        if (audioSource.clip == clip && audioSource.isPlaying) {
            return;
        }

        audioSource.loop = isLooping;
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void PlayMainBGM() {
        if (mainBGM == null) { return; }

        PlayMusic(mainBGM, loopMainBGM);
    }

    public void PlayGameWinBGM() {
        if (gameWinBGM == null) { return; }

        PlayMusic(gameWinBGM, loopGameWinBGM);
    }

    /// <summary>
    /// Plays a one-shot audio clip using the manager.
    /// </summary>
    public void PlayAudioClip(AudioClip clip) {
        if (clip == null) { return; }

        audioSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Randomly chooses and plays a one-shot audio clip using the manager.
    /// </summary>
    public void PlayRandomAudioClip(AudioClip[] clips) {
        if (clips == null || clips.Length == 0) { return; }

        int randomIndex = Random.Range(0, clips.Length);

        audioSource.PlayOneShot(clips[randomIndex]);
    }

    public void StopMusic() {
        audioSource.Stop();
    }
}