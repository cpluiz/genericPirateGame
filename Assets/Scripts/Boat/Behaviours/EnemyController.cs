using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyMovement))]
public class EnemyController : MonoBehaviour{
    private EnemyMovement enemyMovementController;
    [Header("Enemy Attack configuration")]
    [SerializeField, Range(0.1f, 10f)] private float collisionDammage;
    [SerializeField, Range(1f, 10f)] private float activeShootRadius;
    [SerializeField] private BoatController boatMainController;
    void Awake(){
        if(boatMainController == null)
            boatMainController = GetComponent<BoatController>();
        if(enemyMovementController == null)
            enemyMovementController = GetComponent<EnemyMovement>();
    }
    void Update(){
        if(Vector2.Distance(transform.position, LevelController.instance.Player.transform.position) <= activeShootRadius){
            enemyMovementController.ChangeToShoot();
        }else{
            enemyMovementController.ChangeToChase();
        }
        //Se o inimigo estiver no modo de perseguição, encerra aqui
        if(enemyMovementController.isChasing) return;
        //Caso contrário, verifica para qual direção atirar
        if(enemyMovementController.targetIsInFront){
            boatMainController.ShootForard(true);
        }else if(Mathf.Sign(enemyMovementController.angleToPlayer) > 0){
            boatMainController.ShootRight(true);
        }else{
            boatMainController.ShootLeft(true);
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        if(boatMainController.boatType != BoatType.Chaser) return;
        if(!other.gameObject.CompareTag("Player")) return;
        boatMainController.GetComponent<Rigidbody2D>().simulated = false;
        BoatController player = other.gameObject.GetComponent<BoatController>();
        player.TakeDammage(collisionDammage);
        boatMainController.TakeDammage(boatMainController.maxHealth * 100);
    }
}
