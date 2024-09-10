using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {
    [SerializeField] GameObject particleSystem;
    PlayerMovements playerMovement;
    void Start() {
        playerMovement = FindFirstObjectByType<PlayerMovements>();
    }
    //Todo particle tobe spawned not played
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            playerMovement.Coin();
            ParticleSystem[] particles =  particleSystem.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particle in particles) {
                particle.Play();
                Debug.Log(particle.name);
            }
        }
        Destroy(gameObject);
    }
}
