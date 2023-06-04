using UnityEngine;

public class UnitHealth : MonoBehaviour {
    public int MaxHealth { get; private set; }
    public int MaxShield { get; private set; }
    public int CurrentHealth { get; private set; }
    public int CurrentShield { get; private set; }

    [SerializeField] int baseHealth;

    private void Awake() {
        MaxHealth = baseHealth;
        MaxShield = Mathf.RoundToInt(baseHealth * 0.2f);
        
        CurrentHealth = baseHealth;
        CurrentShield = 0;
    }

    public void DamageUnit(int damageAmount) {
        if (CurrentShield > 0) {
            int remainingDamage = Mathf.Max(damageAmount - CurrentShield, 0);
            CurrentShield = Mathf.Max(CurrentShield - damageAmount, 0);
            
            if (remainingDamage > 0) {
                CurrentHealth = Mathf.Max(CurrentHealth - remainingDamage, 0);
            }
        } 
        else {
            CurrentHealth = Mathf.Max(CurrentHealth - damageAmount, 0);
        }

        if (CurrentHealth == 0) {
            HandleDeath();
        }
    }

    public void HealUnit(int healAmount, bool overheal = false) {
        if (overheal) {
            int excessHealAmount = Mathf.Max(CurrentHealth + healAmount - MaxHealth, 0);
            if (excessHealAmount > 0) {
                AddShield(excessHealAmount);
            }
        }

        CurrentHealth = Mathf.Min(CurrentHealth + healAmount, MaxHealth);
    }

    public void AddShield(int shieldAmount) {
        CurrentShield = Mathf.Min(CurrentShield + shieldAmount, MaxShield);
    }

    void HandleDeath() {
        Destroy(gameObject);
    }
}
