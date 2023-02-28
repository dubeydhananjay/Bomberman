using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {
    public KeyCode input = KeyCode.Space;
    public Bomb bomb;

    private void Update() { // using input key to spawn the bomb
        if(bomb.CanSetBomb && Input.GetKeyDown(input)) {
            bomb.Activation(true);
            SetBombPosition();
        }
    }

    private void SetBombPosition() { 
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);
        pos.z = -2;
        bomb.SetPos(pos);
    }
}
