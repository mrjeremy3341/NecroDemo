using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionButtonUI : MonoBehaviour {
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI textMeshPro;

    public void SetAction(BaseAction action) {
        textMeshPro.text = action.GetActionName();
        button.onClick.AddListener(() => { InputSystem.Instance.SetSelectedAction(action); });
    }
}
