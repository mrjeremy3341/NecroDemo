using UnityEngine;
using System.Collections.Generic;

public class AttackAction : BaseAction {
    [SerializeField] int range;
    
    protected override void PerformAction() {
        InputSystem.Instance.ClearUnitSelection();
        isActive = false;
    }

    public override void StartAction(GridPosition targetPosition) {
        Unit targetUnit = GridSystem.Instance.GetGridObject(targetPosition).Unit;
        targetUnit.Health.DamageUnit(10);
        GridSystem.Instance.GetArea(targetUnit.Position, range);
        unit.Animator.PlayAttack();
        isActive = true;
    }

    public override List<GridPosition> GetRangeGridPositions() {
        List<GridPosition> result = new List<GridPosition>();
        foreach(GridPosition gridPosition in GridSystem.Instance.GetNeighbors(unit.Position)) {
            if(GridSystem.Instance.IsValidGridPosition(gridPosition)) {
                result.Add(gridPosition);
            }
        }
        
        return result;
    }

    public override List<GridPosition> GetValidGridPositions() {
        List<GridPosition> result = new List<GridPosition>();
        foreach(GridPosition gridPosition in GetRangeGridPositions()) {
            GridObject gridObject = GridSystem.Instance.GetGridObject(gridPosition);
            if(gridObject.Unit != null && gridObject.Unit.Faction != unit.Faction) {
                result.Add(gridPosition);
            } 
        }

        return result;
    }

    public override string GetActionName() {
        return "Attack";
    }
}
