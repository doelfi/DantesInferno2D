using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class GameStats
{
    // player stats
    public static int lifes = 3;
    public static int coins = 0;
    
    // speed run times
    public static double[] runTimes = new double[6];
    public static string runTimePath = "";

    // write the runTimes to the file to save them
    // should be called on level switch from the player
    public static void SaveRunTimes()
    {
        File.WriteAllText(runTimePath, ""); // delete old content of the file

        foreach (double entry in runTimes)
        {
            File.AppendAllText(runTimePath, entry.ToString());
        }
    }
}
