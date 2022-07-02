using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OperationDisplay : MonoBehaviour
{
    [SerializeField] TuringMachine turingMachine;
    [SerializeField] TextMeshProUGUI operationText;

    // Start is called before the first frame update
    void Start()
    {
        operationText.text = ParseOperation();
    }

    string ParseOperation()
    {
        var operation = turingMachine.operation;
        if(operation.Substring(0,4) == "Temp")
        {
            var subOperation = operation.Substring(21, 2);
            if (subOperation == "CK")
            {
                return "Celcius to Kelvin";
            }
            else if (subOperation == "KC")
            {
                return "Kelvin to Celcius";
            }
            else if (subOperation == "CF")
            {
                return "Celcius to Fahrenheit";
            }
            else if (subOperation == "FC")
            {
                return "Fahrenheit to celcius";
            }
            else if (subOperation == "KF")
            {
                return "Kelvin to Fahrenheit";
            }
            else if (subOperation == "FK")
            {
                return "Fahrenheit to Kelvin";
            }
        }
        else if(operation == "BinaryLogarithm")
        {
            return "Binary Logarithm";
        }
        else
        {
            return operation;
        }

        return "error on ParseOperation";
    }
}
