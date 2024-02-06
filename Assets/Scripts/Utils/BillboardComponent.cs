using System;
using UnityEngine;

namespace Utils
{
    public class BillboardComponent : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        }
    }
}