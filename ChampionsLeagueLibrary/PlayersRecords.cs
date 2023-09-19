using System;
using System.Collections;
using System.Net.NetworkInformation;

namespace ChampionsLeagueLibrary;

#region Třída PlayersCountChangedEventArgs
// TODO: Vytvořte třídu PlayersCountChangedEventArgs (dědící z EventArgs)
// - vlastnost int OldCount
// - vlastnost int NewCount
public class PlayersCountChangedEventArgs : EventArgs
{
    public PlayersCountChangedEventArgs(int oldCount, int newCount)
    {
        OldCount = oldCount;
        NewCount = newCount;
    }

    public int OldCount { get; set; }
    public int NewCount { get; set; }
}
#endregion

// TODO: Vytvořte delegát pro událost PlayersCountChangedEventHandler (využijte výše definované event args)
public delegate void PlayersCountChangedEventHandler(object sender, PlayersCountChangedEventArgs args);

#region Třída PlayerRecords
// TODO: Vytvořte třídu PlayersRecords

// TODO: Vytvořte atribut players typu Player[]

// TODO: Vytvořte vlastnost Count (pouze pro čtení), která vždy vrací aktuální velikost pole players

// TODO: Vytvořte událost PlayersCountChanged (PlayersCountChangedEventHandler)

// TODO: vytvořte indexer [int index], který umožňuje získat Player? z pole (pouze operace get)
// - get - pokud je index neplatný, je vráceno null; jinak je vrácen objekt z pole

// TODO: Vytvořte bezparametrický konstruktor
// - inicializujte pole players na pole délky 0

// TODO: Vytvořte metodu void Add(Player player)
// - zvětšete velikost pole o jeden prvek
// - na poslední index v poli je vložen nový hráč (player)
// - vyvolejte událost PlayersCountChanged s příslušnými argumenty

// TODO: Vytvořte metodu void Delete(int index)
// - pokud je index neplatný - metoda nedělá nic
// - odeberte vybraného hráče z pole, pole setřeste (posuňte hráče z vyšších indexů, tak aby byla zaplněna (null) mezera; zachovejte pořadí hráčů)
// - zmenšete velikost pole o 1 prvek
// - vyvolejte událost PlayersCountChanged s příslušnými argumenty

// TODO: Vytvořte metodu bool FindBestClubs(...)
// - výstupní parametr FootballClub[] clubs
// - výstupní parametr int goalCount
// - pokud je pole players prázdné - pak - clubs: prázdné pole, goalCount: 0, metoda vrací false
// - sečtěte počty gólů podle klubů
// - najděte maximální počet gólů a uložte jej do goalCount
// - najděte všechny kluby, které mají celkově goalCount gólů a uložte je do clubs
// - vraťte true
public class PlayersRecords
{
    public Player?[] players;

    public PlayersRecords()
    {
        this.players = new Player[0];
    }
    public Player? this[int i]
    {
        get {
            if (i >= Count || i < 0)
            {
                return null;
            } else
            {
                return players[i];
            }
        }
    }

    public event PlayersCountChangedEventHandler? PlayersCountChanged;

    protected virtual void OnPlayerCountChanged(PlayersCountChangedEventArgs eventArgs)
    {
        PlayersCountChanged?.Invoke(this, eventArgs);
    }

    public int Count
    {
        get
        {
            if (players == null)
            {
                return 0;
            }
            else
            {
                return players.Length;
            }
        }
    }


    public void Add(Player player)
    {
        // - zvětšete velikost pole o jeden prvek
        // - na poslední index v poli je vložen nový hráč (player)
        // - vyvolejte událost PlayersCountChanged s příslušnými argumenty
        Array.Resize(ref players, players.Length + 1);
        players[Count - 1] = player;

        OnPlayerCountChanged(new PlayersCountChangedEventArgs(Count - 1, Count));
    }

    public void Delete(int index)
    {
        // - pokud je index neplatný - metoda nedělá nic
        // - odeberte vybraného hráče z pole, pole setřeste (posuňte hráče z vyšších indexů, tak aby byla zaplněna (null) mezera; zachovejte pořadí hráčů)
        // - zmenšete velikost pole o 1 prvek
        // - vyvolejte událost PlayersCountChanged s příslušnými argumenty
        if (index >= Count || index < 0)
        {
            return;
        } 
        players[index] = null;
        SetresPole(players);
        Array.Resize(ref players, players.Length - 1);
        OnPlayerCountChanged(new PlayersCountChangedEventArgs(Count+1, Count));
    }

    private void SetresPole(Player?[] pole)
    {
        for (int i = 0; i < pole.Length-1; i++)
        {
            if (pole[i] == null)
            {
                pole[i] = pole[i + 1];
                pole[i+1] = null;
            }
        }

    }

    public bool FindBestClubs(out FootballClub[] clubs, out int goalCount)
    {
        //  výstupní parametr FootballClub[] clubs
        // - výstupní parametr int goalCount
        // - pokud je pole players prázdné - pak - clubs: prázdné pole, goalCount: 0, metoda vrací false
        // - sečtěte počty gólů podle klubů
        // - najděte maximální počet gólů a uložte jej do goalCount
        // - najděte všechny kluby, které mají celkově goalCount gólů a uložte je do clubs
        // - vraťte true

        if (players.Length == 0)
        {
            clubs = new FootballClub[0]; 
            goalCount = 0;
            return false;
        } else
        {

            int[] poleGolu = new int[6];

            for (int i = 0; i < players.Length; i++)
            {
                switch(players[i].Club)
                {
                    case FootballClub.None: poleGolu[0] += players[i].GoalCount; break;
                    case FootballClub.Chelsea: poleGolu[1] += players[i].GoalCount; break;
                    case FootballClub.RealMadrid: poleGolu[2] += players[i].GoalCount; break;
                    case FootballClub.Arsenal: poleGolu[3] += players[i].GoalCount; break;
                    case FootballClub.FCPorto: poleGolu[4] += players[i].GoalCount; break;
                    case FootballClub.Barcelona: poleGolu[5] += players[i].GoalCount; break;

                }
            }

            goalCount = MaxtValue(poleGolu[0], poleGolu[1], poleGolu[2], poleGolu[3], poleGolu[4], poleGolu[5]);
            FootballClub[] footballClubs = { FootballClub.None, FootballClub.Chelsea, FootballClub.RealMadrid, FootballClub.Arsenal, FootballClub.FCPorto, FootballClub.Barcelona };
            FootballClub[] finalniPole = new FootballClub[0];

            for (int i = 0; i < 6; i++)
            {
                if (poleGolu[i] == goalCount)
                {
                    Array.Resize(ref finalniPole, finalniPole.Length + 1);
                    finalniPole[finalniPole.Length-1] = footballClubs[i];
                }
            }


            clubs = finalniPole;
            return true;
        }
    }

    private int MaxtValue(int n, int c, int m, int a, int p, int b)
    {
        return Math.Max(n, Math.Max(c, Math.Max(m, Math.Max(a, Math.Max(p, b)))));
    }

}

#endregion