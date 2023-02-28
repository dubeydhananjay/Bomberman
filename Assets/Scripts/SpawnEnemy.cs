using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SpawnEnemy : MonoBehaviour {
    
    [SerializeField]
    private Enemy pfEnemy;
    [SerializeField]
    private int enemyAmount = 5;
    [SerializeField]
    private Enemy[] enemies;

    private Tilemap indestructibleTiles;
    private Tilemap destructibleTiles;

    public static SpawnEnemy Instance { get; private set; }
    public int EnemyAmount => enemyAmount;

    private void Awake() {
        Instance = this;
        enemies = new Enemy[enemyAmount];
    }

    private void Start() {
       SpawnEnemies();
       SetEnemiesPos();
    }

    //Check the tile which is ground on which the enemy can spawn on
    private bool CanSpawnOnGround(Vector2 v) {
        TileBase tile = indestructibleTiles.GetTile(indestructibleTiles.WorldToCell(v));
        TileBase dTile = destructibleTiles.GetTile(destructibleTiles.WorldToCell(v));
        return (dTile == null) && (tile != null) && (tile.name == "Ground" || tile.name == "GroundShadow");
    }

    private void SpawnEnemies() {
        
        for(int i = 0; i < enemyAmount; i++) {
           enemies[i] = Instantiate(pfEnemy,new Vector3(999,999,-3),Quaternion.identity);
        }
    }

    //from list of indestructible tiles get random ground tile and check if the tile is free
    public void SetEnemiesPos() {
        indestructibleTiles = GameManager.Instance.CurrentLevel.IndestructibleTiles; 
        destructibleTiles = GameManager.Instance.CurrentLevel.DestructibleTiles;
        List<Vector2> posList = new List<Vector2>();
        bool flag = false;
        Vector3 v = Vector3.zero;
        for(int i = 0; i < enemyAmount; i++) {
            do {
                v = new Vector3(Random.Range(-4,9), Random.Range(-5,6),-3);
                flag = CanSpawnOnGround(v) && !posList.Contains(v);
            }while(!flag);
            posList.Add(v);
            enemies[i].transform.position = v;
            enemies[i].Setup();
        }
    }
}
