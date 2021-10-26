using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {
    [SerializeField] GameObject[] obstacle;
    private void Start() {
        SpawnObstacle();
    }

    public void SpawnObstacle() {
        for (int i = 0; i < 5; i++) {

            Vector3 position = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(0, 45)) + transform.position;
            int obstacleType = Random.Range(0, 2);
            GameObject obstables = Instantiate(obstacle[obstacleType], position, Quaternion.Euler(0,180,0));
            
        }
    }
}
