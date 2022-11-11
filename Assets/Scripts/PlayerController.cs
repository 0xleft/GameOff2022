using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{   
    //horizontal speed
    public float playerSpeed = 11.27f;
    //the jump height
    public float playerJumpHeight = 14.6f;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    [SerializeField] LayerMask levelLayer;
    public int currentLevel;
    private float initialGravity;
    [Range (0, 1)]
    public float gravityBoostDelay = 0.118f; // when the gravity boost kicks in
    private float timeSinceJump = 0;
    private bool appliedGravityBoost = false;
    public float gravityBoostAmount = 3.28f;

    void Awake() {
        anim = GetComponent<Animator>(); // for animations
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        // get the scene number we are curently on
        string currentScene = SceneManager.GetActiveScene().name;
        try { // convert toint32 can throw FormatException and OverflowException
            currentLevel = Convert.ToInt32(currentScene.Substring(currentScene.Length-1, 1)); // will work until 9 levels
            Debug.Log("CURRENT LEVEL"+currentLevel);
        } catch (FormatException) {
            Debug.Log("Level does not have a number at the end");
        } catch (OverflowException) {
            Debug.Log("Nuber is too big to fit in int32");
        }

        // set gravity so can remember it for later
        initialGravity = rb.gravityScale;
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

        // apply gravity boost
        if (!isGrounded() && !appliedGravityBoost){
            if (timeSinceJump > gravityBoostDelay){
                Debug.Log("BOOST");
                timeSinceJump = 0;
                appliedGravityBoost = true;
                rb.gravityScale += gravityBoostAmount;
            }
            timeSinceJump += Time.deltaTime;
        }

        // remove gravity boost when grouneded
        if (isGrounded() && appliedGravityBoost){
            appliedGravityBoost = false;
            rb.gravityScale = initialGravity;
        }
    }

    // when player collides with something
    private void OnCollisionEnter2D(Collision2D coll) {
        // if it touched the goal
        if (coll.collider.tag == "Goal"){
            Debug.Log("Level up");
            SceneManager.LoadScene("Level-"+(currentLevel+1)); // next level
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
