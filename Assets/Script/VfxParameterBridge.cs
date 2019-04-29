using UnityEngine;
using UnityEngine.Experimental.VFX;

namespace Evp3
{
    sealed class VfxParameterBridge : MonoBehaviour
    {
        public float parameterValue {
            set { _target.SetFloat(gameObject.name, value); }
        }

        VisualEffect _target;

        void Start()
        {
            _target = transform.parent.GetComponent<VisualEffect>();
        }
    }
}
