using System.Collections.Generic;
using UnityEngine;

public class TestAction : BaseAction
{
    public override string GetActionName()
    {
        return "Test";
    }

    public override List<GridPosition> GetRangeGridPositions()
    {
        throw new System.NotImplementedException();
    }

    public override List<GridPosition> GetValidGridPositions()
    {
        throw new System.NotImplementedException();
    }

    public override void StartAction(GridPosition targetPosition)
    {
        throw new System.NotImplementedException();
    }

    protected override void PerformAction()
    {
        throw new System.NotImplementedException();
    }
}
