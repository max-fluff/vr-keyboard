using UnityEngine;

namespace VRKeyboard
{
    public class LetterKeyset : MonoBehaviour
    {
        [SerializeField] private KeyboardUtilityButton shift;
        [SerializeField] private KeyboardUtilityButton delete;
        [SerializeField] private KeyboardUtilityButton switchLanguage;
        [SerializeField] private KeyboardUtilityButton symbols;
        [SerializeField] private KeyboardUtilityButton enter;

        private Keyboard _keyboard;
        private KeyboardLetterButton[] _letters;
        private bool _isUpperCase;

        private void Awake()
        {
            _keyboard = GetComponentInParent<Keyboard>();
            _letters = GetComponentsInChildren<KeyboardLetterButton>();

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

        public void SetUtilityButtons(
            KeyboardUtilityButton enterButton,
            KeyboardUtilityButton shiftButton,
            KeyboardUtilityButton deleteButton,
            KeyboardUtilityButton languageButton,
            KeyboardUtilityButton symbolsButton)
        {
            enter = enterButton;
            shift = shiftButton;
            delete = deleteButton;
            switchLanguage = languageButton;
            symbols = symbolsButton;
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