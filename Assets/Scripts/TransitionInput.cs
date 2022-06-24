using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionInput
{
    public string state;
    public string symbol;

    public TransitionInput(string state, string symbol)
    {
        this.state = state;
        this.symbol = symbol;
    }
}
