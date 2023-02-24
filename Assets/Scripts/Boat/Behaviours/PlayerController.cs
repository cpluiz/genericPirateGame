using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoatController))]
public class PlayerController : MonoBehaviour{
    private BoatController mainController;

    void Awake(){
        mainController = GetComponent<BoatController>();
    }

    void Update(){
        if(Input.GetButton("FireForward"))
            mainController.ShootForard();
        if(Input.GetButton("FireLeft"))
            mainController.ShootLeft();
        if(Input.GetButton("FireRight"))
            mainController.ShootRight();
    }
}
