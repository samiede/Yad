using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Helpers/HealthBarCreator")]
    public class HealthBarCreator : ScriptableObject
    {
        [SerializeField] private HealthBar _healthBarPrefab;
        private Canvas _mainCamera;
        private Camera _uiCamera;
        

        public void Initialize(Canvas canvas, Camera camera)
        {
            _mainCamera = canvas;
            _uiCamera = camera;
        }

        public HealthBar CreateHealthBar()
        {
            var hpBar = Instantiate(_healthBarPrefab, Vector3.zero, Quaternion.identity);
            hpBar.transform.SetParent(_mainCamera.transform, false);
            hpBar.SetCamera(_uiCamera, _mainCamera.GetComponent<RectTransform>());
            return hpBar;

        }
        
        

    }
}
