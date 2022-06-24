using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    [SerializeField] List<BoxUI> boxes;
    [SerializeField] string inputString = "BBBBBBBBBB";

    private void Awake()
    {
        DistributeSymbols();
    }
    void Start()
    {
 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DistributeSymbols()
    {
        string currentInput = inputString;
        for (int i = 0; i < boxes.Count; i++)
        {
            if (currentInput.Length > 0)
            {
                var symbol = currentInput.Substring(0, 1);
                currentInput = currentInput.Substring(1);
                boxes[i].SetSymbol(symbol);
            } else
            {
                boxes[i].gameObject.SetActive(false);
            }
        }
    }

    public List<BoxUI> GetBoxes() => boxes;

}
