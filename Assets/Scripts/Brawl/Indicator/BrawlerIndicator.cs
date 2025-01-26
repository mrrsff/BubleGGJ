using System;
using TMPro;
using UnityEngine;

namespace GGJ2025.Indicator
{
    public class BrawlerIndicator : MonoBehaviour
    {
        private GameObject _playerName;
        private GameObject _indicator;
        private Quaternion _startRotation;
        [SerializeField] private float yOffset = 0.1f;
        [SerializeField] private Vector2 padding = new(16, 16);
        [SerializeField] private float minScale = 0.25f;
        [SerializeField] private float maxScale = 1.5f;

        private float maxDistance = 50f;
        public char playerid;
        
        private void Awake()
        {
            _indicator = Instantiate(GameResources.IndicatorPrefab, GameManager.Instance.UIManager.Canvas.transform);
            _startRotation = _indicator.transform.rotation;
            _indicator.SetActive(false);
            
            _playerName = Instantiate(GameResources.PlayerNamePrefab, GameManager.Instance.UIManager.Canvas.transform);
            _playerName.SetActive(false);
        }

        private void Start()
        {
            _playerName.GetComponent<TextMeshProUGUI>().text = $"P{playerid}";
        }

        private void Update()
        {
            var screenPos = GameManager.MainCamera.WorldToScreenPoint(transform.position);
            if (IsOnScreen(screenPos))
            {
                _indicator.SetActive(false);
                _playerName.SetActive(true);
                _playerName.transform.position = new Vector3(screenPos.x, screenPos.y, 0);
                _playerName.transform.position += Vector3.up * yOffset;
            }
            else
            {
                _indicator.SetActive(true);
                _playerName.SetActive(false);
                var dir = (transform.position - GameManager.MainCamera.transform.position);
                var distance = dir.magnitude;
                dir.Normalize();
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                _indicator.transform.rotation = _startRotation * Quaternion.Euler(0, 0, angle);
                var canvasRect = GameManager.Instance.UIManager.Canvas.GetComponent<RectTransform>();
                var x = Mathf.Clamp(screenPos.x, padding.x, canvasRect.sizeDelta.x - padding.x);
                var y = Mathf.Clamp(screenPos.y, padding.y, canvasRect.sizeDelta.y - padding.y);
                _indicator.transform.position = new Vector3(x, y, 0);
                
                var scale = _indicator.transform.localScale;
                // as it gets closer to the camera, it should be bigger
                scale.x = scale.y = Mathf.Lerp(minScale, maxScale, 1 - (distance / maxDistance));
                _indicator.transform.localScale = scale;
            }
        }
        private static bool IsOnScreen(Vector3 screenPos)
        {
            return screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height && screenPos.z > 0;
        }
    }
}