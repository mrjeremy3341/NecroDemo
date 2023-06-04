using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class UnitData : ScriptableObject {
    [Range(50, 750)] public int Health = 50;
    [Range(0, 200)] public int Attack = 0;
    [Range(0, 200)] public int Magic = 0;
    [Range(0, 200)] public int Armor = 0;
    [Range(0, 200)] public int Protection = 0;
    [Range(0, 100)] public int Critical = 0;
    [Range(1, 20)] public int Speed = 1;
    [Range(1, 20)] public int Range = 1;
}
