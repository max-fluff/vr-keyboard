using UnityEngine;

namespace OmegaVRKeyboard
{
    public class VRLetterKeyset : MonoBehaviour
    {
        [SerializeField] private VRKeyboardButton shift;
        [SerializeField] private VRKeyboardButton delete;
        [SerializeField] private VRKeyboardButton switchLanguage;
        [SerializeField] private VRKeyboardButton symbols;
        [SerializeField] private VRKeyboardButton enter;

        private VRKeyboard _keyboard;
        private VRKeyboardLetter[] _letters;
        private bool _isUpperCase = true;

        private void Awake()
        {
            _keyboard = GetComponentInParent<VRKeyboard>();
            _letters = GetComponentsInChildren<VRKeyboardLetter>();

            foreach (var letter in _letters)
            {
                letter.OnClick += _keyboard.Write;
            }

            delete.OnClick += _keyboard.Delete;
            enter.OnClick += _keyboard.Return;
            switchLanguage.OnClick += _keyboard.SwitchLanguage;
            symbols.OnClick += _keyboard.EnableSymbols;
            shift.OnClick += ChangeCase;
        }

        private void ChangeCase()
        {
            if (_isUpperCase)
            {
                foreach (var letter in _letters)
                    letter.ToLowerCase();
                _isUpperCase = false;
            }
            else
            {
                foreach (var letter in _letters)
                    letter.ToUpperCase();
                _isUpperCase = true;
            }
        }

        private void OnDestroy()
        {
            foreach (var letter in _letters)
            {
                letter.OnClick -= _keyboard.Write;
            }

            delete.OnClick -= _keyboard.Delete;
            enter.OnClick -= _keyboard.Return;
            switchLanguage.OnClick -= _keyboard.SwitchLanguage;
            symbols.OnClick -= _keyboard.EnableSymbols;
            shift.OnClick -= ChangeCase;
        }
    }
}