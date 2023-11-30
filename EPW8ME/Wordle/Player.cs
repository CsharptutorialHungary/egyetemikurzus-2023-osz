using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Player
{
    public string name { get; set; }
    public int score { get; set; }
	public int round { get; set; }

	public Player()
	{
		name = string.Empty;
		score = 0;
		round = 0;
	}

	public string getplayer()
	{
		return name+","+ score + "," + round;
	}
}
