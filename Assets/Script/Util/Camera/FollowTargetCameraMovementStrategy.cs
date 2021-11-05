using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetCameraMovementStrategy : CameraMovementStrategy
{
    private CameraObject cameraObject;
    private Transform target;

    public Vector2 offset;
    public float speed;

    public FollowTargetCameraMovementStrategy(CameraObject cameraObject, Transform target, Vector2 offset, float speed)
    {
        this.cameraObject = cameraObject;
        this.target = target;
        this.offset = offset;
        this.speed = speed;
    }


    public void Moving()
    {
        Vector3 targetPosition = Vector3.zero;
        targetPosition.Set(target.transform.position.x + offset.x, target.transform.position.y + offset.y, cameraObject.transform.position.z);

        this.cameraObject.transform.position = Vector3.Lerp(this.cameraObject.transform.position, targetPosition, speed * Time.deltaTime);

    }
}
