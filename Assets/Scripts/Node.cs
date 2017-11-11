using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node {
    private string[] state;
	private int depth;
	private int trueHeuristic;
	private int change;
	public int getChange(){return change;}
	public void setChange(int change){
		this.change = change;
	} 
    public Node(string[] state)
    {
        this.state = state.Clone() as string[];
		setDepth (0);
    }
	public int getTrueHeuristic(){
		return trueHeuristic;
	}
	public void setQtd(int v){
		trueHeuristic = v;
	}
	public int getDepth(){
		return depth;
	}
	public void setDepth(int v){
		this.depth = v;
	}
    public string[] getState()
    {
        return state;
    }
}
