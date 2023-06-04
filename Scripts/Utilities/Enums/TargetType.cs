[System.Flags]
public enum TargetType {
    Self = 1 << 1, Ally = 1 << 2, Enemy = 1 << 3, Ground = 1 << 4, All = Self | Ally | Enemy | Ground
}
