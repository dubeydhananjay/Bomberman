using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

  private Vector3 initPos = new Vector3(-8,5,-3);  

  protected override void Start() {
    base.Start();
    Setup();
  }

  protected override void OnCollisionDetection(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion") ||
            other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                RigidbodyActivation(false);
                DeathSequence();
        }
    }

  protected override void DeathSequence() {
    GameManager.Instance.GameLost = true;
    base.DeathSequence();
  }

    public override void DeathSequenceEnded() {
      base.DeathSequenceEnded();
      UIManager.Instance.ActivateGameOverPanel();
   }

  public void Setup() {
    RigidbodyActivation(true);
    gameObject.SetActive(true);
    death = false;
    transform.position = initPos;
  }
}
