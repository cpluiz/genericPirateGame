using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour{
    [SerializeField] private Rigidbody2D ballBody;
    [SerializeField] private Animator explosionEffect;
    [SerializeField] private LayerMask targetLayer;
    [Header("CannonBall configurations")]
    [Range(0.001f,10)] public float shootForce = 0.1f;
    [Range(0.01f, 2)] public float dammage = 1;
    public void SetTarget(LayerMask target){
        targetLayer = target;
        gameObject.layer = LayerMask.LayerToName((int)Mathf.Log(targetLayer.value, 2)) == "Player" ? LayerMask.NameToLayer("EnemyCannon") : LayerMask.NameToLayer("PlayerCannon");
        gameObject.name = LayerMask.LayerToName((int)Mathf.Log(targetLayer.value, 2)) == "Player" ? "Enemy Cannon Ball" : "Player Cannon Ball";
    }
    void OnEnable(){
        explosionEffect.gameObject.SetActive(false);
        ballBody.simulated = true;
        ballBody.AddForce(transform.right * shootForce, ForceMode2D.Impulse);
    }
    void OnDisable(){
        ballBody.angularVelocity = 0;
        ballBody.velocity = Vector2.zero;
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.layer == Mathf.Log(targetLayer.value, 2)){
            ballBody.angularVelocity = 0;
            ballBody.velocity = Vector2.zero;
            ballBody.simulated = false;
            explosionEffect.gameObject.SetActive(true);
            BoatController target = other.gameObject.GetComponent<BoatController>();
            target.TakeDammage(dammage);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Border") || other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            gameObject.SetActive(false);
    }

    public void ExplosionIsOver(){
        explosionEffect.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}