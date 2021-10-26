using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour {
    [SerializeField] GameObject groundTile;
    [SerializeField] int tileNumber = 5;
    [SerializeField] GameObject[] obstacle;
    Vector3 nextSpawnPoint;

    void Start() {
        for (int i = 0; i < tileNumber; i++) {
            SpawnTile();
        }
    }

    public void SpawnTile() {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(0).transform.position;
        SpawnObstacle(temp);
    }

    void SpawnObstacle(GameObject parentObj) {
        for (int i = 0; i < 5; i++) {
            Vector3 position = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(0, 45)) + parentObj.transform.position;
            int obstacleType = Random.Range(0, 2);
            GameObject obstables = Instantiate(obstacle[obstacleType], position, Quaternion.Euler(0, 180, 0));
            obstables.transform.parent = parentObj.transform;
        }
    }
}
