using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;
using Klak.Motion;

namespace Evp3
{
    sealed class CameraController : MonoBehaviour
    {
        [SerializeField] BrownianMotion [] _motionNodes = null;

        List<(BrownianMotion, float3, float3)> _motions;

        public float motionAmount { set { 
            foreach (var m in _motions)
            {
                m.Item1.positionAmount = m.Item2 * value;
                m.Item1.rotationAmount = m.Item3 * value;
            }
        } }

        void Start()
        {
            _motions = new List<(BrownianMotion, float3, float3)>();

            foreach (var node in _motionNodes)
            {
                _motions.Add((node, node.positionAmount, node.rotationAmount));
                node.positionAmount = node.rotationAmount = 0;
            }
        }
    }
}
