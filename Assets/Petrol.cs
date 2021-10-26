using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petrol : MonoBehaviour {
    PlayerMovements playerMovement;

    void Start() {
        playerMovement = FindObjectOfType<PlayerMovements>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            playerMovement.Petrol();
        }
        Destroy(gameObject);
    }
}

