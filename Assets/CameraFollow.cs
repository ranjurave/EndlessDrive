using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] Transform player;
    [SerializeField] Vector3 cameraOffset;

    void Update() {
        Vector3 targetPosition = player.transform.position + cameraOffset;
        targetPosition.x = 0;
        transform.position = targetPosition;
    }
}
