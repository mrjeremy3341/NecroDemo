using UnityEngine;
using System.Collections.Generic;

public class Stat {
    public int BaseValue { get; private set; }
    public int Value { get { return CalulateValue(); } }

    List<int> flatModifers;
    List<float> percentModifiers;

    public Stat(int baseValue) {
        BaseValue = baseValue;
        flatModifers = new List<int>();
        percentModifiers = new List<float>();
    }
 
    public void AddModifier(int value) {
        flatModifers.Add(value);
    }

    public void AddModifier(float value) {
        percentModifiers.Add(value);
    }

    public void RemoveModifier(int value) {
        foreach(int mod in flatModifers) {
            if(mod ==  value) {
                flatModifers.Remove(mod);
                return;
            }
        }
    }

    public void RemoveModifier(float value) {
        foreach(int mod in percentModifiers) {
            if(mod ==  value) {
                percentModifiers.Remove(mod);
                return;
            }
        }
    }

    int CalulateValue() {
        float currentValue = BaseValue;
        foreach(int mod in flatModifers) {
            currentValue += mod;
        }
        
        float percentSum = 0f;
        foreach(float mod in percentModifiers) {
            percentSum += mod;
        }
        currentValue *= 1 + percentSum;

        return Mathf.RoundToInt(currentValue);
    }
}
