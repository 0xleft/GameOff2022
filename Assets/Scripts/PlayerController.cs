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

    void Update(){

        //make player move on the x axes
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*playerSpeed,rb.velocity.y);

        
        if (Input.GetKey(KeyCode.Space)){
            if (isGrounded()){
                rb.velocity = new Vector2(rb.velocity.x, playerJumpHeight); // apply vertical velocity;
            }
        }

    }

    private bool isGrounded(){
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,  Vector2.down, 0.1f, levelLayer); // raycast down to check if player is standing on something;    NOTE layer mask is the layer that it will recognise when it is being hit
        return raycast.collider != null;
    }
}
