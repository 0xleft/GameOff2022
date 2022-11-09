using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector2 cameraOffset;
    private Rigidbody2D rb;
    
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        cameraOffset = player.transform.position - transform.position;
    }


    void Update(){
        rb.MovePosition(new Vector2(player.transform.position.x + cameraOffset.x, player.transform.position.y + cameraOffset.y));
    }
}
