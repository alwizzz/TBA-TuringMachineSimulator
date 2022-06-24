using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionOutput
{
    public string nextState;
    public string nextSymbol;
    public string direction;

    public TransitionOutput(string nextState, string nextSymbol, string direction)
    {
        this.nextState = nextState;
        this.nextSymbol = nextSymbol;
        this.direction = direction;
    }
}
