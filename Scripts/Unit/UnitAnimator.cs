using UnityEngine;

public class UnitAnimator : MonoBehaviour {
    int idle = Animator.StringToHash("Idle");
    int move = Animator.StringToHash("Move");
    int attack = Animator.StringToHash("Attack");
    int hurt = Animator.StringToHash("Hurt");
    int die = Animator.StringToHash("Die");
    
    Animator animator;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
    }

    public void PlayIdle() {
        StartAnimation(idle);
    }

    public void PlayMove() {
        StartAnimation(move);
    }

    public void PlayAttack() {
        StartAnimation(attack);
    }

    public void PlayHurt() {
        StartAnimation(hurt);
    }

    public void PlayDie() {
        StartAnimation(die);
    }

    void StartAnimation(int animation) {
        animator.CrossFade(animation, 0.25f, 0);
    }
}
 