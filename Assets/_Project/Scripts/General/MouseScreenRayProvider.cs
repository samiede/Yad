using UnityEngine;

namespace Deckbuilder
{
    [System.Serializable]
    public class MouseScreenRayProvider : MonoBehaviour, IRayProvider
    {
        [SerializeField] private Camera _camera;

        public Ray CreateRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
        }
    }
}