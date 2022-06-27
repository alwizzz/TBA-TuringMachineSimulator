using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransitionTable
{
    public string operation;
    public string type;
    public int tapeCount;
    public string[] symbols;
    public string blankSymbol;
    public string startState;
    public string[] acceptStates;
    public State[] states;

    [System.Serializable]
    public class State
    {
        public string name;
        public Transition[] transitions;

        [System.Serializable]
        public class Transition
        {
            public string read;
            public string write;
            public string direction;
            public string nextState;
        }

        
    }

    public TransitionTable.State getStateWithName(string name)
    {
        TransitionTable.State targetState = null;
        foreach (TransitionTable.State state in states)
        {
            if (state.name == name)
            {
                targetState = state;
                break;
            }
        }

        return targetState;
    }

}


