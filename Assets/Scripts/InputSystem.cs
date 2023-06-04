using UnityEngine;
using System;

public class InputSystem : Singleton<InputSystem> {
    public Unit SelectedUnit; // { get; private set; }
    public BaseAction SelectedAction { get; private set; }

    public Action<Unit> OnSelectedUnitChange;
    
    [SerializeField] LayerMask unitLayer;
    
    InputState currentState = InputState.WaitingForUnitSelection;
    
    private void Update() {
        if(currentState == InputState.InputBlocked) {
            return;
        }
        else if(currentState == InputState.WaitingForUnitSelection) {
            if(Input.GetMouseButtonDown(0)) {
                HandleUnitSelection();
                return;
            }
        }
        else if(currentState == InputState.WaitingForGridSelection) {
            if(Input.GetMouseButtonDown(1)) {
                ClearUnitSelection();
                return;
            }
            else if(Input.GetMouseButtonDown(0)) {
                HandleGridSelection();
            }
        }
    }

    public void SetSelectedAction(BaseAction action) {
        SelectedAction = action;
        GridSystem.Instance.HideVisuals();
        GridSystem.Instance.ShowVisuals(SelectedAction.GetRangeGridPositions(), SelectedAction.GetValidGridPositions());
    }

    void HandleUnitSelection() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitLayer)) {
            if(hit.transform.TryGetComponent<Unit>(out Unit unit)) {
                unit.Select();
                SelectedUnit = unit;
                currentState = InputState.WaitingForGridSelection;
                SelectedAction = unit.Actions[0];
                OnSelectedUnitChange?.Invoke(SelectedUnit);
                GridSystem.Instance.ShowVisuals(SelectedAction.GetRangeGridPositions(), SelectedAction.GetValidGridPositions());
            }
        }
    }

    public void ClearUnitSelection() {
        SelectedUnit.Deslect();
        SelectedUnit = null;
        currentState = InputState.WaitingForUnitSelection;
        OnSelectedUnitChange?.Invoke(null);
        GridSystem.Instance.HideVisuals();
    }

    void HandleGridSelection() {
        GridPosition targetPosition = GridSystem.Instance.GetMouseGridPosition();
        if(SelectedAction.GetValidGridPositions().Contains(targetPosition)) {
            SelectedAction.StartAction(targetPosition);
            currentState = InputState.InputBlocked;
        }
    }
}
