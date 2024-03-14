using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface ICameraPathFollower : IService
    {
        public void FollowToPoint(Transform point);
        public void FollowToStart();
    }
}