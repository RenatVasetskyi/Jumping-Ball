using UnityEngine;

namespace UI.Base
{
    public class ChangeWindowButton : WindowControlButton
    {
        [SerializeField] private WindowControlButton[] _windowControlButtons;
        
        public override void Control()
        {
            base.Control();

            foreach (WindowControlButton button in _windowControlButtons)
                button.Control();
        }
    }
}