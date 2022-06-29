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
    [SerializeField] string operation;



    private void Awake()
    {
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

    public void QuitButton()
    {
        Application.Quit();
    }

    public void DivisionButton()
    {
        operation = "Division";
        usedTransitionTables = divisionTransitionTables;
        SceneManager.LoadScene(1);
    }
}
