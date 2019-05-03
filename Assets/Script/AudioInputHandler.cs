using UnityEngine;
using UnityEngine.Events;

namespace Evp3
{
    sealed class AudioInputHandler : MonoBehaviour
    {
        #region Editable attributes

        [System.Serializable] class OutputEvent : UnityEvent<float> {}

        [SerializeField] bool _mute = false;
        [SerializeField] AnimationCurve _envelope = null;
        [SerializeField] KeyCode _triggerKey = KeyCode.Z;
        [Space, SerializeField] OutputEvent _target = null;

        #endregion

        #region Public properties and methods

        public float input { set {
            _externalValue = value;
            InvokeEvent();
        }}

        public bool mute { set { _mute = value; } }

        public void Trigger()
        {
            _envelopeTime = 0;
        }

        #endregion

        #region Private members

        float _externalValue = 0;
        float _envelopeTime = 1000;

        void InvokeEvent()
        {
            var env = _envelope.Evaluate(_envelopeTime);
            _target.Invoke(Mathf.Max(_mute ? 0 : _externalValue, env));
        }

        #endregion

        #region MonoBehaviour implementation

        void Update()
        {
            if (Input.GetKeyDown(_triggerKey)) Trigger();

            InvokeEvent();

            _envelopeTime += Time.deltaTime;
        }

        #endregion
    }
}
