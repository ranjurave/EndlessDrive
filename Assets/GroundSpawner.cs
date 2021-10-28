using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour {
    [SerializeField] GameObject groundTile;
    [SerializeField] int tileNumber = 5;
    [SerializeField] GameObject[] obstacle;
    [SerializeField] int policePerTile = 3;
    [SerializeField] int petrolPerTile = 3;
    [SerializeField] int coinPerTile = 1;
    Vector3 nextSpawnPoint;

    void Start() {
        for (int i = 0; i < tileNumber; i++) {
            SpawnTile();
        }
    }

    public void SpawnTile() {
        GameObject worldTile = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = worldTile.transform.GetChild(0).transform.position;
        SpawnObstacle(worldTile);
    }

    void SpawnObstacle(GameObject parentObj) {
        for (int i = 0; i < obstacle.Length; i++) {

            string obstacleName = obstacle[i].name;

            switch (obstacleName) {
                case "Police":
                    SpawnObstacle(obstacle[i], parentObj, policePerTile);
                    break;
                case "Pump":
                    SpawnObstacle(obstacle[i], parentObj, petrolPerTile);
                    break;
                case "Coin":
                    int toSpawn = Random.Range(-1, 2);
                    if (toSpawn == 1) {
                    SpawnObstacle(obstacle[i], parentObj, coinPerTile);
                    }
                    break;
                default:
                    break;
            }
        }
    }
    public void SpawnObstacle(GameObject objectToSpawn, GameObject parentObj, int count) {
        for (int i = 0; i < count; i++) {
            Vector3 position = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(0, 45)) + parentObj.transform.position;
            GameObject obstablesSpawned = Instantiate(objectToSpawn, position, Quaternion.Euler(0, 180, 0));
            obstablesSpawned.transform.parent = parentObj.transform;
        }
    }
}
