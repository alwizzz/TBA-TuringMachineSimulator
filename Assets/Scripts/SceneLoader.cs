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

    [SerializeField] TextAsset[] usedTransitionTables;

    string operation = "Division";
    string operatorSymbol = "/";
    bool hasTwoNumber = true;
    bool hasSign = true;
    int minValue = 1;
    int maxValue = 10;


    private void Awake()
    {
        //DefaultValues();

        Singleton();
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

    void DefaultValues()
    {
        operation = "Division";
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void AdditionButton()
    {
        operatorSymbol = "+";
        operation = "Addition";
        hasTwoNumber = true;
        hasSign = true;
        usedTransitionTables = additionTransitionTables;
        SceneManager.LoadScene(1);
    }
    public void SubstractionButton()
    {
        operatorSymbol = "-";
        operation = "Substraction";
        hasTwoNumber = true;
        hasSign = true;
        usedTransitionTables = substractionTransitionTables;
        SceneManager.LoadScene(1);
    }
    public void MultiplicationButton()
    {
        operatorSymbol = "*";
        operation = "Multiplication";
        hasTwoNumber = true;
        hasSign = true;
        usedTransitionTables = multiplicationTransitionTables;
        SceneManager.LoadScene(1);
    }
    public void DivisionButton()
    {
        operatorSymbol = "/";
        operation = "Division";
        hasTwoNumber = true;
        hasSign = true;
        usedTransitionTables = divisionTransitionTables;
        SceneManager.LoadScene(1);
    }

    public void FactorialButton()
    {
        operatorSymbol = "!";
        operation = "Factorial";
        hasTwoNumber = false;
        hasSign = false;
        usedTransitionTables = factorialTransitionTables;
        SceneManager.LoadScene(1);
    }
    public void PowerButton()
    {
        operatorSymbol = "^";
        operation = "Power";
        hasTwoNumber = true;
        hasSign = false;
        usedTransitionTables = powerTransitionTables;
        SceneManager.LoadScene(1);
    }
    public void BinaryLogarithmButton()
    {
        operatorSymbol = "L";
        operation = "BinaryLogarithm";
        hasTwoNumber = false;
        hasSign = false;
        usedTransitionTables = binaryLogarithmTransitionTables;
        SceneManager.LoadScene(1);
    }
    public void TemperatureConversionButton()
    {
        operatorSymbol = "T";
        operation = "TemperatureConversion";
        hasTwoNumber = false;
        hasSign = true;
        usedTransitionTables = temperatureConversionTransitionTables;
        SceneManager.LoadScene(1);
    }


    public void BackToMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }





    public string GetOperation() => operation;
    public string GetOperatorSymbol() => operatorSymbol;

    public bool HasTwoNumber() => hasTwoNumber;
    public bool HasSign() => hasSign;


}
