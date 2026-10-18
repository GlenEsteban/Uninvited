using UnityEngine;

public class DialogueUI : MonoBehaviour {
    [SerializeField] private Animator animator;

    public void Display() {
        animator.SetBool("isDisplaying", true);
    }
    public void Hide() {
        animator.SetBool("isDisplaying", false);
    }
}
