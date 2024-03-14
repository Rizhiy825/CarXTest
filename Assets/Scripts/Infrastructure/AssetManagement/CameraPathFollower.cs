using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class CameraPathFollower : MonoBehaviour, ICameraPathFollower
    {
        private Transform startPosition;
        private Transform followingCamera;
        private Transform target;

        public void Construct(Transform cameraStartPosition, Camera camera)
        {
            startPosition = cameraStartPosition;
            
            followingCamera = camera.transform;
            followingCamera.SetPositionAndRotation(startPosition.position, startPosition.rotation);
        }
        
        public void FollowToPoint(Transform point)
        {
            target = point;
        }

        public void FollowToStart()
        {
            target = startPosition;
        }

        private void Update()
        {
            if (target == null)
                return;
            
            var newPosition = Vector3.Lerp(followingCamera.position, target.position, Time.deltaTime);
            var newRotation = Quaternion.Lerp(followingCamera.rotation, target.rotation, Time.deltaTime);
                
            followingCamera.position = newPosition;
            followingCamera.rotation = newRotation;
                
            if (Vector3.Distance(followingCamera.transform.position, target.position) < 0.1f)
            {
                target = null;
            }
        }
    }
}