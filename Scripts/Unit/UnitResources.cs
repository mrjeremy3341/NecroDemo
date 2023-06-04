using UnityEngine;

public class UnitResources : MonoBehaviour {
    public int MaxMovement { get; private set; }
    public int MaxEnergy { get; private set; }
    public int MaxMana { get; private set; }
    public int CurrentMovement { get; private set; }
    public int CurrentEnergy { get; private set; }
    public int CurrentMana { get; private set; }

    [SerializeField] int baseMovement;
    [SerializeField] int baseEnergy;
    [SerializeField] int baseMana;

    private void Awake() {
        MaxMovement = baseMovement;
        MaxEnergy = baseEnergy;
        MaxMana = baseMana;
        
        CurrentMovement = baseMovement;
        CurrentEnergy = baseEnergy;
        CurrentMana = baseMana;
    }

    public void UseMovement(int movementAmount) {
        CurrentMovement = Mathf.Max(CurrentMovement - movementAmount, 0);
    }

    public void UseEnergy(int energyAmount) {
        CurrentEnergy = Mathf.Max(CurrentEnergy - energyAmount, 0);
    }

    public void UseMana(int manaAmount) {
        CurrentMana = Mathf.Max(CurrentMana - manaAmount, 0);
    }

    public void Turn() { // change the name of this i think
        // reset movement
        // reset energy
        // mana regen
    }
}
