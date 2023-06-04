using UnityEngine;
using UnityEngine.UI;

public class ActionSystemUI : MonoBehaviour {
    [SerializeField] ActionButtonUI actionButtonPrefab;
    [SerializeField] Transform actionButtonContainer;

    private void Start() {
        InputSystem.Instance.OnSelectedUnitChange += UpdateActionButtons;
    }

    void UpdateActionButtons(Unit selecteUnit) {
        foreach(Transform actionButtonTransform in actionButtonContainer) {
            Destroy(actionButtonTransform.gameObject);
        }
        
        if(selecteUnit != null) {
            foreach(BaseAction action in selecteUnit.Actions) {
                ActionButtonUI actionButton = Instantiate(actionButtonPrefab, actionButtonContainer);
                actionButton.SetAction(action);
            }
        }
    }
}