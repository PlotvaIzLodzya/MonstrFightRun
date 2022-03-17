using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunerHassle
{
    public void Hassle(Enemy enemy, Player player, out bool IsPlayerWin)
    {
        if (enemy.Level > player.Might)
            IsPlayerWin = false;
        else
            IsPlayerWin = true;
    }
}
