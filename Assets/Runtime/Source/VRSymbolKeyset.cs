using System;
using UnityEngine;

namespace OmegaVRKeyboard
{
    public class VRSymbolKeyset : MonoBehaviour
    {
        [SerializeField] private VRKeyboardButton delete;
        [SerializeField] private VRKeyboardButton letters;
        [SerializeField] private VRKeyboardButton enter;

        private VRKeyboard _keyboard;

        private void Awake()
        {
            _keyboard = GetComponentInParent<VRKeyboard>();
            var symbols = GetComponentsInChildren<VRKeyboardLetter>();

            foreach (var symbol in symbols)
                symbol.OnClick += _keyboard.Write;

            delete.OnClick += _keyboard.Delete;
            enter.OnClick += _keyboard.Return;
            letters.OnClick += _keyboard.EnableLetters;
        }

        private void OnDestroy()
        {
            var symbols = GetComponentsInChildren<VRKeyboardLetter>();

            foreach (var symbol in symbols)
                symbol.OnClick -= _keyboard.Write;

            delete.OnClick -= _keyboard.Delete;
            enter.OnClick -= _keyboard.Return;
            letters.OnClick -= _keyboard.EnableLetters;
        }
    }
}