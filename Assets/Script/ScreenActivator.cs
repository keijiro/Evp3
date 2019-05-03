using UnityEngine;

namespace Evp3
{
    sealed class ScreenActivator : MonoBehaviour
    {
        void Start()
        {
            if (!Application.isEditor)
            {
                TryActivateDisplay(0);
                TryActivateDisplay(1);
            }
        }

        void TryActivateDisplay(int index)
        {
            if (index < Display.displays.Length)
                Display.displays[index].Activate();
        }
    }
}
