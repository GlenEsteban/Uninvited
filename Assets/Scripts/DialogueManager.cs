using Febucci.TextAnimatorCore;
using Febucci.TextAnimatorForUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager Instance { get; private set; }

    public bool IsDialogueActive { get; private set; }
    [field: SerializeField] public Transform CharacterSpeaking { get; private set; }

    [SerializeField] private PlayerInput playerControls;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private TypewriterComponent typewriter;

    [SerializeField, TextArea(5, 15)] private string[] lines;
    private int currentLineIndex;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
    }
    private void Start() {
        StartDialogue(lines);
    }
    public void StartDialogue(string[] dialogueLines) {
        if (IsDialogueActive || dialogueLines == null || dialogueLines.Length == 0) { return; }

        playerControls.enabled = false;

        lines = dialogueLines;
        currentLineIndex = 0;

        IsDialogueActive = true;

        dialogueUI.Display();

        DisplayLine();
    }

    public void DisplayLine() {
        string line = lines[currentLineIndex];
        typewriter.ShowText(line);
    }
    public void AdvanceDialogue() {
        if (typewriter.IsShowingText) {
            typewriter.SkipTypewriter();
            return;
        }

        currentLineIndex ++;

        if (currentLineIndex >= lines.Length) {
            EndDialogue();
        }
        else {
            DisplayLine();
        }
    }

    public void EndDialogue() {
        playerControls.enabled = true;

        IsDialogueActive = false;

        dialogueUI.Hide();
    }
}