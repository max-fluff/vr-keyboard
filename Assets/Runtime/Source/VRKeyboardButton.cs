using System;
using UnityEngine;
using UnityEngine.UI;

namespace OmegaVRKeyboard
{
    public class VRKeyboardButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        public event Action OnClick;

        private void Awake()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            OnClick?.Invoke();
        }
    }
}
