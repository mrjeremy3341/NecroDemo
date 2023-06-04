using UnityEngine;
using System.Collections.Generic;

public class Pathfinding {
    GridSystem gridSystem;

    public Pathfinding(GridSystem gridSystem) {
        this.gridSystem = gridSystem;
    }

    public List<GridPosition> GetPath(GridPosition start, GridPosition end) {
        PathNode startNode = gridSystem.GetGridObject(start).PathNode;
        PathNode endNode = gridSystem.GetGridObject(end).PathNode;

        List<PathNode> openSet = new List<PathNode>();
        List<PathNode> closedSet = new List<PathNode>();
        openSet.Add(startNode);

        while(openSet.Count > 0) {
            PathNode currentNode = openSet[0];
            for(int i = 1; i < openSet.Count; i++) {
                if(openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost) {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == endNode) {
                return RetracePath(startNode, endNode);
            }

            foreach(PathNode adjacentNode in GetAdjacentNodes(currentNode)) {
                if(!adjacentNode.IsWalkable || closedSet.Contains(adjacentNode)) {
                    continue;
                }

                int distanceToAdjacent = currentNode.GCost + gridSystem.GetDistance(currentNode.GridPosition, adjacentNode.GridPosition);
                if(distanceToAdjacent < adjacentNode.GCost || !openSet.Contains(adjacentNode)) {
                    adjacentNode.GCost = distanceToAdjacent;
                    adjacentNode.HCost = gridSystem.GetDistance(adjacentNode.GridPosition, endNode.GridPosition);
                    adjacentNode.Parent = currentNode;

                    if(!openSet.Contains(adjacentNode)) {
                        openSet.Add(adjacentNode);
                    }
                }
            }
        }

        return null;
    }

    List<GridPosition> RetracePath(PathNode startNode, PathNode endNode) {
        List<GridPosition> path = new List<GridPosition>();
        PathNode currentNode = endNode;

        while(currentNode != startNode) {
            path.Add(currentNode.GridPosition);
            currentNode = currentNode.Parent;
        }
        path.Reverse();

        return path;
    }

    List<PathNode> GetAdjacentNodes(PathNode pathNode) {
        List<PathNode> result = new List<PathNode>();
        foreach(GridPosition gridPosition in gridSystem.GetNeighbors(pathNode.GridPosition)) {
            if(gridSystem.IsValidGridPosition(gridPosition) && gridSystem.GetGridObject(gridPosition).Unit == null) {
                GridObject gridObject = gridSystem.GetGridObject(gridPosition);
                result.Add(gridObject.PathNode);
            }
        }

        return result;
    }
}
