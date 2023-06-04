using UnityEngine;

public class Unit : MonoBehaviour {    
    public GridPosition Position;
    public Faction Faction;
    public Race Race;

    public UnitAnimator Animator { get; private set; }
    public UnitStats Stats { get; private set; }
    public UnitHealth Health { get; private set; }
    public UnitResources Resources { get; private set; }
    public UnitEffects Effects { get; private set; }

    public BasePassive Passive { get; private set; }
    public BaseAction[] Actions { get; private set; }

    Outline outline;

    private void Awake() {
        Animator = GetComponent<UnitAnimator>();
        Stats = GetComponent<UnitStats>();
        Health = GetComponent<UnitHealth>();
        Resources = GetComponent<UnitResources>();
        Effects = GetComponent<UnitEffects>();

        Passive = GetComponent<BasePassive>();
        Actions = GetComponents<BaseAction>();
        
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void Start() {
        Position = GridSystem.Instance.GetGridPosition(transform.position);
        GridSystem.Instance.GetGridObject(Position).SetUnit(this);
    }

    public void Select() {
        outline.enabled = true;
    }

    public void Deslect() {
        outline.enabled = false;
    }
}
