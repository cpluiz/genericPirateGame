using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour{
    protected BoatController controller;
    protected bool isMoving;
    [SerializeField]protected float turning, angularVelocity, torque;
    [SerializeField]protected Rigidbody2D rb;
    protected void Awake(){
        if(rb == null)
            rb = gameObject.GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate(){
        angularVelocity = rb.angularVelocity;
        MoveForward();
        torque = turning * controller.turnSpeed * -1 ;
        if(
            (angularVelocity >= -controller.maxTurnSpeed && angularVelocity <= controller.maxTurnSpeed) //Se a velocidade angular está dentro do limite máximo permitido
            || Mathf.Sign(angularVelocity) != Mathf.Sign(torque) //ou se o torque a ser aplicado é para a direção oposta
        ){rb.AddTorque(torque, ForceMode2D.Force);}
    }
    protected void MoveForward(){
        if(isMoving)
            rb.AddForce(transform.right * controller.moveSpeed, ForceMode2D.Force);
    }
    public void SetController(BoatController controller){
        this.controller = controller;
    }
}
