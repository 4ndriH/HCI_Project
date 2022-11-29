using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Config {
    private static bool InstantFeedback = false;

    public static bool getInstantFeedback(){
        return InstantFeedback;
    }

    public static void setInstantFeedback(bool a){
        InstantFeedback = a;
    }
}
