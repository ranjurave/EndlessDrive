using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {
    PlayerMovements playerMovement;
    void Start() {
        playerMovement = FindFirstObjectByType<PlayerMovements>();
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            playerMovement.Coin();

            //Car Colour change feedback
            playerMovement.carRenderer.material.EnableKeyword("_EMISSION");
            Color carColor = playerMovement.carRenderer.material.GetColor("_EmissionColor");
            carColor += new Color(0, 0.2f, 0);
            playerMovement.carRenderer.material.SetColor("_EmissionColor", carColor);
            playerMovement.ResetColour();
        }
        Destroy(gameObject);
    }
}
