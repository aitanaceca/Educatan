using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Scripts.Scenes;

public class LoginScript : MonoBehaviour
{
    public GameObject nameInput;
    public GameObject ageInput;
    public GameObject nameError;
    public GameObject ageError;
    public Button loginButton;
    public Button helpButton;

    private TMP_InputField nameInputField;
    private TMP_InputField ageInputField;

    private TMP_Text nameErrorText;
    private TMP_Text ageErrorText;

    private void Start()
    {
        nameInputField = nameInput.GetComponent<TMP_InputField>();
        ageInputField = ageInput.GetComponent<TMP_InputField>();
        nameErrorText = nameError.GetComponent<TMP_Text>();
        ageErrorText = ageError.GetComponent<TMP_Text>();
        nameErrorText.enabled = false;
        ageErrorText.enabled = false;
        loginButton.onClick.AddListener(ValidateLogin);
        helpButton.onClick.AddListener(HelpView);

    }

    private void HelpView() {
        SceneManager.LoadScene((int)GameScene.HELP);
    }

    private void ValidateLogin() {
        string name = nameInputField.text;
        string age = ageInputField.text;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(age)) {
                print("Login Success");
                SceneManager.LoadScene((int)GameScene.MAIN);
            } else {
                print("Wrong login");
            }
            if (string.IsNullOrEmpty(name)) {
                print("Name empty");
                this.nameErrorText.enabled = true;
            }
            if (string.IsNullOrEmpty(age)) {
                print("Age empty");
                this.ageErrorText.enabled = true;
            }
    }

    private void DisableNameErrorText() {
        this.nameErrorText.enabled = false;
    }

    private void DisableAgeErrorText() {
        this.ageErrorText.enabled = false;
    }

    private void Update() {
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
