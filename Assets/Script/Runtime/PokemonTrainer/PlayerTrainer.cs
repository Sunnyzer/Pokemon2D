using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTrainer : Player, BattleFighter
{
    public event Action<Pokemon> OnSwapPokemon = null;

    CharacterMovement characterMovement;
    PokemonTeam pokemonTeam;
    TurnAction actionSelected = null;
    List<PokemonChoice> pokemonChoices = new List<PokemonChoice>();
    int index = 0;

    [SerializeField] UI trainerPrefab;


    public CharacterMovement CharacterMovement => characterMovement;
    public PokemonTeam PokemonTeam => pokemonTeam;
    public Pokemon[] Pokemons => pokemonTeam.Pokemons;
    public Pokemon CurrentPokemonInCombat => Pokemons[index];
    public bool HavePokemonLeft => pokemonTeam.HavePokemonLeft();
    public bool IsInBattle { get; set; }
    public bool IsReady { get; set; }

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

    public bool Swap(int _index)
    {
        Pokemon _pokemonToSwap = pokemonTeam.Pokemons[_index];
        if (_pokemonToSwap == null || _pokemonToSwap.Fainted) return false;
        index = _index;
        OnSwapPokemon?.Invoke(CurrentPokemonInCombat);
        return true;
    }

    public Pokemon GetFirstSlotPokemon()
    {
        return Pokemons[0];
    }
}
