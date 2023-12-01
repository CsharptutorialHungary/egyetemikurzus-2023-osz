using System;

public record Guess
{
	public string answer;
    public string guessedword;
    public string feedback;

	public Guess(string currentanswer,string guess)
	{        
        answer = currentanswer.ToUpper();        
        guessedword = guess.ToUpper();
        feedback = GenerateFeedback();
    }

    public string GenerateFeedback()
    {
        return string.Join("", guessedword.Select((letter, index) =>
        {
            if (letter == answer[index])
                return "█"; // Correct letter in the correct position
            if (answer.Contains(letter))
                return "▄"; // Correct letter in the wrong position
            return "X"; // Incorrect letter
        }));
    }

    public string getFeedback()
    {
        return feedback;
    }
    public string getAnswer()
    {
        return answer;
    }
    public string getGuessedword()
    {
        return guessedword;
    }
}
