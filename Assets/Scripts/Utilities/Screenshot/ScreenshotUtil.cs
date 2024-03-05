using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Utilities.Screenshot
{
    public class ScreenshotUtil : MonoBehaviour
    {
        public List<Tuple<DateTime, Sprite>> ScreenshotData { get; private set; }

        private bool _pressed;
        private string _fileDirectory;

        private const string FileNameFormat = "yyyy_MM_dd HH-mm-ss";

        private void Start()
        {
            // Get directory name.
            _fileDirectory = Path.Combine(Application.persistentDataPath, "Screenshots");

            // Check if the directory exits, if not create it.
            if (!Directory.Exists(_fileDirectory))
                Directory.CreateDirectory(_fileDirectory);
            else
                ReadData();

            ScreenshotData = new List<Tuple<DateTime, Sprite>>();
        }

        private void ReadData()
        {
            StartCoroutine(ReadDataAsync());
        }

        private IEnumerator ReadDataAsync()
        {
            foreach (var path in Directory.GetFiles(_fileDirectory, "*.png"))
            {
                var fileName = Path.GetFileName(path).Replace(".png", "");
                if (!DateTime.TryParseExact(fileName, FileNameFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var captureDateTime))
                    continue;

                var textureRequest = UnityWebRequestTexture.GetTexture(path);
                yield return textureRequest.SendWebRequest();

                if (textureRequest.result != UnityWebRequest.Result.Success)
                    continue;

                var texture = DownloadHandlerTexture.GetContent(textureRequest);
                var sprite = GetSprite(texture);

                ScreenshotData.Add(new Tuple<DateTime, Sprite>(captureDateTime, sprite));
            }
        }

        private static Sprite GetSprite(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, 1920, 1080), Vector2.zero);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X) && !_pressed)
            {
                _pressed = true;

                // Take the screenshot & save the screenshot.
                TakeScreenshot();
            }

            if (Input.GetKeyUp(KeyCode.X))
                _pressed = false;
        }

        public void TakeScreenshot()
        {
            StartCoroutine(TakeScreenshotAsync());
        }

        private IEnumerator TakeScreenshotAsync()
        {
            yield return new WaitForEndOfFrame();

            // Capture the screenshot.
            var screenshot = ScreenCapture.CaptureScreenshotAsTexture(1);
            var sprite = GetSprite(screenshot);

            var screenshotData = new Tuple<DateTime, Sprite>(DateTime.Now, sprite);
            ScreenshotData.Add(screenshotData);

            // Save the screenshot.
            SaveScreenshot(screenshotData);
        }

        private void SaveScreenshot(Tuple<DateTime, Sprite> screenshotData)
        {
            SaveScreenshot(screenshotData.Item2.texture, screenshotData.Item1);
        }

        private void SaveScreenshot(Texture2D texture, DateTime captureTime)
        {
            var fileName = captureTime.ToString(FileNameFormat, CultureInfo.InvariantCulture) + ".png";

            SaveScreenshot(texture, fileName);
        }

        private void SaveScreenshot(Texture2D texture, string fileName)
        {
            // Save the file.
            File.WriteAllBytes(Path.Combine(_fileDirectory, fileName), texture.EncodeToPNG());
        }
    }
}