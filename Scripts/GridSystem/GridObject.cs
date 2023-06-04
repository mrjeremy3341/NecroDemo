using UnityEngine;

public class GridObject : MonoBehaviour {
    public Unit Unit { get; private set; }
    public GridPosition GridPosition { get; private set; }
    public PathNode PathNode { get; private set; }
    
    MeshRenderer visual;
    Color rangeColor = Color.gray;
    Color validColor = Color.blue;

    public void Initialize(GridPosition gridPosition, bool isWalkable) {
        GridPosition = gridPosition;
        PathNode = new PathNode(this, gridPosition, isWalkable);
        visual = GetComponentInChildren<MeshRenderer>();
        ClearUnit();
        HideVisual();
    }

    public void SetUnit(Unit unit) {
        Unit = unit;
    }

    public void ClearUnit() {
        Unit = null;
    }

    public void ShowVisual(bool isValid) {
        visual.enabled = true;
        if(isValid) {
            visual.material.color = validColor;
        }
        else {
            visual.material.color = rangeColor;
        }
    }

    public void HideVisual() {
        visual.enabled = false;
    }
}
