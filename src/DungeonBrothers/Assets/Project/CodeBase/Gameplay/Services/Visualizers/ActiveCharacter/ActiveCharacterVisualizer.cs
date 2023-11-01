using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Characters.View.Outline;
using Project.CodeBase.Gameplay.Services.TurnQueue;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;

namespace Project.CodeBase.Gameplay.Services.Visualizers.ActiveCharacter
{
    public class ActiveCharacterVisualizer : IActiveCharacterVisualizer
    {
        private readonly ITurnQueue _turnQueue;
        private readonly CharacterOutlineColors _characterOutlineColors;

        private readonly CompositeDisposable _disposable = new();

        private ICharacterOutline _visualizedCharacterOutline;

        public ActiveCharacterVisualizer(ITurnQueue turnQueue, IStaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _characterOutlineColors = staticDataProvider.CharacterOutlineColors;
        }

        public void Initialize()
        {
            _turnQueue.ActiveCharacter
                .Skip(1)
                .Subscribe(OutlineActiveCharacter)
                .AddTo(_disposable);
        }

        public void CleanUp() => 
            _disposable.Clear();

        private void OutlineActiveCharacter(ICharacter character)
        {
            _visualizedCharacterOutline?.SwitchOutLine(false);

            ICharacterOutline characterOutline = character.View.CharacterOutline;
            characterOutline.ChangeColor(_characterOutlineColors.Active);
            characterOutline.SwitchOutLine(true);

            _visualizedCharacterOutline = characterOutline;
        }
    }
}