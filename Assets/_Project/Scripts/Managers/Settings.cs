using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public static class Settings
    {
        private static ResourcesManager _resourcesManager;

        public static ResourcesManager GetResourcesManager()
        {
            if (_resourcesManager == null)
            {
                _resourcesManager = (ResourcesManager) Resources.Load("ResourcesManager");
                
            }
            return _resourcesManager;
        }
        

    }
}
