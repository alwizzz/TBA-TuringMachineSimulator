using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusDisplay : MonoBehaviour
{
    [SerializeField] TuringMachine turingMachine;
    [SerializeField] TextMeshProUGUI currentStateText;
    [SerializeField] TextMeshProUGUI currentSymbolText;


    // Start is called before the first frame update
    void Start()
    {
        currentStateText.text = turingMachine.currentStateName;
        currentSymbolText.text = turingMachine.currentSymbol;

    }

    // Update is called once per frame
    void Update()
    {
        currentStateText.text = turingMachine.currentStateName;
        currentSymbolText.text = turingMachine.currentSymbol;
    }
}
