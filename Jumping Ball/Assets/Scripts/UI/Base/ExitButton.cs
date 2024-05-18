using UnityEngine;
using UnityEngine.UI;

namespace UI.Base
{
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(Quit);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Quit);
        }

        private void Quit()
        {
            Application.Quit();
        }
    }
}
