namespace Deckbuilder
{
    public interface IStateMachine<in T>
    {
        public void CheckForStateChange();

        public void SetState(T state);
    }
}