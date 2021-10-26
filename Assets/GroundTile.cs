using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour {
    GroundSpawner groundSpawner;
    ObstacleSpawner obstacleSpawner;
    void Start() {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        obstacleSpawner = GetComponent<ObstacleSpawner>();
    }

    private void OnTriggerExit(Collider other) {
        groundSpawner.SpawnTile();
        Destroy(gameObject, 2);
    }
}
