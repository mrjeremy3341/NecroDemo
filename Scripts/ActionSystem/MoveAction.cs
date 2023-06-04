using UnityEngine;
using System.Collections.Generic;

public class MoveAction : BaseAction {
    List<GridPosition> path;
    int pathIndex;
    
    protected override void PerformAction() {
        float stoppingDistance = 0.1f;
        Vector3 targetPosition = GridSystem.Instance.GetWorldPosition(path[pathIndex]);
        
        if(Vector3.Distance(transform.position, targetPosition) > stoppingDistance) {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            float rotationspeed = 5f;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationspeed * Time.deltaTime);
        }
        else {
            pathIndex++;
            if(pathIndex >= path.Count) {
                unit.Position = path[path.Count - 1];
                GridSystem.Instance.GetGridObject(unit.Position).SetUnit(unit);
                
                InputSystem.Instance.ClearUnitSelection();
                unit.Animator.PlayIdle();
                isActive = false;
            }
        }
    }

    public override void StartAction(GridPosition targetPosition) {
        path = GridSystem.Instance.Pathfinding.GetPath(unit.Position, targetPosition);
        pathIndex = 0;
        
        GridSystem.Instance.GetGridObject(unit.Position).ClearUnit();
        unit.Position = null;
        
        unit.Resources.UseMovement(path.Count);
        unit.Animator.PlayMove();
        isActive = true;
    }

    public override List<GridPosition> GetRangeGridPositions() {
        List<GridPosition> result = new List<GridPosition>();
        
        int moveRange = unit.Resources.CurrentMovement;
        foreach(GridPosition gridPosition in GridSystem.Instance.GetArea(unit.Position, moveRange)) {
            if(GridSystem.Instance.IsValidGridPosition(gridPosition)) {
                result.Add(gridPosition);  
            }
        }

        return result;
    }

    public override List<GridPosition> GetValidGridPositions() {
        List<GridPosition> result = new List<GridPosition>();
        List<GridPosition> range = GetRangeGridPositions();
        
        int moveRange = unit.Resources.CurrentMovement;
        foreach(GridPosition gridPosition in range) {
            GridObject gridObject = GridSystem.Instance.GetGridObject(gridPosition);
            List<GridPosition> path = GridSystem.Instance.Pathfinding.GetPath(unit.Position, gridPosition);
            
            if(path != null && path.Count <= moveRange && gridObject.Unit == null) {
                result.Add(gridPosition);
            } 
        }

        return result;
    }
 
    public override string GetActionName() {
        return "Move";
    }
}
