using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class IA : MonoBehaviour {
    private const int inf = 9999;
    private int minOrMax;
    private GameController gameController;

	// Use this for initialization
	void Start () {
        //minOrMax = -1;
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	public void makeAMove(){
		string[] state = new string[9];
		for (int i = 0; i < gameController.buttonList.Length; i++)
			state [i] = gameController.buttonList [i].text;
		int space = minimax (new Node (state));
		GridSpace gridSpace = gameController.buttonList [space].GetComponentInParent<GridSpace> ();
		gridSpace.SetSpace ();
	}

    private int minimax(Node state)
    {
		
		return minValue(new Node(state.getState()))[1];
    }
	private int[] minValue(Node state)
    {
		if (isTerminal (state)) { 
			
			return new int[]{ heuristicFunction (new Node(state.getState())), inf };
		}
		int a = 0;
        int v = inf;
        string[] buttons = state.getState();
       for (int i = 0; i < buttons.Length; i++)
        {
            string button = buttons[i];
			if (button == "") {
				int previousV = v;
				int nV = maxValue (generateState (new Node(state.getState()), i, true)) [0];
				if (!(v <= nV)) {
					v = nV;
					a = i;
				}
			}
        }
		//Debug.Log (state.getDepth());
		return new int[]{v, a};
        
    }
    private int[] maxValue(Node state)
    {
		if (isTerminal(state)) return new int[]{heuristicFunction(new Node(state.getState())), inf};
		int a = 0;
		int v = -inf;
		string[] buttons = state.getState();
		for (int i = 0; i < buttons.Length; i++)
		{
			string button = buttons[i];
			if (button == "") {
				int previousV = v;
				int nV = minValue (generateState (new Node(state.getState()), i, false)) [0];
				if (!(v >= nV)) {
					v = nV;
					a = i;
				}
			}
		};
		return new int[]{v, a};
    }

	private Node generateState(Node state, int button, bool min)
    {
        string[] stateStrings = state.getState().Clone() as string[];
        stateStrings[button] = min? "O" : "X";
        Node node = new Node(stateStrings);
		node.setDepth (state.getDepth () + 1);
		return node;
    }

    private bool isTerminal(Node state)
    {
		return victory (state) != null;
		/*
		if (state.getState()[0] == state.getState()[1] && state.getState()[1] == state.getState()[2] && state.getState()[0] != "")
        {
            return true;
        }
		else if (state.getState()[3] == state.getState()[4] && state.getState()[4] == state.getState()[5] && state.getState()[3] != "")
        {
            return true;
        }
		else if (state.getState()[6] == state.getState()[7] && state.getState()[7] == state.getState()[8]  && state.getState()[6] != "")
        {
            return true;
        }
		else if (state.getState()[0] == state.getState()[3] && state.getState()[3] == state.getState()[6]  && state.getState()[0] != "")
        {
            return true;
        }
		else if (state.getState()[1] == state.getState()[4] && state.getState()[4] == state.getState()[7]  && state.getState()[1] != "")
        {
            return true;
        }
		else if (state.getState()[2] == state.getState()[5] && state.getState()[5] == state.getState()[8] && state.getState()[2] != "")
        {
            return true;
        }
		else if (state.getState()[0] == state.getState()[4] && state.getState()[4] == state.getState()[8] && state.getState()[0] != "")
        {
            return true;
        }
		else if (state.getState()[2] == state.getState()[4] && state.getState()[4] == state.getState()[6] && state.getState()[2] != "")
        {
            return true;
        } else
        {
            bool draw = true;
            foreach(string v in state.getState())
            {
                if(v == "") { draw = false;  break; }
            }
            if (draw)
                return true;
        }
        return false;*/
    }

    private int heuristicFunction(Node state)
    {
		if (victory (state) == "X")
			return 1;
		if (victory (state) == "O")
			return -1;
		return 0;
       /* int v = 0;
        string[][] matrix = { new string[] { state.getState()[0], state.getState()[1], state.getState()[2] }, new string[] { state.getState()[3], state.getState()[4], state.getState()[5] }, new string[] { state.getState()[6], state.getState()[7], state.getState()[8] } };
        //run every row
        for(int i = 0; i< 3; i++)
        {
            int qtdX = 0;
            int qtdO = 0;
            for (int j = 0; j <3; j++)
            {
                if (matrix[i][j] == "X") qtdX++;
                else if (matrix[i][j] == "O") qtdO++;
            }
            if (!(qtdX > 0 && qtdO > 0))
            {
				if (qtdX > 0) {
					v += (int)Math.Pow (10, qtdX);
				} else if (qtdO > 0) {
					v -= (int)Math.Pow (10, qtdO);
				}


            }
        }
        //run every col
        for (int i = 0; i < 3; i++)
        {
            int qtdX = 0;
            int qtdO = 0;
            for (int j = 0; j < 3; j++)
            {
                if (matrix[j][i] == "X") qtdX++;
                else if (matrix[j][i] == "O") qtdO++;
            }
			if (!(qtdX > 0 && qtdO > 0))
			{
				if (qtdX > 0) {
					v += (int)Math.Pow (10, qtdX);
				} else if (qtdO > 0) {
					v -= (int)Math.Pow (10, qtdO);

				}


			}
        }
        //run diagonals
        int qtdX2 = 0;
        int qtdO2 = 0;
        if (matrix[0][0] == "X") qtdX2++;
        else if (matrix[0][0] == "O") qtdO2++;
        if (matrix[1][1] == "X") qtdX2++;
        else if (matrix[1][1] == "O") qtdO2++;
        if (matrix[2][2] == "X") qtdX2++;
        else if (matrix[2][2] == "O") qtdO2++;
        if (!(qtdX2 > 0 && qtdO2 > 0))
        {
			if (qtdX2 > 0) v +=(int) Math.Pow(10, qtdX2);
			else if (qtdO2 > 0) v -= (int)Math.Pow(10, qtdO2);
        }
        qtdX2 = 0;
        qtdO2 = 0;
        if (matrix[0][2] == "X") qtdX2++;
        else if (matrix[0][2] == "O") qtdO2++;
        if (matrix[1][1] == "X") qtdX2++;
        else if (matrix[1][1] == "O") qtdO2++;
        if (matrix[2][0] == "X") qtdX2++;
        else if (matrix[2][0] == "O") qtdO2++;
        if (!(qtdX2 > 0 && qtdO2 > 0))
        {
			if (qtdX2 > 0) v +=(int)Math.Pow(10, qtdX2);
			else if (qtdO2 > 0) v -= (int)Math.Pow(10, qtdO2);
        }
        return v;*/
    }
	void showState(Node state){
		string str = "";
		for (int i = 0; i < 3; i++)
			str += state.getState () [i * 3] + " | " + state.getState () [i * 3 + 1] + " | " + state.getState () [i * 3 + 2] + "\n";
		Debug.Log (str);
	}

	private string victory(Node state){
		string player = "X";
		for (int i = 0; i < 2; i++) {
			if (state.getState()[0] == state.getState()[1] && state.getState()[1] == state.getState()[2] && state.getState()[0] == player)
			{
				return player;
			}
			else if (state.getState()[3] == state.getState()[4] && state.getState()[4] == state.getState()[5] && state.getState()[3] == player)
			{
				return player;
			}
			else if (state.getState()[6] == state.getState()[7] && state.getState()[7] == state.getState()[8]  && state.getState()[6] == player)
			{
				return player;
			}
			else if (state.getState()[0] == state.getState()[3] && state.getState()[3] == state.getState()[6]  && state.getState()[0] == player)
			{
				return player;
			}
			else if (state.getState()[1] == state.getState()[4] && state.getState()[4] == state.getState()[7]  && state.getState()[1] == player)
			{
				return player;
			}
			else if (state.getState()[2] == state.getState()[5] && state.getState()[5] == state.getState()[8] && state.getState()[2] == player)
			{
				return player;
			}
			else if (state.getState()[0] == state.getState()[4] && state.getState()[4] == state.getState()[8] && state.getState()[0] == player)
			{
				return player;
			}
			else if (state.getState()[2] == state.getState()[4] && state.getState()[4] == state.getState()[6] && state.getState()[2] == player)
			{
				return player;
			} else
			{
				bool draw = true;
				foreach(string v in state.getState())
				{
					if(v == "") { draw = false;  break; }
				}
				if (draw)
					return "Draw";
			}
			player = "O";
		}
		return null;
	}
}
