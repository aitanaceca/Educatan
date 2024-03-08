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
        [SerializeField] private Image _helpImage;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _prevButton;
        [SerializeField] private Button _nextButton;

        public Image HelpImage
        {
            get { return _helpImage; }
            set { _helpImage = value; }
        }

        public Button BackButton
        {
            get { return _backButton; }
            set { _backButton = value; }
        }

        public Button PrevButton
        {
            get { return _prevButton; }
            set { _prevButton = value; }
        }

        public Button NextButton
        {
            get { return _nextButton; }
            set { _nextButton = value; }
        }

        private List<string> images;
        private int currentIndex = 0;
        private const int MIN_INDEX = 0;
        private const int MAX_INDEX = 5;
        private Image image;

        private void Start()
        {
            _helpImage = HelpImage;
            Database.Database database = Database.Database.InitializeDatabase("Database.db");
            image = _helpImage;
            images = database.GetImagesTableData();
            LoadImage(currentIndex);
            BackButton.onClick.AddListener(LoginView);
            PrevButton.onClick.AddListener(LoadPreviousImage);
            NextButton.onClick.AddListener(LoadNextImage);
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

        private void LoadImage(int index)
        {
            string imageInBase64 = (this.images[index]).Substring(22);
            byte[] imageInBase64ToBytes = System.Convert.FromBase64String(imageInBase64);
            Texture2D newTexture = new Texture2D(1, 1);
            newTexture.LoadImage(imageInBase64ToBytes);
            Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.one * 0.5f);
            this.image.sprite = newSprite;
        }

        private void Update()
        {
            _helpImage = image;
        }
    }
}

