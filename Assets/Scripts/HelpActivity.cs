using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Scripts.Scenes;
using Scripts.Database;

namespace Scripts.HelpActivity
{
    public class HelpActivity : MonoBehaviour
    {
        public Button backButton;
        public Button prevButton;
        public Button nextButton;

        private List<string> images;
        private int currentIndex;

        private void Start()
        {
            images = Database.Database.GetImagesTableData();
            backButton.onClick.AddListener(LoginView);
            prevButton.onClick.AddListener(LoadPreviousImage);
            nextButton.onClick.AddListener(LoadNextImage);
        }

        private void LoginView()
        {
            SceneManager.LoadScene((int)GameScene.LOGIN);
        }

        private void LoadPreviousImage()
        {

        }

        private void LoadNextImage()
        {

        }

        private void Update()
        {
            
        }
    }
}