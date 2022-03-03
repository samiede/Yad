namespace Deckbuilder
{
    public interface IStateMachine<T>
    {
        public void CheckForStateChange();

        public void SetState(T state);
    }
}