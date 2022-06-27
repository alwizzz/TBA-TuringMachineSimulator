using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    string state;
    Transition[] transitions;
    public class Transition
    {
        char[] reads;
        char[] writes;
        char[] directions;
        string nextState;
    }
}
