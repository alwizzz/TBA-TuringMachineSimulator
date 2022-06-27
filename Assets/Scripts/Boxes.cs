using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boxes : MonoBehaviour
{
    // CONFIGS
    [SerializeField] float canvasScale;
    //float moveSpeed = 10;
    [SerializeField] int boxCount = 5;
    public int startIndex = 5;
    Vector3 offset;

    // STATES

    // PROPERTIES
    [SerializeField] string inputString = "BBBBBBBBBB";


    // CACHES
    [SerializeField] Box boxPrefab;
    [SerializeField] List<Box> boxes;


    //float boxWidth;





    private void Awake()
    {
        Setup();

        GenerateBox();
        DistributeSymbols();
    }
    void Start()
    {

    }

    void Setup()
    {
        var boxWidth = boxPrefab.gameObject.GetComponent<RectTransform>().sizeDelta.x * canvasScale;
        offset = new Vector3(boxWidth, 0, 0);
    }

    void GenerateBox()
    {
        //Debug.Log(transform.position);
        for(int i=0; i<boxCount; i++)
        {
            Vector3 boxPos;
            if (i == 0) { boxPos = transform.position; }
            else
            {
                boxPos = boxes[i - 1].GetComponent<RectTransform>().position + offset;
            }

            var newBox = Instantiate(
                boxPrefab,
                boxPos,
                Quaternion.identity
            );

            boxes.Add(newBox);
            newBox.transform.SetParent(transform, true);
            newBox.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void DistributeSymbols()
    {
        string currentInput = inputString;
        for (int i = 0; i < boxes.Count; i++)
        {
            //boxes[i].SetSymbol(i.ToString());


            if (i < startIndex)
            {
                boxes[i].SetSymbol("B");
            }
            else if (currentInput.Length > 0)
            {
                var symbol = currentInput.Substring(0, 1);
                currentInput = currentInput.Substring(1);
                boxes[i].SetSymbol(symbol);
            }
            else
            {
                boxes[i].SetSymbol("B");
            }
        }
    }

    public List<Box> GetBoxes() => boxes;

    public Box GetStartBox() => boxes[startIndex];

    public Box MoveRight(int i)
    {
        transform.position -= offset;
        return boxes[i];
    }

    public Box MoveLeft(int i)
    {
        transform.position += offset;
        return boxes[i];
    }

    public string GetInputOutputString()
    {
        string outputString = "";
        bool isReading = false;
        foreach(Box box in boxes)
        {
            if(box.GetSymbol() != "B") { isReading = true; }
            if (isReading && box.GetSymbol() != "B")
            {
                outputString += box.GetSymbol();
            } else if(isReading && box.GetSymbol() == "B")
            {
                break;
            }
        }

        var result = $"input: {inputString}" + "\n" + $"output: {outputString}";
        return result;
    }

}
