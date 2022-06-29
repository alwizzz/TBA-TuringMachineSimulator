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


    SceneLoader sceneLoader;



    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        titleText.text = sceneLoader.GetOperation();
        operation = sceneLoader.GetOperation();
        operatorSymbolText.text = sceneLoader.GetOperatorSymbol();
        hasTwoNumber = sceneLoader.HasTwoNumber();
        hasSign = sceneLoader.HasSign();

        Setup();


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

        UpdateInputString();
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
            if(machineType == "STP")
            {

            }
            else if (machineType == "MTR")
            {

            }
            else if (machineType == "MTP")
            {

            }
        }

        else if (operation == "Substraction")
        {
            if (machineType == "STP")
            {

            }
            else if (machineType == "MTR")
            {

            }
            else if (machineType == "MTP")
            {

            }
        }

        else if (operation == "Multiplication")
        {
            if (machineType == "STP")
            {

            }
            else if (machineType == "MTR")
            {

            }
            else if (machineType == "MTP")
            {

            }
        }

        else if (operation == "Division")
        {
            //if (machineType == "STP")
            //{

            //}
            //else if (machineType == "MTR")
            //{

            //}
            //else if (machineType == "MTP")
            //{

            //}
            string result = "";
            string divider = "1";

            result += DecimalToUnary(firstNumber, firstSign, "0");
            result += divider;
            result += DecimalToUnary(secondNumber, secondSign, "0");

            inputStringText.text = result;
        }

        else if (operation == "Factorial")
        {
            if (machineType == "STP")
            {

            }
            else if (machineType == "MTR")
            {

            }
            else if (machineType == "MTP")
            {

            }
        }

        else if (operation == "Power")
        {
            if (machineType == "STP")
            {

            }
            else if (machineType == "MTR")
            {

            }
            else if (machineType == "MTP")
            {

            }
        }

        else if (operation == "BinaryLogarithm")
        {
            //if (machineType == "STP")
            //{

            //}
            //else if (machineType == "MTR")
            //{

            //}
            //else if (machineType == "MTP")
            //{

            //}
            string result = "";
            string divider = "1";

            result += DecimalToUnary(firstNumber, firstSign, "0");
            result += divider;
            
            inputStringText.text = result;

        }

        else if (operation == "TemperatureConversion")
        {
            if (machineType == "STP")
            {

            }
            else if (machineType == "MTR")
            {

            }
            else if (machineType == "MTP")
            {

            }
        }
    }
}

