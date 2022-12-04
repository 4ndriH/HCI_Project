using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using System.IO;
using Debug = UnityEngine.Debug;
using Newtonsoft.Json;

public static class GatherData
{   
    static int submit;
    static int undo;
    static int failures;
    static List<string> stringList = new List<string>();
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
        Debug.Log("" + Config.getName() + ": Completing " + title + " took " + elapsedTime + " and the subject failed " + failures + " time(s) ("+instant+")");
        if(Config.getInstantFeedback()){
            stringList.Add("" + title + " " + elapsedTime + " " + failures + " " + instant + " " + Config.getName());
        }else{
            stringList.Add("" + title + " " + elapsedTime + " " + submit + " " + undo + " " + instant + " " + Config.getName());
        }
        
    }

    public static void writeLogToFile() {
        String[] strArr = File.ReadAllLines("data.txt");
        int idx = 0;

        foreach (String str in strArr) {
            stringList.Insert(idx++, str);
        }

        File.WriteAllLines("data.txt", stringList);
        stringList.Clear();
    }

    public static void endTest(){
        // Creating a file
    string myfile = @"file.txt";

        // Appending the given texts
        using(StreamWriter sw = File.AppendText(myfile))
        {
            sw.WriteLine("Gfg");
            sw.WriteLine("GFG");
            sw.WriteLine("GeeksforGeeks");
        }
    }

    public static void addFailure(){
        failures += 1;
    }

    public static void addSubmit(){
        submit += 1;
    }
    public static void addUndo(){
        undo +=1;
    }
}
