using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    SceneLoader sceneLoader;

    
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitButton()
    {
        sceneLoader.QuitButton();
    } //Quit
    public void BackToMainMenuButton()
    {
        sceneLoader.BackToMainMenuButton();
    } //BackToMainMenu
    public void NextButton()
    {
        sceneLoader.NextButton();
    }






    public void AdditionButton()
    {
        sceneLoader.AdditionButton();
    }
    public void SubstractionButton()
    {
        sceneLoader.SubstractionButton();
    }
    public void MultiplicationButton()
    {
        sceneLoader.MultiplicationButton();
    }
    public void DivisionButton()
    {
        sceneLoader.DivisionButton();
    }
    public void FactorialButton()
    {
        sceneLoader.FactorialButton();
    }
    public void PowerButton()
    {
        sceneLoader.PowerButton();
    } //Power
    public void BinaryLogarithmButton()
    {
        sceneLoader.BinaryLogarithmButton();
    } //BinaryLogarithm
    public void TemperatureConversionButton()
    {
        sceneLoader.TemperatureConversionButton();
    } //TemperatureConversion
}
