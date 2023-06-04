[System.Flags]
public enum EffectType {
    Self = 1 << 1, Ally = 1 << 2, Enemy = 1 << 3, All = Self | Ally | Enemy
}
