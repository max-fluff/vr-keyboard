using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace VRKeyboard
{
    public partial class VRKeyboard : MonoBehaviour
    {
        [Header("Keyboard Runtime Properties")] [SerializeField]
        private List<LetterKeyset> lettersKeysets;

        [SerializeField] private SymbolKeyset symbolsKeyset;
        [SerializeField] private ReturnButtonModes returnMode;

        private TMP_InputField _inputField;

        private int _currentKeyset;
        public static VRKeyboard instance;

        private enum ReturnButtonModes
        {
            NewLine,
            CloseKeyboard
        }

        public event Action<string> OnWrite; 
        public event Action OnDelete; 
        public event Action OnReturn; 

        private void Awake()
        {
            instance = this;

            foreach (var keyboard in lettersKeysets)
                keyboard.gameObject.SetActive(false);
            symbolsKeyset.gameObject.SetActive(false);
        }

        public void EnableSymbols()
        {
            lettersKeysets[_currentKeyset].gameObject.SetActive(false);
            symbolsKeyset.gameObject.SetActive(true);
        }

        public void EnableLetters()
        {
            SelectLetterKeyset(_currentKeyset);
        }

        public void SwitchLanguage()
        {
            var nextKeyset = (_currentKeyset + 1) % lettersKeysets.Count;
            SelectLetterKeyset(nextKeyset);
        }

        private void SelectLetterKeyset(int number)
        {
            lettersKeysets[_currentKeyset].gameObject.SetActive(false);
            symbolsKeyset.gameObject.SetActive(false);
            lettersKeysets[number].gameObject.SetActive(true);
            _currentKeyset = number;
        }

        public void Write(string text)
        {
            OnWrite?.Invoke(text);
            
            if (_inputField is null) return;
            _inputField.text += $"{text}";
            if (_inputField.characterLimit > 0)
                _inputField.text = _inputField.text.Remove(_inputField.characterLimit);
        }

        public void Delete()
        {
            OnDelete?.Invoke();
            if (_inputField is null) return;
            var text = _inputField.text;
            if (text.Length > 0)
                _inputField.text = text.Remove(text.Length - 1);
        }

        public void Return()
        {
            OnReturn?.Invoke();
            
            switch (returnMode)
            {
                case ReturnButtonModes.NewLine:
                    if (!(_inputField is null))
                        _inputField.text += "\n";
                    break;
                case ReturnButtonModes.CloseKeyboard:
                    HideKeyboard();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void ShowKeyboard(TMP_InputField tmpInputField)
        {
            if (instance is null)
            {
                Debug.LogError("Could not find keyboard instance on scene");
                return;
            }

            if (tmpInputField == instance._inputField) return;

            HideKeyboard();
            instance.lettersKeysets[instance._currentKeyset].gameObject.SetActive(true);
            instance._inputField = tmpInputField;
        }

        public static void HideKeyboard()
        {
            if (instance is null)
            {
                Debug.LogError("Could not find keyboard instance on scene");
                return;
            }

            if (!(instance._inputField is null))
                instance._inputField.DeactivateInputField();

            foreach (var keyboard in instance.lettersKeysets)
                keyboard.gameObject.SetActive(false);
            instance.symbolsKeyset.gameObject.SetActive(false);

            instance._inputField = null;
        }

        private void OnDestroy()
        {
            instance = null;
        }
    }
}