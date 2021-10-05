using UnityEngine;

namespace VRKeyboard
{
    public class SymbolKeyset : MonoBehaviour
    {
        [SerializeField] private KeyboardUtilityButton delete;
        [SerializeField] private KeyboardUtilityButton letters;
        [SerializeField] private KeyboardUtilityButton enter;

        private Keyboard _keyboard;

        private void Awake()
        {
            _keyboard = GetComponentInParent<Keyboard>();
            var symbols = GetComponentsInChildren<KeyboardLetterButton>();

            foreach (var symbol in symbols)
                symbol.OnClick += _keyboard.Write;

            delete.OnClick += _keyboard.Delete;
            enter.OnClick += _keyboard.Return;
            letters.OnClick += _keyboard.EnableLetters;
        }

        private void OnDestroy()
        {
            var symbols = GetComponentsInChildren<KeyboardLetterButton>();

            foreach (var symbol in symbols)
                symbol.OnClick -= _keyboard.Write;

            delete.OnClick -= _keyboard.Delete;
            enter.OnClick -= _keyboard.Return;
            letters.OnClick -= _keyboard.EnableLetters;
        }
    }
}