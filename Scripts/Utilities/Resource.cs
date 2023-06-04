using UnityEngine;

public class Resource {
    public int CurrentAmount { get; private set; }
    public int MaxAmount { get; private set; }

    bool resetToMax;

    public Resource(int maxAmount, bool initializeToMax) {
        MaxAmount = maxAmount;
        CurrentAmount = initializeToMax ? maxAmount : 0;
        resetToMax = initializeToMax;
    }

    public void Decrease(int amount) {
        CurrentAmount -= amount;
        if (CurrentAmount < 0) {
            CurrentAmount = 0;
        }
    }

    public void Increase(int amount) {
        CurrentAmount += amount;
        if (CurrentAmount > MaxAmount) {
            CurrentAmount = MaxAmount;
        }
    }

    public void Reset() {
        CurrentAmount = resetToMax ? MaxAmount : 0;
    }
}