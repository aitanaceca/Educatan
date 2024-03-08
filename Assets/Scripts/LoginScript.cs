using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Scripts.Scenes;
using Scripts.Database;

namespace Scripts.LoginScript
{
    public class LoginScript : MonoBehaviour
    {
        [SerializeField] private GameObject _nameInput;
        [SerializeField] private GameObject _ageInput;
        [SerializeField] private GameObject _nameError;
        [SerializeField] private GameObject _ageError;
        [SerializeField] private Button _loginButton;
        [SerializeField] private Button _helpButton;

        public GameObject NameInput
        {
            get { return _nameInput; }
            set { _nameInput = value; }
        }

        public GameObject AgeInput
        {
            get { return _ageInput; }
            set { _ageInput = value; }
        }

        public GameObject NameError
        {
            get { return _nameError; }
            set { _nameError = value; }
        }

        public GameObject AgeError
        {
            get { return _ageError; }
            set { _ageError = value; }
        }

        public Button LoginButton
        {
            get { return _loginButton; }
            set { _loginButton = value; }
        }

        public Button HelpButton
        {
            get { return _helpButton; }
            set { _helpButton = value; }
        }

        private TMP_InputField nameInputField;
        private TMP_InputField ageInputField;

        private TMP_Text nameErrorText;
        private TMP_Text ageErrorText;

        private void Start()
        {
            _nameInput = NameInput;
            _ageInput = AgeInput;
            _nameError = NameError;
            _ageError = AgeError;
            _loginButton = LoginButton;
            _helpButton = HelpButton;

            nameInputField = _nameInput.GetComponent<TMP_InputField>();
            ageInputField = _ageInput.GetComponent<TMP_InputField>();
            nameErrorText = _nameError.GetComponent<TMP_Text>();
            ageErrorText = _ageError.GetComponent<TMP_Text>();
            nameErrorText.enabled = false;
            ageErrorText.enabled = false;
            _loginButton.onClick.AddListener(ValidateLogin);
            _helpButton.onClick.AddListener(HelpView);
        }

        private void HelpView()
        {
            Database.Database database = Database.Database.InitializeDatabase("Database.db");
            database.CreateTables();
            SceneManager.LoadScene((int)GameScene.HELP);
        }

        private void ValidateLogin()
        {
            string name = nameInputField.text;
            string age = ageInputField.text;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(age))
            {
                print("Login Success");
                SceneManager.LoadScene((int)GameScene.MAIN);
            }
            else
            {
                print("Wrong login");
            }
            if (string.IsNullOrEmpty(name))
            {
                print("Name empty");
                this.nameErrorText.enabled = true;
            }
            if (string.IsNullOrEmpty(age))
            {
                print("Age empty");
                this.ageErrorText.enabled = true;
            }
        }

        private void DisableNameErrorText()
        {
            this.nameErrorText.enabled = false;
        }

        private void DisableAgeErrorText()
        {
            this.ageErrorText.enabled = false;
        }

        private void Update()
        {
            if (nameInputField.isFocused)
            {
                DisableNameErrorText();
            }
            if (ageInputField.isFocused)
            {
                DisableAgeErrorText();
            }
        }
    }
}