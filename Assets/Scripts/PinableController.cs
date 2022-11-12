using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinableController : MonoBehaviour
{
    public GameObject pin;
    private int pinCount = 0;
    public bool isMouseOver = false;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update(){
        if (Input.GetButtonDown("Fire1") && isMouseOver){
            AttatchPin(Input.mousePosition);
        }
    }
 
    public void AttatchPin(Vector2 mousePosition){
        pinCount++;
        // shot a ray from camera
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 0));
        // get the origin of the ray which is equal to the position in the world and set it as a child of the pinable
        GameObject pinInstance = Instantiate(pin, ray.origin, Quaternion.identity, this.transform);
        // create new target joint component and attach it to pinable
        TargetJoint2D customTargetJoint = this.gameObject.AddComponent<TargetJoint2D>();
        customTargetJoint.autoConfigureTarget = false;
        // they are both at the same location so it looks like it was pinned
        customTargetJoint.target = ray.origin;
        customTargetJoint.anchor = pinInstance.transform.localPosition;
    }

    private void OnMouseEnter() {
        isMouseOver = true;
    }

    private void OnMouseExit() {
        isMouseOver = false;
    }
}
