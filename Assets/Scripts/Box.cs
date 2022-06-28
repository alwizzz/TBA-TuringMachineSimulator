using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Box : MonoBehaviour
{
    float changeSymbolAnimDuration = 0.3f;
    [SerializeField] Color changeSymbolAnimColor;


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
    public void SetSymbol(string input, bool withAnim = false) 
    {
        if (withAnim)
        {
            StartCoroutine(ChangeSymbolAnimation(input));
        } else
        {
            symbol = input;
            thisText.text = symbol;
        }
    }

    IEnumerator ChangeSymbolAnimation(string input)
    {
        //Debug.Log("oy");
        var thisImage = GetComponent<Image>();
        var currentColor = thisImage.color;
        var flashInDur = changeSymbolAnimDuration / 2;
        var flashOutDur = flashInDur;

        for (float t = 0.01f; t < flashInDur; t += Time.deltaTime)
        {
            thisImage.color = Color.Lerp(currentColor, changeSymbolAnimColor, Mathf.Min(1, t / flashInDur));
            yield return null;
        }

        symbol = input;
        thisText.text = symbol;
        currentColor = thisImage.color;


        for (float t = 0.01f; t < flashOutDur; t += Time.deltaTime)
        {
            thisImage.color = Color.Lerp(currentColor, Color.white, Mathf.Min(1, t / flashOutDur));
            yield return null;
        }


    }

}
