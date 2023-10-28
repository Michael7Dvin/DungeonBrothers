﻿using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.Characters.CharacterInfo;
using _Project.CodeBase.Gameplay.Characters.View.Outline;
using _Project.CodeBase.Gameplay.Services.Attack;
using _Project.CodeBase.Gameplay.Services.Map;
using _Project.CodeBase.Gameplay.Services.Move;
using _Project.CodeBase.Gameplay.Services.PathFinder;
using _Project.CodeBase.Gameplay.Services.TurnQueue;
using _Project.CodeBase.Gameplay.Tiles;
using _Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;

namespace _Project.CodeBase.Gameplay.Services.Visualizers.Attackable
{
    public class AttackableTilesVisualizer : IAttackableTilesVisualizer
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IMoverService _moverService;
        private readonly IAttackService _attackService;
        private readonly IMapService _mapService;
        private readonly CharacterOutlineColors _characterOutlineColors;

        private readonly CompositeDisposable _disposable = new();
        private readonly List<CharacterOutline> _visualizedCharactersOutlines = new();

        public AttackableTilesVisualizer(ITurnQueue turnQueue,
            IMoverService moverService,
            IAttackService attackService,
            IMapService mapService,
            IStaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _moverService = moverService;
            _attackService = attackService;
            _mapService = mapService;

            _characterOutlineColors = staticDataProvider.CharacterOutlineColors;
        }

        public void Initialize()
        {
            _turnQueue.ActiveCharacter
                .Skip(1)
                .Where(character => character.Team == CharacterTeam.Ally)
                .Subscribe(VisualizeAttackableCharacters)
                .AddTo(_disposable);
            
            _turnQueue.ActiveCharacter
                .Skip(1)
                .Where(character => character.Team == CharacterTeam.Enemy)
                .Subscribe(_ => ResetLastCharacters())
                .AddTo(_disposable);

            _moverService.IsMoved
                .Where(character => character.Team == CharacterTeam.Ally)
                .Subscribe(VisualizeAttackableCharacters)
                .AddTo(_disposable);
        }

        public void Disable() =>
            _disposable.Clear();
        
        private void VisualizeAttackableCharacters(ICharacter character)
        {
            ResetLastCharacters();

            PathFindingResults pathFindingResults = _attackService.GetPathFindingResults(character);
            
            foreach (var coordinate in pathFindingResults.ObstaclesCoordinates)
            {
                if (_mapService.TryGetTile(coordinate, out Tile tile)) 
                    VisualizeTile(character, tile);
            }
        }

        private void VisualizeTile(ICharacter character, Tile tile)
        {
            if (tile.Logic.Character.Team != character.Team)
            {
                CharacterOutline characterOutline = tile.Logic.Character.View.CharacterOutline;
                
                characterOutline.ChangeColor(_characterOutlineColors.Attackable);
                characterOutline.SwitchOutLine(true);
                
                _visualizedCharactersOutlines.Add(characterOutline);
            }
        }

        private void ResetLastCharacters()
        {
            foreach (CharacterOutline characterOutline in _visualizedCharactersOutlines) 
                characterOutline.SwitchOutLine(false);

            _visualizedCharactersOutlines.Clear();
        }
    }
}