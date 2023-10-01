namespace CodeBase.Gameplay.Services.InteractionsService
{
    public interface IInteractionService
    {
        public void Initialize();
        
        public bool IsInteract { get; }
    }
}