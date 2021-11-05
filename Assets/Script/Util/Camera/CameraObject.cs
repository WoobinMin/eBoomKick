using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
    CameraMovementStrategy cameraMovementStrategy;
    public Transform playerTrans;

    void Start()
    {
        cameraMovementStrategy = new FollowTargetCameraMovementStrategy(this, playerTrans, Vector2.up * 3f, 3f);
    }

    void Update()
    {
        cameraMovementStrategy.Moving();
    }
}
