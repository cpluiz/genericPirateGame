using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : BoatMovement{
    [Header("Define a abertura de ângulo do barco do inimigo com relação ao player onde o barco inimigo não gira")]
    [SerializeField, Range(0, 90)] private float angleWithoutRotation;
    [Header("Qual a distância mínima para definir se o alvo está muito próximo?")]
    [SerializeField, Range(0, 2f)] private float minDistance = 0.5f;
    private Transform target;
    private Vector3 targetDir;
    [SerializeField] private float absoluteAngle, dot;
    public float angleToPlayer, vector2Angle;
    public bool isChasing;
    public bool targetIsInFront;
    public float chasingConeAngle{get{return angleWithoutRotation / 2;}}
    void Start(){
        target = LevelController.instance.Player.transform;
    }
    new void FixedUpdate(){
        base.FixedUpdate();
        targetDir = (target.position - transform.position).normalized;
        angleToPlayer = Vector2.SignedAngle(isChasing ? transform.right : transform.up, targetDir);
        absoluteAngle = Mathf.Abs(angleToPlayer);
        targetIsInFront = absoluteAngle <= chasingConeAngle || Vector2.Distance(transform.position, target.position) <= minDistance;
    }
    void Update(){
        if(!targetIsInFront || (!isChasing && targetIsInFront)){
            turning = -Mathf.Sign(angleToPlayer) * (isChasing ? 1 : 1.5f);
        }else{
            rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0, Time.time);
        }
        isMoving = true;
    }
    void LateUpdate(){
        Debug.DrawLine(transform.position, transform.position + targetDir, Color.magenta);
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);
        Debug.DrawLine(transform.position, transform.position + transform.right, Color.red);
    }
    new void MoveForward(){
        rb.AddForce(transform.right * controller.moveSpeed * (isChasing ? 1 : 0.5f), ForceMode2D.Force);
    }
    public void ChangeToChase(){
        if(isChasing) return;
        isChasing = true;
    }
    public void ChangeToShoot(){
        if(controller.boatType != BoatType.Shooter) return;
        isChasing = false;
    }
}
