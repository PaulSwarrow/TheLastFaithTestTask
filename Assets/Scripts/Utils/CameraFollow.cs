using System;
using UnityEngine;

namespace Utils
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        private float _y;
        [SerializeField] private float _easing = 10;

        private void Awake()
        {
            _y = transform.position.y;
        }

        private void LateUpdate()
        {
            var targetPosition = new Vector3(_target.position.x, _y, _target.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, _easing * Time.deltaTime);
        }
    }
}