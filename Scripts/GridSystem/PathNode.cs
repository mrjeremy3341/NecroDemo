using UnityEngine;

public class PathNode {
    public GridObject GridObject { get; private set; }
    public GridPosition GridPosition { get; private set; }
    public bool IsWalkable { get; private set; }
    
    public int GCost;
    public int HCost;
    public int FCost { get { return GCost + HCost; } }
    
    public PathNode Parent;

    public PathNode(GridObject gridObject, GridPosition gridPosition, bool isWalkable) {
        GridObject = gridObject;
        GridPosition = gridPosition;
        IsWalkable = isWalkable;
    }
}
