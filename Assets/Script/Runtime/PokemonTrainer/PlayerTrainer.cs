using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTrainer : Player, BattleFighter
{
    CharacterMovement characterMovement;
    PokemonTeam pokemonTeam;
    TurnAction actionSelected = null;
    
    [SerializeField] UI trainerPrefab;

    public List<PokemonChoice> pokemonChoices = new List<PokemonChoice>();
    public CharacterMovement CharacterMovement => characterMovement;
    public PokemonTeam PokemonTeam => pokemonTeam;
    public bool IsInBattle { get; set; }
    public Pokemon[] Pokemons => pokemonTeam.Pokemons;
    public bool IsReady { get; set; }
    public Pokemon CurrentPokemonInCombat => GetPokemon();
    public TurnAction ActionSelected => actionSelected;

    protected override void Start()
    {
        base.Start();
        characterMovement = GetComponent<CharacterMovement>();
        pokemonTeam = GetComponent<PokemonTeam>();
    }
    public override void RemoveControl()
    {
        base.RemoveControl();
        characterMovement.ClearInput();
        characterMovement.StopSprint();
    }
    public override void UpdateController(float _deltaTime)
    {
        pokemonChoices = pokemonChoices.OrderBy(p => p.IndexPokemon).ToList();
        if (Input.GetKeyDown(KeyCode.F))
        {
            UIManager.Instance.SetCurrentUIDisplay(trainerPrefab);
            return;
        }
        characterMovement.Sprint();
        characterMovement.RegisterInputKeyDown(KeyCode.W, new Vector2(0, 1));
        characterMovement.RegisterInputKeyDown(KeyCode.S, new Vector2(0, -1));
        characterMovement.RegisterInputKeyDown(KeyCode.D, new Vector2(1, 0));
        characterMovement.RegisterInputKeyDown(KeyCode.A, new Vector2(-1, 0));
    }

    public Pokemon GetPokemon()
    {
        return pokemonTeam.GetFirstLivingPokemon();
    }

    public void SelectAction(TurnAction _actionSelected)
    {
        actionSelected = _actionSelected;
        IsReady = true;
    }

    public TurnAction Turn(BattleInfo _battleInfo)
    {
        actionSelected.BattleInfo = _battleInfo;
        return actionSelected; 
    }
}
