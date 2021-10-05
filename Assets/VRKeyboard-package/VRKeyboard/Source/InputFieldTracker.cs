using TMPro;
using UnityEngine;

namespace VRKeyboard
{
    [RequireComponent(typeof(TMP_InputField))]
    public class InputFieldTracker : MonoBehaviour
    {
        private TMP_InputField _inputField;

        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            _inputField.onSelect.AddListener(_ => Keyboard.ShowKeyboard(_inputField));
        }
    }
}