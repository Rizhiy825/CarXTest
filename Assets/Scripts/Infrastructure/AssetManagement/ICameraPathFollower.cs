using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface ICameraPathFollower
    {
        public void FollowToPoint(Transform point);
        public void FollowToStart();
    }
}