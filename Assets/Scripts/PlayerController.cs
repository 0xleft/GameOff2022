using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    //horizontal speed
    public float playerSpeed = 10f;
    //the jump height
    public float playerJumpHeight = 10f;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    [SerializeField] LayerMask levelLayer;

    void Awake() {
        anim = GetComponent<Animator>(); // for animations
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // values only modified in the update method
    void Update(){

        // if player on the wall it is a glitch where he will just float when huggin the wall
        // move player on the x axis
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*playerSpeed,rb.velocity.y);
        
        if (onLeftWall() && rb.velocity.x < 0){ // on left wall and moving left
            // cancel the movement to the left
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (onRightWall()){ // on right wall and moving right
            // cancel the movement to the right
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
        if (Input.GetKey(KeyCode.Space)){
            if (isGrounded()){
                rb.velocity = new Vector2(rb.velocity.x, playerJumpHeight); // apply vertical velocity;
            }
        }
    }

    // check if player is on the ground 
    private bool isGrounded(){
        //NOTE layer mask is the layer that it will recognise when it is being hit
        return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 0.6f, levelLayer).collider != null;
    }

    // check if player is huggin left wall
    private bool onLeftWall(){
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,  Vector2.left, 0.05f, levelLayer).collider != null;
    }

    // is player huggin right wall
    private bool onRightWall(){
        // check if player is huggin right wall
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,  Vector2.right, 0.05f, levelLayer).collider != null;
    }

    // is player huggin a wall
    private bool onWall(){
        // check left side
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,  Vector2.left, 0.05f, levelLayer);
        if (raycast.collider != null){
            return raycast.collider;
        }
        // check right side
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,  Vector2.right, 0.05f, levelLayer).collider != null; // 
    }
}
