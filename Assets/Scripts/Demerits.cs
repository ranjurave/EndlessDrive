using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demerits : MonoBehaviour {
    PlayerMovements playerMovement;


    void Start() {
        playerMovement = GameObject.FindObjectOfType<PlayerMovements>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            playerMovement.Demerit();
        }
        Destroy(gameObject);
    }
}
