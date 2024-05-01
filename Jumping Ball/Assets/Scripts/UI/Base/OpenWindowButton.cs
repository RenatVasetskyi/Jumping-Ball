using UnityEngine;

namespace UI.Base
{
    public class OpenWindowButton : WindowControlButton
    {
        public override void Control()
        {
            base.Control();

            foreach (GameObject window in _windows)
            {
                AnimatedWindow[] animatedWindows = window.GetComponentsInChildren<AnimatedWindow>();

                if (animatedWindows != null && animatedWindows.Length > 0)
                {
                    foreach (AnimatedWindow animatedWindow in animatedWindows)
                    {
                        animatedWindow.Open();    
                    }
                }
                else
                {
                    window.SetActive(true);
                }
            }
        }
    }
}