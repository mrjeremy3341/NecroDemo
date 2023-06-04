using UnityEngine;
using System.Collections.Generic;

public abstract class BaseAction : MonoBehaviour {
    public int EnergyCost;
    public int ManaCost;
    
    protected Unit unit;
    protected bool isActive;

    protected virtual void Awake() {
        unit = GetComponent<Unit>();
    }

    private void Update() {
        if(!isActive) { return; }
        PerformAction();
    }

    protected abstract void PerformAction();

    public abstract void StartAction(GridPosition targetPosition);
    public abstract List<GridPosition> GetRangeGridPositions();
    public abstract List<GridPosition> GetValidGridPositions();
    public abstract string GetActionName();
}
