using System;

public interface BattleFighter
{
    public event Action<Pokemon> OnSwapPokemon;

    public Pokemon[] Pokemons { get; }
    public Pokemon CurrentPokemonInCombat { get; }
    public bool IsInBattle { get; set; }
    public bool IsReady { get; set; }
    public abstract Pokemon GetFirstSlotPokemon();
    public abstract TurnAction Turn(BattleInfo _battleInfo);
    public abstract bool Swap(int _index);
}