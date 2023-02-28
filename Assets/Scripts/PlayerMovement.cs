using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    private Vector2 direction = Vector2.down;
    public float speed = 5f;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public Sprite downSprite;
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite activeSprite;

    private Player player;
    public SpriteRenderer spriteRenderer;

     private void Awake() {
        activeSprite = downSprite;
        player = GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Take input from user and move character in the particular direction
    private void Update() {
        if(player.Death) return;
        
        if (Input.GetKey(inputUp)) {
            SetDirection(Vector2.up, "UpAnimation");
            activeSprite = upSprite;
        } else if (Input.GetKey(inputDown)) {
            SetDirection(Vector2.down, "DownAnimation");
            activeSprite = downSprite;
        } else if (Input.GetKey(inputLeft)) {
            SetDirection(Vector2.left, "LeftAnimation");
            activeSprite = leftSprite;
        } else if (Input.GetKey(inputRight)) {
            SetDirection(Vector2.right, "RightAnimation");
            activeSprite = rightSprite;
        } else {
            SetDirection(Vector2.zero, "Idle");
            spriteRenderer.sprite = activeSprite;
        }
    }

    private void FixedUpdate() {
        Vector2 position = player.CharacterRB.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        player.CharacterRB.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 dir, string animationName) {
        direction = dir;
        player.PlayAnimation(animationName);
    }

}
