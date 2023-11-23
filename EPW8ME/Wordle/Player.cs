using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Player
{
	private string _name;
	private int _score;
	private int _wordsInARow;

	public Player()
	{
		_name = "Anonymus";
		_score = 0;
        _wordsInARow = 0;
	}

	public void updateScoreBy(int newPoints)
	{
		_score += newPoints;
	}




	


    //setter / getters
    public int getScore()
    {
        return _score;
    }
    public void setScore(int score)
	{
		_score = score;
	}	

	public string getName()
	{
		return _name;
	}
	public void setName(string name)
	{ 		
		_name = name;
	}
	public int getWords()
	{
		return _wordsInARow;
	}
	public void setWords(int words)
	{
        _wordsInARow = words;
	}
}
