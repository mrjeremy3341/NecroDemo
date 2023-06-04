using UnityEngine;
using System.Collections.Generic;

public class GridSystem : Singleton<GridSystem> {
    public Pathfinding Pathfinding { get; private set; }
    
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float cellSize;
    
    [SerializeField] GridObject gridObjectPrefab;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask obstacleLayer;

    GridObject[,] grid;
    List<GridPosition> validPositions;

    protected override void Awake() {
        base.Awake();

        Pathfinding = new Pathfinding(this);
        grid = new GridObject[width, height];
        validPositions = new List<GridPosition>();
        
        for(int x = 0; x < width; x++) {
            for(int z = 0; z < height; z++) {
                GridPosition gridPosition = GetGridPosition(x, z);
                grid[x, z] = CreateGridObject(gridPosition);
                validPositions.Add(gridPosition);
            }
        }
    }

    public List<GridObject> All() {
        List<GridObject> result = new List<GridObject>();
        foreach(GridObject gridObject in grid) {
            result.Add(gridObject);
        }
        
        return result;
    }

    public Vector3 GetMousePosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, groundLayer);

        return hit.point;
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition) {
        float x = cellSize * (Mathf.Sqrt(3f) * gridPosition.Q + (Mathf.Sqrt(3f) / 2f) * gridPosition.R);
        float z = cellSize * ((3f / 2f) * gridPosition.R);

        return new Vector3(x, 0f, z); 
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) {
        float fracQ = (Mathf.Sqrt(3f) / 3f * worldPosition.x - 1f / 3f * worldPosition.z) / cellSize;
        float fracR = (2f / 3f * worldPosition.z) / cellSize;
        float fracS = -fracQ - fracR;

        int q = Mathf.RoundToInt(fracQ);
        int r = Mathf.RoundToInt(fracR);
        int s = Mathf.RoundToInt(fracS);

        float deltaQ = Mathf.Abs(q - fracQ);
        float deltaR = Mathf.Abs(r - fracR);
        float deltaS = Mathf.Abs(s - fracS);

        if(deltaQ > deltaR && deltaQ > deltaS) {
            q = -r - s;
        }
        else if(deltaR > deltaS) {
            r = -q - s;
        }

        return new GridPosition(q, r);
    }

    public GridPosition GetMouseGridPosition() {
        return GetGridPosition(GetMousePosition());
    }

    public GridObject GetGridObject(GridPosition gridPosition) {
        return grid[gridPosition.Q + Mathf.FloorToInt(gridPosition.R / 2f), gridPosition.R];
    }

    public int GetDistance(GridPosition a, GridPosition b) {
        int deltaQ = Mathf.Abs(a.Q - b.Q);
        int deltaR = Mathf.Abs(a.R - b.R);
        int deltaS = Mathf.Abs(a.S - b.S);

        return Mathf.Max(deltaQ, deltaR, deltaS);
    }
    
    public List<GridPosition> GetNeighbors(GridPosition gridPosition) {
        List<GridPosition> result = new List<GridPosition>();
        for(int i = 0; i < 6; i++) {
            result.Add(GetNeighbor(gridPosition, (HexDirections)i));
        }

        return result;
    }

    public List<GridPosition> GetLine(GridPosition origin, int length) {
        return null;
    }

    public List<GridPosition> GetCone(GridPosition origin, int length) {
        return null;
    }
    
    public List<GridPosition> GetArea(GridPosition center, int radius) {
        List<GridPosition> result = new List<GridPosition>();
        
        GridPosition start = GetNeighbor(center, HexDirections.W);
        for(int i = 1; i <= radius; i++) {
            GridPosition current = start;
            for(int j = 0; j < 6; j++) {
                for(int k = 0; k < i; k++) {
                    result.Add(current);
                    current = GetNeighbor(current, (HexDirections)j);
                }
            }

            start = GetNeighbor(start, HexDirections.W);
        }

        return result;
    }

    public bool IsValidGridPosition(GridPosition gridPosition) {
        return validPositions.Contains(gridPosition);
    }

    public void ShowVisuals(List<GridPosition> range, List<GridPosition> valid) {
        foreach(GridPosition gridPosition in range) {
            GridObject gridObject = GetGridObject(gridPosition);
            gridObject.ShowVisual(false);
        }
        
        foreach(GridPosition gridPosition in valid) {
            GridObject gridObject = GetGridObject(gridPosition);
            gridObject.ShowVisual(true);
        }
    }

    public void HideVisuals() {
        foreach(GridObject gridObject in grid) {
            gridObject.HideVisual();
        }
    }

    GridPosition GetGridPosition(int x, int z) {
        return new GridPosition(x - Mathf.FloorToInt(z / 2f), z);
    }

    GridObject CreateGridObject(GridPosition gridPosition) {
        Vector3 worldPosition = GetWorldPosition(gridPosition);
        GridObject gridObject = Instantiate(gridObjectPrefab, worldPosition, Quaternion.identity, this.transform);
        gridObject.name = gridPosition.ToString();
        // check if tile is walkable or not
        gridObject.Initialize(gridPosition, true);

        return gridObject;
    }

    GridPosition GetNeighbor(GridPosition gridPosition, HexDirections direction) {
        if(direction == HexDirections.NE) {
            return gridPosition + new GridPosition(0, 1);
        }
        else if(direction == HexDirections.E) {
            return gridPosition + new GridPosition(1, 0);
        }
        else if(direction == HexDirections.SE) {
            return gridPosition + new GridPosition(1, -1);
        }
        else if(direction == HexDirections.SW) {
            return gridPosition + new GridPosition(0, -1);
        }
        else if(direction == HexDirections.W) {
            return gridPosition + new GridPosition(-1, 0);
        }
        else {
            return gridPosition + new GridPosition(-1, 1);
        }
    }
}
