using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour {
    [SerializeField]
    private Tilemap indestructibleTiles; 
    [SerializeField]
    private Tilemap destructibleTiles;

    public Tilemap IndestructibleTiles => indestructibleTiles;
    public Tilemap DestructibleTiles => destructibleTiles;
}
