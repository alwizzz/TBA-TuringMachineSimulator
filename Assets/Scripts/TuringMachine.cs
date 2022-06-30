using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//using Newtonsoft.Json;

public class TuringMachine : MonoBehaviour
{
    // CONFIGS
    [SerializeField] string inputString;
    [SerializeField] bool withMoveTapeAnim;
    [SerializeField] bool withChangeSymbolAnim;
    [SerializeField] float moveSpeed;
    [SerializeField] float runDelay;

    // STATES
    [SerializeField] bool isHalting;
    [SerializeField] bool isRunning;

    // CACHES
    [SerializeField] TextAsset jsonFile;

    TransitionTable transitionTable;
    [SerializeField] InfoDisplay infoDisplay;
    [SerializeField] Button stepButton;
    [SerializeField] Button runButton;
    [SerializeField] Button stopButton;
    [SerializeField] Button resetButton;
    [SerializeField] NoticePanel panelNotice;

    SceneLoader sceneLoader;

    // PROPERTIES
    string operation;
    string operationString;
    string type;
    string blankSymbol;
    int currentIndex;
    TransitionTable.State currentState;
    [SerializeField] string currentStateName; 
    [SerializeField] string currentSymbol;
    // STP PROPERTIES
    [Header("STP")]
    [SerializeField] Boxes boxesSTP;
    Box currentBoxSTP;
    // MTR PROPERTIES
    [Header("MTR")]
    [SerializeField] Boxes boxesMTR1;
    [SerializeField] Boxes boxesMTR2;
    Box currentBoxMTR1;
    Box currentBoxMTR2;
    // MTP PROPERTIES
    [Header("MTP")]
    [SerializeField] Boxes boxesMTP1;
    [SerializeField] Boxes boxesMTP2;
    [SerializeField] Boxes boxesMTP3;
    Box currentBoxMTP1;
    Box currentBoxMTP2;
    Box currentBoxMTP3;
    int currentIndexMTP1;
    int currentIndexMTP2;
    int currentIndexMTP3;


    // DEBUG
    [Header("Debug")]
    [SerializeField] TextAsset debugJsonFile;
    
    // Start is called before the first frame update

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        operation = sceneLoader.GetOperation();
        operationString = sceneLoader.GetOperationString();
        Debug.Log(operationString);

        jsonFile = sceneLoader.GetUsedJsonFile();
        jsonFile = (jsonFile == null) ? debugJsonFile : jsonFile;
        
        inputString = sceneLoader.GetInputString();

        transitionTable = JsonUtility.FromJson<TransitionTable>(jsonFile.ToString());
        type = transitionTable.type;

        //if(type == "STP")
        //{
        //    boxesSTP.Spawn(inputString, moveSpeed);
        //} 
        //else if(type == "MTR")
        //{
        //    boxesMTR1.Spawn(inputString, moveSpeed);
        //    boxesMTR2.Spawn("BB", moveSpeed);
        //}
        //else if(type == "MTP")
        //{
        //    boxesMTP1.Spawn(inputString, moveSpeed);
        //    boxesMTP2.Spawn("BB", moveSpeed);
        //    boxesMTP3.Spawn("BB", moveSpeed);
        //}

