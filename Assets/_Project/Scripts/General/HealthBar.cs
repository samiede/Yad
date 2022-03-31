using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deckbuilder
{
    public class HealthBar : MonoBehaviour
    {

        public RectTransform targetCanvas;
        public RectTransform healthBar;
        public Transform objectToFollow;
        public Vector3 offset;
        public Slider slider;
        private Camera _camera;

        private void Awake()
        {
            healthBar = GetComponent<RectTransform>();
            slider = GetComponent<Slider>();        
        }
        
        public void SetCamera(Camera camera, RectTransform canvas)
        {
            targetCanvas = canvas;
            _camera = camera;

        }
        

        public void InitWithUnitData(Transform targetTransform, float maxHp)
        {
            offset = new Vector3(0, -0.5f, 0);
            objectToFollow = targetTransform;
            slider.maxValue = maxHp;
        }

        public void OnHealthChanged(float healthFill)
        {
            slider.value = healthFill;
        }

        private void LateUpdate()
        {
            Vector2 viewportPosition = _camera.WorldToViewportPoint(objectToFollow.position + offset);
            Vector2 worldObjectScreenPosition = new Vector2(
                ((viewportPosition.x * targetCanvas.sizeDelta.x) - (targetCanvas.sizeDelta.x * 0.5f)),
                ((viewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * 0.5f)));
            healthBar.anchoredPosition = worldObjectScreenPosition;

        }
    }
}
