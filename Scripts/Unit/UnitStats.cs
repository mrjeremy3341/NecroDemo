using UnityEngine;

public class UnitStats : MonoBehaviour {
    public Stat Attack { get; private set; }
    public Stat Magic { get; private set; }
    public Stat Critical { get; private set; }
    public Stat Armor { get; private set; }
    public Stat Protection { get; private set; }

    [SerializeField] int baseAttack;
    [SerializeField] int baseMagic;
    [SerializeField] int baseCritical;
    [SerializeField] int baseArmor;
    [SerializeField] int baseProtection;

    private void Awake() {
        Attack = new Stat(baseAttack);
        Magic = new Stat(baseMagic);
        Critical = new Stat(baseCritical);
        Armor = new Stat(baseArmor);
        Protection = new Stat(baseProtection);
    }
}