        Setup();
    }
    void Start()
    {
        isHalting = false;

        stopButton.interactable = false;
    }

    private void Update()
    {
        //UpdateInfo();
    }

    void Setup()
    {

        blankSymbol = transitionTable.blankSymbol;
        FetchStartState();


        if (type == "STP")
        {
            boxesSTP.Spawn(inputString, moveSpeed);

            currentIndex = boxesSTP.startIndex;
            currentBoxSTP = boxesSTP.GetStartBox();
        }
        else if (type == "MTR")
        {
            boxesMTR1.Spawn(inputString, moveSpeed);
            boxesMTR2.Spawn("BB", moveSpeed);

            currentIndex = boxesMTR1.startIndex;
            currentBoxMTR1 = boxesMTR1.GetStartBox();
            currentBoxMTR2 = boxesMTR2.GetStartBox();
        } 
        else if(type == "MTP")
        {
            boxesMTP1.Spawn(inputString, moveSpeed);
            boxesMTP2.Spawn("BB", moveSpeed);
            boxesMTP3.Spawn("BB", moveSpeed);

            currentIndexMTP1 = boxesMTP1.startIndex;
            currentIndexMTP2 = boxesMTP2.startIndex;
            currentIndexMTP3 = boxesMTP3.startIndex;

            currentBoxMTP1 = boxesMTP1.GetStartBox();
            currentBoxMTP2 = boxesMTP2.GetStartBox();
            currentBoxMTP3 = boxesMTP3.GetStartBox();
        }

        UpdateInfo();
    }

    void FetchStartState()
    {
        string startState = transitionTable.startState;
        currentState = transitionTable.getStateWithName(startState);
    }

    // Updates currentStateName and currentSymbol
    void UpdateInfo()
    {
        currentStateName = currentState.name;
        
        if(type == "STP")
        {
            currentSymbol = currentBoxSTP.GetSymbol();
        }
        else if(type == "MTR")
        {
            currentSymbol = currentBoxMTR1.GetSymbol();
            currentSymbol += currentBoxMTR2.GetSymbol();
        }
        else if(type == "MTP")
        {
            currentSymbol = currentBoxMTP1.GetSymbol();
            currentSymbol += currentBoxMTP2.GetSymbol();
            currentSymbol += currentBoxMTP3.GetSymbol();
        }
    }


    public void Step()
    {
        if (!isHalting)
        {
            string nextStateName = "dummy";
            string nextSymbol = "dummy";
            string direction = "dummy";
            
            bool found = false;
            foreach (TransitionTable.State.Transition t in currentState.transitions)
            {
                //Debug.Log(t.read);

                if (t.read == currentSymbol)
                {
                    //Debug.Log("Oy");

                    found = true;

                    // if symbol get changed
                    if (currentSymbol != t.write)
                    {
                        ChangeSymbol(t.write);
                    }
                    nextSymbol = t.write;

                    // move tape
                    MoveTape(t.direction);
                    direction = t.direction;
                    //if (t.direction == "L") { currentBoxSTP = boxesSTP.MoveLeft(--currentIndex); }
                    //else if (t.direction == "R") { currentBoxSTP = boxesSTP.MoveRight(++currentIndex); }

                    // if state get changed
                    if (currentStateName != t.nextState) 
                    {
                        currentState = transitionTable.getStateWithName(t.nextState); 
                    }
                    nextStateName = t.nextState;

                    break;
                }
            }


            if (found) 
            {
                Debug.Log($"\n<b>{currentStateName}</b> \"{currentSymbol}\" ==> <b>{nextStateName}</b> \"{nextSymbol}\" {direction}");
                UpdateInfo(); 
            }
            else 
            {
                Debug.Log($"\n<b>{currentStateName}</b> \"{currentSymbol}\" ==> HALTS");
                Halt(); 
            }
        }
    }

    void ChangeSymbol(string write)
    {
        if(type == "STP")
        {
            currentBoxSTP.SetSymbol(write, withChangeSymbolAnim);
        } 
        else if(type == "MTR")
        {
            var writeSymbol1 = write.Substring(0, 1);
            var currentSymbol1 = currentSymbol.Substring(0, 1);
            if(writeSymbol1 != currentSymbol1)
            {
                currentBoxMTR1.SetSymbol(writeSymbol1, withChangeSymbolAnim);
            }

            var writeSymbol2 = write.Substring(1, 1);
            var currentSymbol2 = currentSymbol.Substring(1, 1);
            if (writeSymbol2 != currentSymbol2)
            {
                currentBoxMTR2.SetSymbol(writeSymbol2, withChangeSymbolAnim);
            }
        }
        else if (type == "MTP")
        {
            var writeSymbol1 = write.Substring(0, 1);
            var currentSymbol1 = currentSymbol.Substring(0, 1);
            if (writeSymbol1 != currentSymbol1)
            {
                currentBoxMTP1.SetSymbol(writeSymbol1, withChangeSymbolAnim);
            }

            var writeSymbol2 = write.Substring(1, 1);
            var currentSymbol2 = currentSymbol.Substring(1, 1);
            if (writeSymbol2 != currentSymbol2)
            {
                currentBoxMTP2.SetSymbol(writeSymbol2, withChangeSymbolAnim);
            }

            var writeSymbol3 = write.Substring(2, 1);
            var currentSymbol3 = currentSymbol.Substring(2, 1);
            if (writeSymbol3 != currentSymbol3)
            {
                currentBoxMTP3.SetSymbol(writeSymbol3, withChangeSymbolAnim);
            }
        }

    }

    void MoveTape(string direction)
    {
        if(type == "STP")
        {
            if (direction == "L")
            {
                currentIndex--;
                currentBoxSTP = boxesSTP.MoveLeft(currentIndex, withMoveTapeAnim);

            }
            else if(direction == "R")
            {
                currentIndex++;
                currentBoxSTP = boxesSTP.MoveRight(currentIndex, withMoveTapeAnim);

            }
        }
        else if(type == "MTR")
        {
            if (direction == "L")
            {
                currentIndex--;
                currentBoxMTR1 = boxesMTR1.MoveLeft(currentIndex, withMoveTapeAnim);
                currentBoxMTR2 = boxesMTR2.MoveLeft(currentIndex, withMoveTapeAnim);

            }
            else if (direction == "R")
            {
                currentIndex++;
                currentBoxMTR1 = boxesMTR1.MoveRight(currentIndex, withMoveTapeAnim);
                currentBoxMTR2 = boxesMTR2.MoveRight(currentIndex, withMoveTapeAnim);

            }
        }
        else if(type == "MTP")
        {
            var dir1 = direction.Substring(0, 1);
            if(dir1 != "S")
            {
                if(dir1 == "L")
                {
                    currentIndexMTP1--;
                    currentBoxMTP1 = boxesMTP1.MoveLeft(currentIndexMTP1, withMoveTapeAnim);
                }
                else if (dir1 == "R")
                {
                    currentIndexMTP1++;
                    currentBoxMTP1 = boxesMTP1.MoveRight(currentIndexMTP1, withMoveTapeAnim);
                }
            }
            
            var dir2 = direction.Substring(1, 1);
            if (dir2 != "S")
            {
                if (dir2 == "L")
                {
                    currentIndexMTP2--;
                    currentBoxMTP2 = boxesMTP2.MoveLeft(currentIndexMTP2, withMoveTapeAnim);
                }
                else if (dir2 == "R")
                {
                    currentIndexMTP2++;
                    currentBoxMTP2 = boxesMTP2.MoveRight(currentIndexMTP2, withMoveTapeAnim);
                }
            }

            var dir3 = direction.Substring(2, 1);
            if (dir3 != "S")
            {
                if (dir3 == "L")
                {
                    currentIndexMTP3--;
                    currentBoxMTP3 = boxesMTP3.MoveLeft(currentIndexMTP3, withMoveTapeAnim);
                }
                else if (dir3 == "R")
                {
                    currentIndexMTP3++;
                    currentBoxMTP3 = boxesMTP3.MoveRight(currentIndexMTP3, withMoveTapeAnim);
                }
            }


        }

    }

    public void Run()
    {
        stopButton.interactable = true;
        runButton.interactable = false;
        stepButton.interactable = false;
        resetButton.interactable = false;

        isRunning = true;
        StartCoroutine(RunCoroutine());
    }

    IEnumerator RunCoroutine()
    {
        while (!isHalting && isRunning)
        {
            Step();
            yield return new WaitForSeconds(runDelay);
        }
    }

    IEnumerator QuickDelay(float delay) { yield return new WaitForSeconds(delay); }

    public void Stop()
    {
        isRunning = false;
        StartCoroutine(QuickDelay(runDelay));

        stopButton.interactable = false;
        runButton.interactable = true;
        stepButton.interactable = true;
        resetButton.interactable = true;
    }

    public void ResetSession()
    {
        boxesSTP.Despawn();

        boxesMTR1.Despawn();
        boxesMTR2.Despawn();

        boxesMTP1.Despawn();
        boxesMTP2.Despawn();
        boxesMTP3.Despawn();

        isHalting = false;
        Setup();
        //if (type == "STP")
        //{
        //    currentIndex = boxesSTP.startIndex;
        //    currentBoxSTP = boxesSTP.GetStartBox();
        //}
        //else if (type == "MTR")
        //{
        //    currentIndex = boxesMTR1.startIndex;
        //    currentBoxMTR1 = boxesMTR1.GetStartBox();
        //    currentBoxMTR2 = boxesMTR2.GetStartBox();
        //}
        //else if (type == "MTP")
        //{
        //    currentIndexMTP1 = boxesMTP1.startIndex;
        //    currentIndexMTP2 = boxesMTP2.startIndex;
        //    currentIndexMTP3 = boxesMTP3.startIndex;

        //    currentBoxMTP1 = boxesMTP1.GetStartBox();
        //    currentBoxMTP2 = boxesMTP2.GetStartBox();
        //    currentBoxMTP3 = boxesMTP3.GetStartBox();
        //}

    }

    void Halt()
    {
        isHalting = true;
        bool isAcceptingState = false;
        // check if currentState is an accepting state
        foreach(string acc in transitionTable.acceptStates)
        {
            if(currentStateName == acc)
            {
                isAcceptingState = true;
                break;
            }
        }

        if (isAcceptingState)
        {
            string outputString = "dummy";
            string result = "dummy";

            if (type == "STP")
            {
                outputString = boxesSTP.GetOutputString();
            }
            else if(type == "MTR")
            {
                outputString = boxesMTR1.GetOutputString();
            }
            else if(type == "MTP")
            {
                outputString = boxesMTP1.GetOutputString();
            }

            result = ResultParser(outputString);

            infoDisplay.UpdateDisplay(
                $"Halted in {currentStateName} (accepting state)\n" +
                $"Operation: {operationString}\n" +
                $"Input: {inputString}\n" +
                $"Output: {outputString}\n" +
                $"Result: {result}"
            ); ;
        } else
        {
            infoDisplay.UpdateDisplay($"Halted in {currentStateName} (non accepting state)");
        }

        panelNotice.Show();
    }
    
    string ResultParser(string output)
    {
        if (operation == "Addition")
        {
            if (type == "STP")
            {

            }
            else if (type == "MTR")
            {

            }
            else if (type == "MTP")
            {

            }
        }

        else if (operation == "Substraction")
        {
            if (type == "STP")
            {

            }
            else if (type == "MTR")
            {

            }
            else if (type == "MTP")
            {

            }
        }

        else if (operation == "Multiplication")
        {
            if (type == "STP")
            {

            }
            else if (type == "MTR")
            {

            }
            else if (type == "MTP")
            {

            }
        }

        else if (operation == "Division")
        {
            string result = "";

            string signSymbol = output.Substring(0, 1);
            int value = output.Substring(1).Length;

            if(value != 0)
            {
                result += (signSymbol == "+") ? "" : signSymbol;
            }
            result += value.ToString();

            return result;
        }

        else if (operation == "Factorial")
        {
            if (type == "STP")
            {

            }
            else if (type == "MTR")
            {

            }
            else if (type == "MTP")
            {

            }
        }

        else if (operation == "Power")
        {
            if (type == "STP")
            {

            }
            else if (type == "MTR")
            {

            }
            else if (type == "MTP")
            {

            }
        }

        else if (operation == "BinaryLogarithm")
        {
            return output.Length.ToString();
        }

        else if (operation.Substring(0,12) == "TemperatureConversion")
        {
            var subOperation = operation.Substring(21, 2);
            string result = "";
            int value = 0;

            string signSymbol = output.Substring(0, 1);
            int i;
            for (i = 1; ; i++)
            {
                var currentChar = output[i].ToString();
                if (currentChar == "+" || currentChar == "-")
                {
                    break;
                }
            }
            value = i - 1;

            if(subOperation == "CK")
            {
                value += 273;
            }
            else if (subOperation == "KC")
            {
                value -= 273;
            }
            else if (subOperation == "CF")
            {
                value += 32;
            }
            else if (subOperation == "FC")
            {
                value -= 18;
            }
            else if (subOperation == "KF")
            {
                value -= 459;
            }
            else if (subOperation == "FK")
            {
                value += 255;
            }


            if (value != 0)
            {
                result += (signSymbol == "+") ? "" : signSymbol;
            }
            result += value.ToString();
            return result;

            //if (subOperation == "CK" || subOperation == "KC")
            //{
            //    string signSymbol = output.Substring(0, 1);
            //    int i;
            //    for(i=1; ; i++)
            //    {
            //        var currentChar = output[i].ToString();
            //        if(currentChar == "+" || currentChar == "-")
            //        {
            //            break;
            //        }
            //    }
            //    value = i - 1;


            //    if (value != 0)
            //    {
            //        result += (signSymbol == "+") ? "" : signSymbol;
            //    }
            //    result += value.ToString();

            //}

            //int value = output.Substring(1).Length;

            //if (value != 0)
            //{
            //    result += (signSymbol == "+") ? "" : signSymbol;
            //}
            //result += value.ToString();

            //return result;


        }

        return "error on ResultParser";
    }
}
