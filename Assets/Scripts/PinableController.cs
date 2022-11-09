using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinableController : MonoBehaviour
{

    [SerializeField] GameObject attachable;
    private Rigidbody2D rb;
    void Start(){
        rb = GetComponent<Rigidbody2D>();        
    }

    void Update(){
        
    }
}
