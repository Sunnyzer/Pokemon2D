using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTrainer : Player
{
    public event Action<Pokemon> OnSwapPokemon = null;

    CharacterMovement characterMovement;
    PokemonTeam pokemonTeam;
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
    public void Interact()
    {
        Vector3 _dir = new Vector3(characterMovement.DirectionEye.x, characterMovement.DirectionEye.y, 0);
        RaycastHit2D _raycast = Physics2D.CircleCast(transform.position + _dir, 0.1f, Vector3.forward);
        if (!_raycast) return;
        InteractObject _interact = _raycast.collider.GetComponent<InteractObject>();
        if(_interact)
        {
            _interact.Interact(this);
        }
    }
    public override void UpdateController(float _deltaTime)
    {
        pokemonChoices = pokemonChoices.OrderBy(p => p.IndexPokemon).ToList();
        if (Input.GetKeyDown(KeyCode.F))
        {
            UIManager.Instance.SetCurrentUIDisplay(trainerPrefab, this);
            return;
        }
        if(Input.GetMouseButtonDown(0))
        {
            Interact();
        }
        characterMovement.Sprint();
        characterMovement.RegisterInputKeyDown(KeyCode.W, new Vector2(0, 1));
        characterMovement.RegisterInputKeyDown(KeyCode.S, new Vector2(0, -1));
        characterMovement.RegisterInputKeyDown(KeyCode.D, new Vector2(1, 0));
        characterMovement.RegisterInputKeyDown(KeyCode.A, new Vector2(-1, 0));
    }

    public bool Swap(int _index)
    {
        Pokemon _pokemonToSwap = pokemonTeam.Pokemons[_index];
        if (_pokemonToSwap == null || _pokemonToSwap.Fainted) return false;
        index = _index;
        OnSwapPokemon?.Invoke(CurrentPokemonInCombat);
        return true;
    }
    public int GetFirstPokemonNotFaintedIndex()
    {
        return PokemonTeam.GetFirstLivingPokemonIndex();
    }
    public Pokemon GetFirstPokemonNotFainted()
    {
        return PokemonTeam.GetFirstLivingPokemon();
    }
    public Pokemon GetFirstSlotPokemon()
    {
        return Pokemons[0];
    }
}
