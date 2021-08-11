using System;
using UnityEngine;
using UnityEngine.UI;

namespace VRKeyboard
{
    public class KeyboardUtilityButton : MonoBehaviour
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