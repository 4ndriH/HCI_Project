using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    public bool InstantFeedback = false;
    public void getInstantFeedback(){
        return InstantFeedback;
    }

    public void setInstantFeedback(bool a){
        InstantFeedback = a;
    }
}
