using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberController : MonoBehaviour
{
    // PROPERTIES
    [SerializeField] int currentValue;
    [SerializeField] string currentSign;
    [SerializeField] int minValue;
    [SerializeField] int maxValue;

    // STATES
    public bool hasSign = true;
    [SerializeField] bool isMinus;
    [SerializeField] bool isSecondNumber;



    // CACHES
    [SerializeField] TextMeshProUGUI numberText;
    //[SerializeField] Button buttonUp;
    //[SerializeField] Button buttonDown;
    public Button buttonSign;
    TextMeshProUGUI signText;
    ConfigMaster configMaster;
    SceneLoader sceneLoader;


    private void Awake()
    {
        configMaster = FindObjectOfType<ConfigMaster>();
        sceneLoader = FindObjectOfType<SceneLoader>();

        minValue = sceneLoader.GetMinValue();
        maxValue = sceneLoader.GetMaxValue();

    }

    // Start is called before the first frame update
    void Start()
    {
        signText = buttonSign.transform.Find("MinusSign").GetComponent<TextMeshProUGUI>();
        DefaultValues();

        configMaster.UpdateNumber(currentValue, currentSign, isSecondNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DefaultValues()
    {
        currentValue = 1;
        UpdateDisplay();

        if (hasSign)
        {
            currentSign = "+";
            isMinus = false;
            UpdateSign();
        } else
        {
            currentSign = "";
        }
    }

    public void ValueUp()
    {
        if(currentValue == maxValue) { return; }
        currentValue++;
        UpdateDisplay();
    }

    public void ValueDown()
    {
        if (currentValue == minValue) { return; }
        currentValue--;
        UpdateDisplay();
    }

    public void ChangeSign()
    {
        isMinus = !isMinus;
        currentSign = (isMinus) ? "-" : "+";
        UpdateSign();
    }

    void UpdateDisplay() 
    { 
        numberText.text = currentValue.ToString();

        configMaster.UpdateNumber(currentValue, currentSign, isSecondNumber);
    }
    //void UpdateSign() { signText.IsActive( (isMinus) ? true : false )}
    void UpdateSign() 
    { 
        signText.gameObject.SetActive((isMinus) ? true : false);

        configMaster.UpdateNumber(currentValue, currentSign, isSecondNumber);
    }

}
