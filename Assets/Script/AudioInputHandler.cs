using UnityEngine;
using UnityEngine.Events;

namespace Evp3
{
    sealed class AudioInputHandler : MonoBehaviour
    {
        #region Editable attributes

        [System.Serializable] class OutputEvent : UnityEvent<float> {}

        [SerializeField] AnimationCurve _envelope = null;
        [SerializeField] KeyCode _triggerKey = KeyCode.Z;
        [Space, SerializeField] OutputEvent _target = null;

        #endregion

        #region Public properties and methods

        public float input { set {
            _externalValue = value;
            InvokeEvent();
        }}

        public bool mute { get; set; }

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
            _target.Invoke(mute ? 0 : Mathf.Max(_externalValue, env));
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
