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
        public Image helpImage;
        public Button backButton;
        public Button prevButton;
        public Button nextButton;

        private static List<string> images;
        private int currentIndex = 0;
        private const int MIN_INDEX = 0;
        private const int MAX_INDEX = 5;
        private static Image image;

        private void Start()
        {
            image = helpImage;
            images = Database.Database.GetImagesTableData();
            LoadImage(currentIndex);
            backButton.onClick.AddListener(LoginView);
            prevButton.onClick.AddListener(LoadPreviousImage);
            nextButton.onClick.AddListener(LoadNextImage);
        }

        private void LoginView()
        {
            SceneManager.LoadScene((int)GameScene.LOGIN);
        }

        private void LoadPreviousImage() {
            if (currentIndex > MIN_INDEX) {
                currentIndex = currentIndex - 1;
            }
            LoadImage(currentIndex);
        }

        private void LoadNextImage() {
            if (currentIndex < MAX_INDEX) {
                currentIndex = currentIndex + 1;
            }
            LoadImage(currentIndex);
        }

        private static void LoadImage(int index)
        {
            string imageInBase64 = (images[index]).Substring(22);
            byte[] imageInBase64ToBytes = System.Convert.FromBase64String(imageInBase64);
            Texture2D newTexture = new Texture2D(1, 1);
            newTexture.LoadImage(imageInBase64ToBytes);
            Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.one * 0.5f);
            image.sprite = newSprite;
        }

        private void Update()
        {
            helpImage = image;
        }
    }
}

