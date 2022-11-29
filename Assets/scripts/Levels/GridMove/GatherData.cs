using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using Debug = UnityEngine.Debug;


public class GatherData : MonoBehaviour
{   
    int failures = 0;
    Stopwatch sw = new Stopwatch();
    public void startLevel(){
        failures = 0;
        sw.Reset();
        sw.Start();
    }
    public void stopLevel(string title){
        TimeSpan ts = sw.Elapsed;
        string elapsedTime = String.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        Debug.Log("Completing " + title + " took " + elapsedTime + " and the subject failed " + failures + " time(s)");
    }

    public void addFailure(){
        failures += 1;
    }
}
