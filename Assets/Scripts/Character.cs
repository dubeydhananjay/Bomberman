using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour { // Blueprint class for player and enemies
   private new Rigidbody2D characterRB;
   private Collider2D col2D;
   protected bool death;
   public Animator animator;
   public bool Death => death;
   public Rigidbody2D CharacterRB => characterRB;

    protected virtual void Awake() {
        animator = GetComponent<Animator>();
        characterRB = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
        RigidbodyActivation(true);
    }

    protected virtual void Start() {

    }

    protected virtual void DeathSequence() { 
        death = true;
        PlayAnimation("DeathAnimation");
    }

    protected virtual void OnCollisionDetection(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion")) {
                RigidbodyActivation(false);
                DeathSequence();
        }
    }

    protected void RigidbodyActivation(bool activate) {
        characterRB.constraints = activate ? RigidbodyConstraints2D.FreezeRotation : RigidbodyConstraints2D.FreezeAll;
        col2D.isTrigger = !activate;
    }

    public virtual void DeathSequenceEnded() {
        gameObject.SetActive(false);
    }
    

    public void PlayAnimation(string animationName) {
        animator.Play(animationName);
    }

    

    private void OnCollisionEnter2D(Collision2D other) {
       OnCollisionDetection(other);
    }

}
