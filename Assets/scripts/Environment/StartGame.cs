using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class StartGame : MonoBehaviour
{

    // Path to the file that contains the speed run times.
    private string _runTimesPath;
    
    public void OnStartPress()
    {
        // To start the game, the lifes, coins, and first level are initialized.
        GameStats.lifes = 3;
        GameStats.coins = 0;
        SceneManager.LoadScene(1);
    }

    public void OnSpeedPress()
    {
        // By clicking on the speed run times, the scene is loaded and times are displayed.
        SceneManager.LoadScene(7); 
    }

    private void ReadRunTimes()
    {
        // Create File if it doesn't exist
        if (!File.Exists(_runTimesPath))
        {
            for (int i = 0; i < 6; i++)
            {
                File.AppendAllText(_runTimesPath, "0\n");
            }
        }
        
        // Read content into the array stored in GameStats
        IEnumerable<string> content = File.ReadLines(_runTimesPath);
        int n = 0;
        foreach (string line in content)
        {
            // German systems use a ',' as a separator for floats when converting to a string.
            // We need to change it back in order to work with it correctly.
            string correctLine = line.Replace(",", ".");
            double time = double.Parse(correctLine, CultureInfo.InvariantCulture);
            GameStats.runTimes[n] = time;
            n++;
        }
    }
    
    void Start()
    {
        _runTimesPath = Application.dataPath + "/SpeedRunTimes.txt";
        GameStats.runTimePath = _runTimesPath;
        ReadRunTimes();
    }
}
