using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 10f;
    public float playerJumpHeight = 10f;

    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb;

    void Awake() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        grounded = Physics2D.Raycast(transform.position, Vector2.down, 0.5f); // raycast down to check if player is standing on something

        //make player move on the x axes
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*playerSpeed,rb.velocity.y);

        // so the player doesnt rotate since it has rigidbody: dynamic
        transform.rotation = Quaternion.identity;

        if (Input.GetKey(KeyCode.Space)){
            Debug.Log("GROUNDED: "+grounded);
            if (grounded){
                rb.velocity = new Vector2(rb.velocity.x, playerJumpHeight);
            }
        }
        
    }
}
