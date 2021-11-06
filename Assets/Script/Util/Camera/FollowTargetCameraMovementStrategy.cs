using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetCameraMovementStrategy : CameraMovementStrategy
{
    private CameraObject cameraObject;
    private Transform target;
    private float halfWidth;
    private float halfHeight;

    public Vector2 offset;
    public float speed;

    public FollowTargetCameraMovementStrategy(CameraObject cameraObject, Transform target, Vector2 offset, float speed)
    {
        this.cameraObject = cameraObject;
        this.target = target;
        this.offset = offset;
        this.speed = speed;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }


    public void Moving()
    {
        Vector3 targetPosition = Vector3.zero;

        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.cameraObject.transform.position.z);

            this.cameraObject.transform.position = Vector3.Lerp(this.cameraObject.transform.position, targetPosition, speed * Time.deltaTime);

            var clampX = Mathf.Clamp(this.cameraObject.transform.position.x, this.cameraObject.coll.bounds.min.x + halfWidth, this.cameraObject.coll.bounds.max.x - halfWidth);
            var clampY = Mathf.Clamp(this.cameraObject.transform.position.y, this.cameraObject.coll.bounds.min.y + halfHeight, this.cameraObject.coll.bounds.max.y - halfHeight);

            this.cameraObject.transform.position = new Vector3(clampX, clampY, this.cameraObject.transform.position.z);
        }
    }
}
