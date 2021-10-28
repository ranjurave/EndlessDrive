using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {
    PlayerMovements playerMovement;
    void Start() {
        playerMovement = FindObjectOfType<PlayerMovements>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            playerMovement.Coin();
        }
        Destroy(gameObject);
    }
}
