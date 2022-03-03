using UnityEngine;

namespace Deckbuilder
{
    public interface IRayProvider
    {
        Ray CreateRay();
    }
}