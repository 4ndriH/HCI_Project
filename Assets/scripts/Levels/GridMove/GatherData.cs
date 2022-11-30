using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using Debug = UnityEngine.Debug;


public static class GatherData
{   
    static int failures;
    static Stopwatch sw = new Stopwatch();
    public static void startLevel(){
        failures = 0;
        sw.Reset();
        sw.Start();
    }
    public static void stopLevel(string title){
        TimeSpan ts = sw.Elapsed;
        string instant = "ERR";
        if(Config.getInstantFeedback()){
            instant = "IF";
        }
        else{
            instant = "CAP";
        }
        string elapsedTime = String.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        Debug.Log("Completing " + title + " took " + elapsedTime + " and the subject failed " + failures + " time(s) ("+instant+")");
    }

    public static void addFailure(){
        failures += 1;
    }
}
