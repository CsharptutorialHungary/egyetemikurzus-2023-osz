using System;

public class Guess
{
	private string _answer;
	private string _guessedword;
	private string _feedback;

	public Guess(string answer,string guess)
	{

        if (answer.Length != 5 || guess.Length != 5)
        {
            throw new ArgumentException("Both answer and guessedWord must be 5 characters long.");
        }
        _answer = answer.ToUpper();
        _guessedword = guess.ToUpper();
        _feedback = GenerateFeedback();
    }

    private string GenerateFeedback()
    {
        return string.Join("", _guessedword.Select((letter, index) =>
        {
            if (letter == _answer[index])
                return "█"; // Correct letter in the correct position
            if (_answer.Contains(letter))
                return "▄"; // Correct letter in the wrong position
            return "X"; // Incorrect letter
        }));
    }

    public string getFeedback()
    {
        return _feedback;
    }
    public string getAnswer()
    {
        return _answer;
    }
    public string getGuessedword()
    {
        return _guessedword;
    }
}
