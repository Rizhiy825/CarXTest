using UnityEngine;
using Utils;

namespace Infrastructure.AssetManagement
{
    public class CameraPathFollower : MonoBehaviour, ICameraPathFollower
    {
        private const float CameraStopDistance = 0.05f;
        private const float CameraSpeed = 5f;
        
        // Position of the camera at the start of the game
        private Transform startPosition;
        private Transform followingCamera;
        private Transform target;

        // Position of the camera at the start of the movement to the target
        private Transform moveStartingPos;
        private float pathLenght;

        public void Construct(Transform cameraStartPosition, Camera camera)
        {
            startPosition = cameraStartPosition;
            
            followingCamera = camera.transform;
            followingCamera.SetPositionAndRotation(startPosition.position, startPosition.rotation);
        }

        public void FollowToStart() => 
            FollowToPoint(startPosition);

        public void FollowToPoint(Transform point)
        {
            target = point;
            pathLenght = Vector3.Distance(followingCamera.position, point.position);
            moveStartingPos = followingCamera.transform;
        }

        private void Update()
        {
            if (target == null || moveStartingPos == null || pathLenght == 0)
                return;

            var passedLenght = GetPassedLenght();
            var passedCoef = Mathf.Clamp01(passedLenght.Remap(0, pathLenght, 0, 1) + Time.deltaTime * CameraSpeed);
            var smoothedCoef = Mathf.SmoothStep(0, 1, passedCoef);
            
            var newPosition = Vector3.Lerp(moveStartingPos.position, target.position, smoothedCoef);
            var newRotation = Quaternion.Lerp(moveStartingPos.rotation, target.rotation, smoothedCoef);
                
            followingCamera.position = newPosition;
            followingCamera.rotation = newRotation;
                
            if (Vector3.Distance(followingCamera.transform.position, target.position) < CameraStopDistance)
            {
                target = null;
                moveStartingPos = null;
                pathLenght = 0;
            }
        }
        
        private float GetPassedLenght() => 
            Vector3.Distance(followingCamera.position, moveStartingPos.position);
    }
}