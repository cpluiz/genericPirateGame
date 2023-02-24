using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour{
    [SerializeField] private CannonBall cannonBall;
    [SerializeField] private BoatController boat;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] explosionSound;
    void OnEnable(){
        if(explosionSound.Length <=0 ) return;
        audioSource.PlayOneShot(explosionSound[Random.Range(0, explosionSound.Length)]);
    }
    public void ExplosionIsOver(){
        if(cannonBall == null && boat == null){
            gameObject.SetActive(false);
            return;
        }
        if(cannonBall != null)
            cannonBall.ExplosionIsOver();
        if(boat != null)
            boat.ExplosionIsOver();
    }
}
