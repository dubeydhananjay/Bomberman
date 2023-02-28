using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
     
   [SerializeField]
   private float speed = 2f;
   [SerializeField]
   private float waitTime = 0.5f;
   private bool canMove;
   private bool isCaged = true;
   private List<Vector2> posList;
   private Vector3 nextPos;

   public static int enemyDeadCount;

   protected override void Awake() {
      posList = new List<Vector2>();
      base.Awake();
   }

   //Setup the enemy and check for the movement
   public void Setup() { 
      ResetState();
      gameObject.SetActive(true);
      RigidbodyActivation(true);
      canMove = CheckForMovement();
      nextPos = transform.position;
      if(canMove) {
         GetNextPos();
         isCaged = false;
      }
      else StartCoroutine(CheckIfCaged());
   }

   private void OnDisable() {
      ResetState();
   }

   private void Update() {
      if(isCaged) return;
      if((transform.position - nextPos).sqrMagnitude > 0.01f) {
         transform.position = Vector3.MoveTowards(transform.position,nextPos,speed * Time.deltaTime);
      }
      else {
         isCaged = true;
         canMove = CheckForMovement();
         if(canMove) {
            isCaged = false;
            GetNextPos();
         }
         
      }
   }

   //check for movement in all direction and return true if there is atleast one block is free
   private bool CheckForMovement() {
      Vector2 pos = transform.position;
      pos.x = Mathf.Round(pos.x);
      pos.y = Mathf.Round(pos.y);

      posList.Clear();
      CheckNextBlock(pos,Vector2.up);
      CheckNextBlock(pos,Vector2.down);
      CheckNextBlock(pos,Vector2.left);
      CheckNextBlock(pos,Vector2.right);

      if(posList.Count > 0)
         return true;
      
      return false;
   }

   //Add the tile pos in the posotion list
   private void CheckNextBlock(Vector2 pos, Vector2 dir) {
      pos += dir;
      if(!CheckColliders(pos))
         posList.Add(pos);
   }

   //Get renadom tile pos from position list
   private void GetNextPos() {
      nextPos = posList[Random.Range(0,posList.Count)];
      nextPos.z = -3;
      Vector3 scale = Vector3.one;
      if(nextPos.x < transform.position.x)
         scale.x = -1;
      transform.localScale = scale;
   }

   private bool CheckColliders(Vector2 pos) {
      return Physics2D.OverlapBox(pos,Vector2.one/2f,0,LayerMask.GetMask("Tile")) ||
               Physics2D.OverlapBox(pos,Vector2.one/2f,0,LayerMask.GetMask("Bomb"));
     
   }

   //Check if there is no tile to move to
   private IEnumerator CheckIfCaged() {
      yield return waitTime;

      if(CheckForMovement()) {
         isCaged = false;
         yield break;
      }

      StartCoroutine(CheckIfCaged());
   } 

   private void ResetState() {
      isCaged = true;
      StopCoroutine(CheckIfCaged());
   }

    public override void DeathSequenceEnded() {
      base.DeathSequenceEnded();
      enemyDeadCount++;
      if(enemyDeadCount >= SpawnEnemy.Instance.EnemyAmount) {
         enemyDeadCount = 0;
         UIManager.Instance.ActivateWinGamePanel();
      }
   }

  protected override void DeathSequence() {
    UIManager.Instance.IncrementScore();
    base.DeathSequence();
  }
}
