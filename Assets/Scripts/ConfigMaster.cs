using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfigMaster : MonoBehaviour
{


    // PROPERTIES
    [SerializeField] string operation;
    [SerializeField] string machineType;
    [SerializeField] int firstNumber;
    [SerializeField] string firstSign;
    [SerializeField] int secondNumber;
    [SerializeField] string secondSign;

    // STATES
    [SerializeField] bool hasTwoNumber;
    [SerializeField] bool hasSign;


    // CACHES
    [SerializeField] Button buttonSTP;
    [SerializeField] Button buttonMTR;
    [SerializeField] Button buttonMTP;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI operatorSymbolText;
    [SerializeField] TextMeshProUGUI inputStringText;
    [SerializeField] NumberController firstNumberController;
    [SerializeField] NumberController secondNumberController;
    [SerializeField] Canvas extraCanvas;
    [SerializeField] Button[] extraButtons;


    SceneLoader sceneLoader;
    string TemperatureConversionString = "TemperatureConversion";



    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        hasTwoNumber = sceneLoader.HasTwoNumber();
        hasSign = sceneLoader.HasSign();
        operation = sceneLoader.GetOperation();

        if(operation == "TemperatureConversion")
        {
            titleText.text = "Temperature Conversion";
        }
        else if (operation == "BinaryLogarithm")
        {
            titleText.text = "Binary Logarithm";
        }
        else
        {
            titleText.text = sceneLoader.GetOperation();
        }

        operatorSymbolText.text = sceneLoader.GetOperatorSymbol();

        Setup();
        if(operation == "TemperatureConversion")
        {
            ExtraSetup();
            CKButton();
        }

        DefaultValues();
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void Setup()
    {
        if (!hasTwoNumber)
        {
            secondNumberController.gameObject.SetActive(false);
        }

        if (!hasSign)
        {
            firstNumberController.hasSign = false;
            firstNumberController.buttonSign.gameObject.SetActive(false);

            if (hasTwoNumber)
            {
                secondNumberController.hasSign = false;
                secondNumberController.buttonSign.gameObject.SetActive(false);
            }
        }
    }

    void ExtraSetup()
    {
        extraCanvas.gameObject.SetActive(true);
        //TemperatureConversionString = operation;
    }

    void DefaultValues()
    {
        STPButton();
        firstNumber = 1;
        firstSign = "";
        secondNumber = (hasTwoNumber) ? 1 : 0;
        secondSign = "";
    }

    void UpdateButton()
    {
        buttonSTP.interactable = true;
        buttonMTR.interactable = true;
        buttonMTP.interactable = true;

        if(machineType == "STP")
        {
            buttonSTP.interactable = false;
        } else if(machineType == "MTR")
        {
            buttonMTR.interactable = false;
        }
        else if(machineType == "MTP")
        {
            buttonMTP.interactable = false;
        }

        sceneLoader.SetMachineType(machineType);
        UpdateInputString();
    }

    void UpdateExtraButton(string subOperation)
    {
        foreach(Button btn in extraButtons)
        {
            if(btn.gameObject.name == $"Button {subOperation}")
            {
                btn.interactable = false;
            } else
            {
                btn.interactable = true;
            }
        }

        sceneLoader.SetOperation(operation);
    }

    public void STPButton()
    {
        machineType = "STP";
        UpdateButton();
    }
    public void MTRButton()
    {
        machineType = "MTR";
        UpdateButton();
    }
    public void MTPButton()
    {
        machineType = "MTP";
        UpdateButton();
    }
    public void CKButton()
    {
        var subOperation = "CK";
        operation = TemperatureConversionString + subOperation;
        UpdateExtraButton(subOperation);
    }
    public void CFButton()
    {
        var subOperation = "CF";
        operation = TemperatureConversionString + subOperation;
        UpdateExtraButton(subOperation);
    }
    public void KFButton()
    {
        var subOperation = "KF";
        operation = TemperatureConversionString + subOperation;
        UpdateExtraButton(subOperation);
    }
    public void FCButton()
    {
        var subOperation = "FC";
        operation = TemperatureConversionString + subOperation;
        UpdateExtraButton(subOperation);
    }
    public void KCButton()
    {
        var subOperation = "KC";
        operation = TemperatureConversionString + subOperation;
        UpdateExtraButton(subOperation);
    }
    public void FKButton()
    {
        var subOperation = "FK";
        operation = TemperatureConversionString + subOperation;
        UpdateExtraButton(subOperation);
    }

    public void UpdateNumber(int value, string sign, bool isSecondNumber)
    {
        if (!isSecondNumber)
        {
            firstNumber = value;
            firstSign = sign;
        }
        else
        {
            secondNumber = value;
            secondSign = sign;
        }

        UpdateInputString();
    }

    string DecimalToUnary(int value, string sign, string symbol)
    {
        string result = sign;
        for(int i=0; i<value; i++)
        {
            result += symbol;
        }

        return result;
    }

    void UpdateInputString()
    {
        if (operation == "Addition")
        {
            //if(machineType == "STP" || machineType == "MTR")
            //{
            //    string result = "";
            //    string divider = "0";

            //    result += DecimalToUnary(firstNumber, firstSign, "1");
            //    result += divider;
            //    result += DecimalToUnary(secondNumber, secondSign, "1");

            //    inputStringText.text = result;
            //    sceneLoader.SetInputString(result);
            //}
            //else if (machineType == "MTP")
            //{

            //}
            string result = "";
            string divider = "0";

            result += DecimalToUnary(firstNumber, firstSign, "1");
            result += divider;
            result += DecimalToUnary(secondNumber, secondSign, "1");

            inputStringText.text = result;
            sceneLoader.SetInputString(result);
        }

        else if (operation == "Substraction")
        {
            //if (machineType == "STP")
            //{
            //    string result = "";
            //    string divider = "0";

            //    result += DecimalToUnary(firstNumber, firstSign, "1");
            //    result += divider;
            //    result += DecimalToUnary(secondNumber, secondSign, "1");

            //    inputStringText.text = result;
            //    sceneLoader.SetInputString(result);
            //}
            //else if (machineType == "MTR")
            //{

            //}
            //else if (machineType == "MTP")
            //{

            //}
            string result = "";
            string divider = "0";

            result += DecimalToUnary(firstNumber, firstSign, "1");
            result += divider;
            result += DecimalToUnary(secondNumber, secondSign, "1");

            inputStringText.text = result;
            sceneLoader.SetInputString(result);
        }

        else if (operation == "Multiplication")
        {
            if(machineType == "MTR" || machineType == "MTP")
            {
                string result = "";
                string divider = "1";

                result += DecimalToUnary(firstNumber, firstSign, "0");
                result += divider;
                result += DecimalToUnary(secondNumber, secondSign, "0");

                inputStringText.text = result;
                sceneLoader.SetInputString(result);
            }
            else if(machineType == "STP")
            {
                string result = "";
                string divider = "#";

                result += DecimalToUnary(firstNumber, firstSign, "0");
                result += divider;
                result += DecimalToUnary(secondNumber, secondSign, "0");
                result += divider;

                inputStringText.text = result;
                sceneLoader.SetInputString(result);
            }
        }

        else if (operation == "Division")
        {
            string result = "";
            string divider = "1";

            result += DecimalToUnary(firstNumber, firstSign, "0");
            result += divider;
            result += DecimalToUnary(secondNumber, secondSign, "0");

            inputStringText.text = result;
            sceneLoader.SetInputString(result);
        }

        else if (operation == "Factorial")
        {
            if (machineType == "STP" || machineType == "MTR")
            {
                string result = "";
                string divider = "1";

                result += divider;
                result += DecimalToUnary(firstNumber, firstSign, "0");
                result += divider;

                inputStringText.text = result;
                sceneLoader.SetInputString(result);
            }
            else if (machineType == "MTP")
            {
                string result = "";
                string divider = "1";

                result += DecimalToUnary(firstNumber, firstSign, "0");
                result += divider;

                inputStringText.text = result;
                sceneLoader.SetInputString(result);
            }
        }

        else if (operation == "Power")
        {
            //if (machineType == "STP")
            //{
            //    string result = "";
            //    string divider = "1";

            //    result += DecimalToUnary(firstNumber, firstSign, "0");
            //    result += divider;
            //    result += "#";
            //    result += DecimalToUnary(secondNumber, secondSign, "0");
            //    result += divider;


            //    inputStringText.text = result;
            //    sceneLoader.SetInputString(result);
            //}
            //else if (machineType == "MTR")
            //{
            //    string result = "";
            //    string divider = "1";

            //    result += DecimalToUnary(firstNumber, firstSign, "0");
            //    result += divider;
            //    result += DecimalToUnary(secondNumber, secondSign, "0");


            //    inputStringText.text = result;
            //    sceneLoader.SetInputString(result);
            //}
            //else if (machineType == "MTP")
            //{
            //    string result = "";
            //    string divider = "1";

            //    result += DecimalToUnary(firstNumber, firstSign, "0");
            //    result += divider;
            //    result += DecimalToUnary(secondNumber, secondSign, "0");


            //    inputStringText.text = result;
            //    sceneLoader.SetInputString(result);
            //}

            string result = "";
            string divider = "1";

            result += DecimalToUnary(firstNumber, firstSign, "0");
            result += divider;
            result += DecimalToUnary(secondNumber, secondSign, "0");


            inputStringText.text = result;
            sceneLoader.SetInputString(result);
        }

        else if (operation == "BinaryLogarithm")
        {
            string result = "";

            result += DecimalToUnary(firstNumber, firstSign, "0");
            
            inputStringText.text = result;
            sceneLoader.SetInputString(result);
        }

        else if (operation.Substring(0,21) == "TemperatureConversion")
        {
            string result = "";
            result += DecimalToUnary(firstNumber, firstSign, "0");

            inputStringText.text = result;
            sceneLoader.SetInputString(result);
        }
    }


    private void OnDestroy()
    {
        string operationString = "";


        if(operation.Length > 21 &&  operation.Substring(0,21) == "TemperatureConversion")
        {
            var subOperation = operation.Substring(21, 2);

            operationString += (hasSign && firstSign == "-") ? firstSign : "";
            operationString += firstNumber;

            if (subOperation == "CK" || subOperation == "CF")
            {
                operationString += " °C";
            }
            else if (subOperation == "FC" || subOperation == "FK")
            {
                operationString += " °F";
            }
            else if (subOperation == "KC" || subOperation == "KF")
            {
                operationString += " K";
            }
        }
        else if (operation == "BinaryLogarithm")
        {
            operationString += "Log_2(";
            operationString += firstNumber;
            operationString += ")";
        }
        else
        {
            operationString += (hasSign && firstSign == "-") ? firstSign : "";
            operationString += firstNumber;
            operationString += " ";
            operationString += sceneLoader.GetOperatorSymbol();

            if (hasTwoNumber)
            {
                operationString += " ";
                operationString += (hasSign && secondSign == "-") ? secondSign : "";
                operationString += secondNumber;
            }
        }



        sceneLoader.SetOperationString(operationString);
    }
}

