using TMPro;
using UnityEngine;

public class LevelCounterUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI text;

    private void OnEnable() {
        PlayerStateManager.Instance.LevelUp += UpdateLevelCounterUI;
    }
    private void OnDisable() {
        PlayerStateManager.Instance.LevelUp -= UpdateLevelCounterUI;

    }
    private void Start () {
        UpdateLevelCounterUI(PlayerStateManager.Instance.Level);
    }
    private void UpdateLevelCounterUI(int level) {
        if (text != null) {
            text.text = "Level " + level.ToString();   
        }
    }
}
