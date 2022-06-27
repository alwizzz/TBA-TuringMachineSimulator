using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Box : MonoBehaviour
{
    Text thisText;
    [SerializeField] string symbol;

    private void Awake()
    {
        thisText = transform.Find("Text").GetComponent<Text>();
    }

    private void Start()
    {
        thisText.text = symbol;
    }

    public string GetSymbol() => symbol;
    public void SetSymbol(string input) 
    { 
        symbol = input;
        thisText.text = symbol;
    }

}
