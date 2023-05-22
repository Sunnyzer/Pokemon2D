using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPokemon : Player
{
    CharacterMovement characterMovement;
    PokemonTeam pokemonTeam;
    public int encounter = 0;
    public int encounterRare = 0;
    public int tryE = 0;
    public int tryS = 0;
    public List<PokemonChoice> pokemonChoices = new List<PokemonChoice>();
    public CharacterMovement CharacterMovement => characterMovement;
    public PokemonTeam PokemonTeam => pokemonTeam;
    [SerializeField] UI trainerPrefab;
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
}
