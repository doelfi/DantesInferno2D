using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class GameStats
{
    // Collects the current game stats and saves the times each level is finished to a file.

    // Player stats
    public static int lifes = 3;
    public static int coins = 0;
    
    // Speed run times
    public static double[] runTimes = new double[6];
    public static string runTimePath = "";

    public static void SaveRunTimes()
    {
        // Writes the runTimes to the file to save them

        // Deletes old content of the file
        File.WriteAllText(runTimePath, ""); 

        foreach (double entry in runTimes)
        {
            File.AppendAllText(runTimePath, entry.ToString() + "\n");
        }
    }
}
