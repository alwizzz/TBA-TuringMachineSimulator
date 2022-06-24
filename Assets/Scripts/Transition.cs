using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Transition")]
public class Transition : ScriptableObject
{
    public string state;
    public string symbol;
    public string nextState;
    public string nextSymbol;
    public string direction; 
}
