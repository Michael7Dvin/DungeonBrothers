using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.View.Outline;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;

namespace CodeBase.Gameplay.Services.Visualizers.ActiveCharacter
{
    public class ActiveCharacterVisualizer : IActiveCharacterVisualizer
    {
        private readonly ITurnQueue _turnQueue;
        private readonly CharacterOutlineColors _characterOutlineColors;

        private readonly CompositeDisposable _disposable = new();

        private CharacterOutline _visualizedCharacterOutline;

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

            CharacterOutline characterOutline = character.View.CharacterOutline;
            characterOutline.ChangeColor(_characterOutlineColors.Active);
            characterOutline.SwitchOutLine(true);

            _visualizedCharacterOutline = characterOutline;
        }
    }
}