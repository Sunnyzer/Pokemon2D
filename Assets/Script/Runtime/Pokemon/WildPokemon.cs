using System;

public class WildPokemon
{
    public event Action<Pokemon> OnSwapPokemon;

    Pokemon pokemon = null;
    public Pokemon[] Pokemons { get; }
    public bool IsInBattle { get; set; }
    public bool IsReady { get; set; }

    public Pokemon CurrentPokemonInCombat => pokemon;

    public Pokemon GetFirstSlotPokemon()
    {
        return pokemon;
    }

    public TurnAction CalculAction(BattleField _battleField)
    {
        Move _move = pokemon.GetRandomMove();
        AttackAction _action = new AttackAction(pokemon, _move);
        return _action;
    }

    public bool Swap(int _index)
    {
        return false;
    }

    public WildPokemon(Pokemon _pokemon)
    {
        pokemon = _pokemon;
    }
}
