using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] TextAsset[] additionTransitionTables;
    [SerializeField] TextAsset[] substractionTransitionTables;
    [SerializeField] TextAsset[] multiplicationTransitionTables;
    [SerializeField] TextAsset[] divisionTransitionTables;
    [SerializeField] TextAsset[] factorialTransitionTables;
    [SerializeField] TextAsset[] powerTransitionTables;
    [SerializeField] TextAsset[] binaryLogarithmTransitionTables;
    [SerializeField] TextAsset[] temperatureConversionTransitionTables;

    TextAsset[] usedTransitionTables;
    TextAsset usedJsonFile;
    string inputString = "+00001+00";

    string operation = "Division";
    string operationString = "4 / 20";
    string machineType = "STP";
    string operatorSymbol = "/";
    bool hasTwoNumber = true;
    bool hasSign = true;
    int minValue = 1;
    int maxValue = 10;


    private void Awake()
    {
        //DefaultValues();
        //Debug.Log("TemperatureConversionCK".Substring(21,2));

        Singleton();
        usedTransitionTables = divisionTransitionTables;
    }

    void Singleton()
    {
        var thisScriptCount = FindObjectsOfType<SceneLoader>().Length;
        if(thisScriptCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    //void DefaultValues()
    //{
    //    operation = "Division";
    //}

    public void QuitButton()
    {
        Application.Quit();
    }
    public void BackToMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void NextButton()
    {
        if(operation.Substring(0,4) != "Temp")
        {
            if(machineType == "STP") {
                usedJsonFile = usedTransitionTables[2];
            } else if(machineType == "MTR"){
                usedJsonFile = usedTransitionTables[1];
            } else if (machineType == "MTP"){
                usedJsonFile = usedTransitionTables[0];
            }
        }
        else
        {
            var subOperation = operation.Substring(21, 2);
            if(subOperation == "CF")
            {
                if(machineType == "MTP")
                {
                    usedJsonFile = usedTransitionTables[0];
                }
                else if (machineType == "MTR")
                {
                    usedJsonFile = usedTransitionTables[1];
                }
                else if (machineType == "STP")
                {
                    usedJsonFile = usedTransitionTables[2];
                }
            }
            else if (subOperation == "CK")
            {
                if (machineType == "MTP")
                {
                    usedJsonFile = usedTransitionTables[3];
                }
                else if (machineType == "MTR")
                {
                    usedJsonFile = usedTransitionTables[4];
                }
                else if (machineType == "STP")
                {
                    usedJsonFile = usedTransitionTables[5];
                }
            }
            else if (subOperation == "FC")
            {
                if (machineType == "MTP")
                {
                    usedJsonFile = usedTransitionTables[6];
                }
                else if (machineType == "MTR")
                {
                    usedJsonFile = usedTransitionTables[7];
                }
                else if (machineType == "STP")
                {
                    usedJsonFile = usedTransitionTables[8];
                }
            }
            else if (subOperation == "FK")
            {
                if (machineType == "MTP")
                {
                    usedJsonFile = usedTransitionTables[9];
                }
                else if (machineType == "MTR")
                {
                    usedJsonFile = usedTransitionTables[10];
                }
                else if (machineType == "STP")
                {
                    usedJsonFile = usedTransitionTables[11];
                }
            }
            else if (subOperation == "KC")
            {
                if (machineType == "MTP")
                {
                    usedJsonFile = usedTransitionTables[12];
                }
                else if (machineType == "MTR")
                {
                    usedJsonFile = usedTransitionTables[13];
                }
                else if (machineType == "STP")
                {
                    usedJsonFile = usedTransitionTables[14];
                }
            }
            else if (subOperation == "KF")
            {
                if (machineType == "MTP")
                {
                    usedJsonFile = usedTransitionTables[15];
                }
                else if (machineType == "MTR")
                {
                    usedJsonFile = usedTransitionTables[16];
                }
                else if (machineType == "STP")
                {
                    usedJsonFile = usedTransitionTables[17];
                }
            }

            //operation == "TemperatureConversion";
        }

        SceneManager.LoadScene("MachineScene");
    }







    public void AdditionButton()
    {
        maxValue = 30;

        operatorSymbol = "+";
        operation = "Addition";
        hasTwoNumber = true;
        hasSign = true;
        usedTransitionTables = additionTransitionTables;
        SceneManager.LoadScene(1);
    }
    public void SubstractionButton()
    {
        maxValue = 30;

        operatorSymbol = "-";
        operation = "Substraction";
        hasTwoNumber = true;
        hasSign = true;
        usedTransitionTables = substractionTransitionTables;
        SceneManager.LoadScene(1);
    }
    public void MultiplicationButton()
    {
        maxValue = 8;

        operatorSymbol = "*";
        operation = "Multiplication";
        hasTwoNumber = true;
        hasSign = true;
        usedTransitionTables = multiplicationTransitionTables;
        SceneManager.LoadScene(1);
    }
    public void DivisionButton()
    {
        maxValue = 30;

        operatorSymbol = "/";
        operation = "Division";
        hasTwoNumber = true;
        hasSign = true;
        usedTransitionTables = divisionTransitionTables;
        SceneManager.LoadScene(1);
    }

    public void FactorialButton()
    {
        maxValue = 4;

        operatorSymbol = "!";
        operation = "Factorial";
        hasTwoNumber = false;
        hasSign = false;
        usedTransitionTables = factorialTransitionTables;
        SceneManager.LoadScene(1);
    }
    public void PowerButton()
    {
        maxValue = 3;

        operatorSymbol = "^";
        operation = "Power";
        hasTwoNumber = true;
        hasSign = false;
        usedTransitionTables = powerTransitionTables;
        SceneManager.LoadScene(1);
    }
    public void BinaryLogarithmButton()
    {
        maxValue = 80;

        operatorSymbol = "L";
        operation = "BinaryLogarithm";
        hasTwoNumber = false;
        hasSign = false;
        usedTransitionTables = binaryLogarithmTransitionTables;
        SceneManager.LoadScene(1);
    }
    public void TemperatureConversionButton()
    {
        maxValue = 5;

        operatorSymbol = "T";
        operation = "TemperatureConversion";
        hasTwoNumber = false;
        hasSign = true;
        usedTransitionTables = temperatureConversionTransitionTables;
        SceneManager.LoadScene(1);
    }








    public string GetOperation() => operation;
    public void SetOperation(string input) { operation = input; }
    public string GetOperationString() => operationString;
    public void SetOperationString(string input) { operationString = input; }
    public string GetOperatorSymbol() => operatorSymbol;
    public string GetInputString() => inputString;
    public void SetInputString(string input) { inputString = input; }
    public string GetMachineType() => machineType;
    public void SetMachineType(string input) { machineType = input; }

    public TextAsset GetUsedJsonFile() => usedJsonFile;

    public bool HasTwoNumber() => hasTwoNumber;
    public bool HasSign() => hasSign;


    public int GetMinValue() => minValue;
    public int GetMaxValue() => maxValue;

}
