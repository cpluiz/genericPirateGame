using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BoatMovement{
    
    void Update(){
        isMoving = Input.GetAxis("Vertical") > 0;
        turning = Input.GetAxis("Horizontal");
    }
}
