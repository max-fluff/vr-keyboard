using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace OmegaVRKeyboard
{
    public class VRKeyboardLetter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Button button;

        public event Action<string> OnClick;

        private void Awake()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        public void ToUpperCase()
        {
            label.text = label.text.ToUpper();
        }
        
        public void ToLowerCase()
        {
            label.text = label.text.ToLower();
        }

        private void OnButtonClick()
        {
            OnClick?.Invoke(label.text);
        }
    }
}
