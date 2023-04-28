using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStats
{
    // FIXME: need a different way of setting the stats at the start of the game. 
    // This way on level load everything gets resetted to (3,0).
    public static int lifes = 3;
    public static int coins = 0;
}
