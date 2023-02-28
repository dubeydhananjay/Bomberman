using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Bomb : MonoBehaviour {
   
    public Animator animator;
    [SerializeField]
    private float bombBlastTimer = 3; //time to blast
    [SerializeField]
    private int explosionRadius = 2; //no of block covered
    [SerializeField]
    private Explosion pfExplosion;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Destructible pfDestructible;
    [SerializeField]
    private Tilemap destructibleTiles;
    private bool canSetBomb = true;
    private Collider2D bombCollider;

    public bool CanSetBomb => canSetBomb;

    private void Awake() {
        animator = GetComponent<Animator>();
        bombCollider = GetComponent<Collider2D>();
        Activation(false);
    }
    private void OnEnable() {
        destructibleTiles = GameManager.Instance.CurrentLevel.DestructibleTiles;
        StartCoroutine(BombBlast());
    }

    private void OnDisable() {
        canSetBomb = true;
        TriggerActivation(true);
        StopCoroutine(BombBlast());
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
            TriggerActivation(false);
    }

     private void TriggerActivation(bool activate) {
        bombCollider.isTrigger = activate;
    }

    //Method to blast the bomb and cover number of block
    private IEnumerator BombBlast() {
        canSetBomb = false;
        yield return new WaitForSeconds(bombBlastTimer);
        Vector3 pos = transform.position;
        Explosion explosion = Instantiate(pfExplosion, pos, Quaternion.identity);
        explosion.SetPos(pos);
        ExplodeExtend(pos,Vector2.up,explosionRadius);
        ExplodeExtend(pos,Vector2.down,explosionRadius);
        ExplodeExtend(pos,Vector2.left,explosionRadius);
        ExplodeExtend(pos,Vector2.right,explosionRadius);
        Activation(false);
    }

    //Method to cover number of block and check the tiles around the bomb explosion
    private void ExplodeExtend(Vector2 pos, Vector2 dir, int length) {
        if(length <= 0) {
            return;
        }
        pos += dir;
        if(Physics2D.OverlapBox(pos,Vector2.one/2f,0,layerMask)) {
            ClearDestructible(pos);
            return;
        }
        
        Explosion explosion = Instantiate(pfExplosion, pos, Quaternion.identity);
        explosion.SetPos(pos);
        ExplodeExtend(pos,dir,length - 1);
    }
    
    //Remove the destructible tile and replace it with animated tile
     private void ClearDestructible(Vector2 position) {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile = destructibleTiles.GetTile(cell);

        if (tile != null) {
            Instantiate(pfDestructible, position, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }
    }

    public void Activation(bool activate) {
        gameObject.SetActive(activate);
    }

    public void SetPos(Vector3 pos) {
        transform.position = pos;
    }
    
}
