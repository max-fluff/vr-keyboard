#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace OmegaVRKeyboard
{
    public partial class VRKeyboard
    {
        [Header("Keyboard Generating Properties")]
        [SerializeField] private string[] lettersRows;
        [SerializeField] private KeyboardLetterButton letterPrefab;
        [SerializeField] private KeyboardUtilityButton shiftPrefab;
        [SerializeField] private KeyboardUtilityButton deletePrefab;
        [SerializeField] private KeyboardUtilityButton languagePrefab;
        [SerializeField] private KeyboardUtilityButton symbolsPrefab;
        [SerializeField] private KeyboardUtilityButton enterPrefab;
        [SerializeField] private KeyboardLetterButton spaceBarPrefab;
        [SerializeField] private GameObject backgroundGameObject;

        [ContextMenu("GenerateKeyboard")]
        private void GenerateKeyboard()
        {
            var keysetPrefab = Resources.Load<LetterKeyset>("KeySetPrefab");
            var keysetInstance = Instantiate(keysetPrefab, backgroundGameObject.transform);

            var rowPrefab = Resources.Load<GameObject>("RowPrefab");

            for (var i = 0; i < lettersRows.Length - 1; i++)
            {
                var rowInstance = Instantiate(rowPrefab, keysetInstance.transform);
                var charSet = lettersRows[i].ToCharArray();
                foreach (var letter in charSet)
                {
                    var letterButton = Instantiate(letterPrefab, rowInstance.transform);
                    letterButton.SetLetter(letter.ToString());
                }
            }

            var penultimateRowInstance = Instantiate(rowPrefab, keysetInstance.transform);
            var lastCharSet = lettersRows[lettersRows.Length - 1].ToCharArray();
            var shiftButton = Instantiate(shiftPrefab, penultimateRowInstance.transform);
            foreach (var letter in lastCharSet)
            {
                var letterButton = Instantiate(letterPrefab, penultimateRowInstance.transform);
                letterButton.SetLetter(letter.ToString());
            }

            var deleteButton = Instantiate(deletePrefab, penultimateRowInstance.transform);

            var lastRowInstance = Instantiate(rowPrefab, keysetInstance.transform);
            var languageButton = Instantiate(languagePrefab, lastRowInstance.transform);
            var symbolsButton = Instantiate(symbolsPrefab, lastRowInstance.transform);
            Instantiate(letterPrefab, lastRowInstance.transform).SetLetter("@");
            Instantiate(spaceBarPrefab, lastRowInstance.transform);
            Instantiate(letterPrefab, lastRowInstance.transform).SetLetter(".");
            var enterButton = Instantiate(enterPrefab, lastRowInstance.transform);

            keysetInstance.SetUtilityButtons(enterButton, shiftButton, deleteButton, languageButton, symbolsButton);
            lettersKeysets.Add(keysetInstance);
        }
    }
}
#endif