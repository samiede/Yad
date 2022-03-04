namespace Deckbuilder
{
    public interface IGenericGameEvent
    {

        void Raise();
        void AddListener(GenericGameEventListener listener);
        
        void RemoveListener(GenericGameEventListener listener);
        void RemoveAll();
    }
    
    
}