using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Rigidbody2D rb;
    public float cameraSpeed = 5; // camera sensitivity on both axes. NOTE we can split it later so it is different speed on diff axes.
                                  // 5 is optimal in my opinion
    
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        // the point where the camera has to go to.
        Vector2 cameraGoal = new Vector2(player.transform.position.x, player.transform.position.y);
        // set camera velocity to that point so it moves there.
        rb.velocity = new Vector2((cameraGoal.x - transform.position.x)*cameraSpeed, 
                                  (cameraGoal.y - transform.position.y)*cameraSpeed);
    }
}
