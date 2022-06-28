using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boxes : MonoBehaviour
{
    // CONFIGS
    [SerializeField] float canvasScale;
    float moveSpeed = 4;
    [SerializeField] int boxCount = 5;
    public int startIndex = 5;
    Vector3 offset;

    // STATES
    bool isMoving = false;
    [SerializeField] bool isMovingRight = false;
    [SerializeField] bool isMovingLeft = false;



    // PROPERTIES
    [SerializeField] string inputString = "BBBBBBBBBB";
    Vector3 destination;


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

    private void Update()
    {
        if (isMoving)
        {
            //Debug.Log("oy");
            Move();
        }
    //else if (isMovingRight)
    //{

    //}
    }

    void Move()
    {
        var distance = destination.x - transform.position.x;
        //Debug.Log(distance);
        if (isMovingRight)
        {
            if (Mathf.Abs(distance) >= 0.01)
            {

                if (distance <= 0)
                {
                    Debug.Log("ke kanan gerak kanan");

                    transform.Translate(Time.deltaTime * moveSpeed * Vector2.left);
                }
                //else
                //{
                //    Debug.Log("ke kanan gerak kiri");

                //    transform.Translate(Time.deltaTime * (moveSpeed/2) * Vector2.right);
                //}
            }
            else
            {
                isMoving = false;
                isMovingRight = false;
            }
        } else if (isMovingLeft)
        {
            //Debug.Log("ke kiri");

            if (Mathf.Abs(distance) >= 0.01)
            {

                if (distance >= 0)
                {
                    Debug.Log("ke kiri gerak kiri");

                    transform.Translate(Time.deltaTime * moveSpeed * Vector2.right);
                }
                //else
                //{
                //    Debug.Log("ke kiri gerak kanan");

                //    transform.Translate(Time.deltaTime * (moveSpeed/2) * Vector2.left);
                //}
            }
            else
            {
                isMoving = false;
                isMovingLeft = false;
            }
        }
        if(Mathf.Abs(distance) >= 0.01)
        {

            if(distance <= 0)
            {
                transform.Translate(Time.deltaTime * moveSpeed * Vector2.left);
            } else
            {
                transform.Translate(Time.deltaTime * moveSpeed * Vector2.right);
            }
        } else
        {
            isMoving = false;
        }
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
        //transform.position -= offset;
        //return boxes[i];

        destination = transform.position - offset;
        isMovingRight = true;
        isMoving = true;

        return boxes[i];
    }

    public Box MoveLeft(int i)
    {
        destination = transform.position + offset;
        isMovingLeft = true;
        isMoving = true;


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
