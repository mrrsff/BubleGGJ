using System;
using UnityEngine;

namespace GGJ2025.Indicator
{
    public class BrawlerIndicator : MonoBehaviour
    {
        private GameObject _indicator;
        private Vector3 startRotation;
        [SerializeField] private Vector2 padding = new(16, 16);
        private void Start()
        {
            _indicator = Instantiate(GameResources.IndicatorPrefab, GameManager.Instance.UIManager.Canvas.transform);
            startRotation = _indicator.transform.rotation.eulerAngles;
            _indicator.SetActive(false);
        }
        private void Update()
        {
            var screenPos = GameManager.MainCamera.WorldToScreenPoint(transform.position);
            if (IsOnScreen(screenPos))
            {
                _indicator.SetActive(false);
            }
            else
            {
                _indicator.SetActive(true);
                var dir = (transform.position - GameManager.MainCamera.transform.position).normalized;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                _indicator.transform.rotation = Quaternion.Euler(0, 0, angle + startRotation.z);
                var canvasRect = GameManager.Instance.UIManager.Canvas.GetComponent<RectTransform>();
                var x = Mathf.Clamp(screenPos.x, padding.x, canvasRect.sizeDelta.x - padding.x);
                var y = Mathf.Clamp(screenPos.y, padding.y, canvasRect.sizeDelta.y - padding.y);
                _indicator.transform.position = new Vector3(x, y, 0);
            }
        }
        private static bool IsOnScreen(Vector3 screenPos)
        {
            return screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height && screenPos.z > 0;
        }
    }
}