//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Pointer : MonoBehaviour
//{
//    //CONFIGS
//    [SerializeField] Vector3 offset = new Vector2(0, 2);

//    //STATES
//    [SerializeField] string currentState;
//    [SerializeField] string currentSymbol;
//    [SerializeField] bool isAcceptingState;
//    [SerializeField] bool isHalting;

//    //CACHE
//    List<BoxUI> boxes;
//    [SerializeField] List<Transition> transitionTable;
//    Transition currentTransition;
//    [SerializeField] BoxUI pointedBox;
//    [SerializeField] int pointedBoxIndex;
//    [SerializeField] InfoDisplay infoDisplay;

//    private void Awake()
//    {
//        boxes = FindObjectOfType<Boxes>().GetBoxes();
//        //CreateTransitionTable();
//    }
//    // Start is called before the first frame update
//    void Start()
//    {
//        FindHead();
//        currentState = "Q0";
//        currentSymbol = pointedBox.GetSymbol();
//        CheckIfIsAcceptingState();
//        isHalting = false;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        UpdatePosition();

//        CheckIfIsAcceptingState();
//        if (isHalting)
//        {
//            string message = "is halting, ";
//            string add = (isAcceptingState) ? "state is in accepting state" : "state is NOT in accepting state";
//            message = message + add;
//            //Debug.Log(message);
//            infoDisplay.UpdateDisplay(message);
//        }
//    }

//    void FindHead()
//    {
//        for(int i=0; i<boxes.Count; i++)
//        {
//            if(boxes[i].GetSymbol() != "B")
//            {
//                pointedBox = boxes[i];
//                pointedBoxIndex = i;
//                break;
//            }
//        }

//    }

//    void UpdatePosition()
//    {
//        transform.position = pointedBox.transform.position + offset;
//    }

//    void UpdateSymbol()
//    {
//        currentSymbol = pointedBox.GetSymbol();
//    }

//    void CheckIfIsAcceptingState()
//    {
//        if(currentState == "Q6" || currentState == "Q7" || currentState == "Q8")
//        {
//            isAcceptingState = true;
//        } else
//        {
//            isAcceptingState = false;
//        }
//    }

//    public void Continue()
//    {
//        if (isHalting) { return; }
//        BoxUI tempBox = pointedBox;

//        foreach(Transition transition in transitionTable)
//        {
//            if(transition.state == currentState && transition.symbol == currentSymbol)
//            {
//                string debug = transition.state + "," + transition.symbol + " / " + transition.nextState + "," + transition.nextSymbol + "," + transition.direction;
//                Debug.Log(debug);
//                currentState = transition.nextState;
//                pointedBox.SetSymbol(transition.nextSymbol);
//                MoveToNextBox(transition.direction);
//                break;
//            } 

//        }

//        if(pointedBox == tempBox) // means box not moving
//        {
//            isHalting = true;
//        }
//    }

//    void MoveToNextBox(string direction)
//    {
//        if(direction == "L")
//        {
//            pointedBoxIndex--;
//        } else if(direction == "R")
//        {
//            pointedBoxIndex++;
//        }

//        pointedBox = boxes[pointedBoxIndex];
//        currentSymbol = pointedBox.GetSymbol();
//    }
//}
