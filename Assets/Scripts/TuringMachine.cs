using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//using Newtonsoft.Json;

public class TuringMachine : MonoBehaviour
{
    // CONFIGS

    // STATES
    [SerializeField] bool isHalting;
    [SerializeField] bool isRunning;
    [SerializeField] float runDelay;

    // PROPERTIES
    int currentIndex;
    Box currentBox;
    TransitionTable.State currentState;
    string blankSymbol;
    [SerializeField] string currentStateName;
    [SerializeField] string currentSymbol;

    // CACHES
    [SerializeField] TextAsset jsonFile;
    [SerializeField] Boxes boxes;
    TransitionTable transitionTable;
    [SerializeField] InfoDisplay infoDisplay;
    [SerializeField] Button stepButton;
    [SerializeField] Button runButton;
    [SerializeField] Button stopButton;


    // Start is called before the first frame update
    void Start()
    {
        isHalting = false;
        Setup();

        stopButton.interactable = false;
    }

    private void Update()
    {
        //UpdateInfo();
    }

    void Setup()
    {
        transitionTable = JsonUtility.FromJson<TransitionTable>(jsonFile.ToString());

        blankSymbol = transitionTable.blankSymbol;
        FetchStartState();

        currentBox = boxes.GetStartBox();
        UpdateInfo();

        currentIndex = boxes.startIndex;
    }

    void FetchStartState()
    {
        string startState = transitionTable.startState;
        currentState = transitionTable.getStateWithName(startState);
    }

    void UpdateInfo()
    {
        currentStateName = currentState.name;
        currentSymbol = currentBox.GetSymbol();
    }


    public void Step()
    {
        if (!isHalting)
        {
            bool found = false;
            foreach (TransitionTable.State.Transition t in currentState.transitions)
            {
                //Debug.Log(t.read);

                if (t.read == currentSymbol)
                {
                    //Debug.Log("Oy");

                    found = true;

                    // if symbol get changed
                    if (currentSymbol != t.write) { currentBox.SetSymbol(t.write); }

                    // move tape
                    if (t.direction == "L") { currentBox = boxes.MoveLeft(--currentIndex); }
                    else if (t.direction == "R") { currentBox = boxes.MoveRight(++currentIndex); }

                    // if state get changed
                    if (currentStateName != t.nextState) { currentState = transitionTable.getStateWithName(t.nextState); }

                    break;
                }
            }

            if (found) { UpdateInfo(); }
            else { Halt(); }
        }
    }

    public void Run()
    {
        stopButton.interactable = true;
        runButton.interactable = false;
        stepButton.interactable = false;

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
            var inputOutputString = boxes.GetInputOutputString();
            infoDisplay.UpdateDisplay($"Halts in {currentStateName} (accepting state)\n{inputOutputString}");
        } else
        {
            infoDisplay.UpdateDisplay($"Halts in {currentStateName} (non accepting state)");
        }
    }
    
}
