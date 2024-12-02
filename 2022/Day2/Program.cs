var input = await File.ReadAllLinesAsync("input.txt");

var pointMoves = new Dictionary<Move, int>
{
    { Move.Rock, 1 },
    { Move.Paper, 2 },
    { Move.Scissors, 3 }
};

var pointOutcomes = new Dictionary<Result, int>
{
    { Result.Victory, 6 },
    { Result.Draw, 3 },
    { Result.Loss, 0 }
};

var theirMoves = new Dictionary<char, Move>
{
    { 'A', Move.Rock },
    { 'B', Move.Paper },
    { 'C', Move.Scissors }
};

var ourMoves = new Dictionary<char, Strategy>
{
    { 'X', Strategy.Lose },
    { 'Y', Strategy.Draw },
    { 'Z', Strategy.Win }
};

var totalPoints = 0;

foreach (var round in input)
{
    var theirs = theirMoves[round[0]];
    var ours = ourMoves[round[2]];

    var ourPoints = 0;
    if (ours == Strategy.Lose)
    {
        ourPoints += pointOutcomes[Result.Loss];
    }
    else if (ours == Strategy.Draw)
    {
        ourPoints += pointOutcomes[Result.Draw];
    }
    else
    {
        ourPoints += pointOutcomes[Result.Victory];
    }


    var outcome =  EvaluateMove(theirs, ours);

    ourPoints += pointMoves[outcome];
    

    totalPoints += ourPoints;
}

Console.WriteLine(totalPoints);

Move EvaluateMove(Move theirs, Strategy ours)
{
    if (theirs == Move.Rock)
    {
        if (ours == Strategy.Win) return Move.Paper;
        if (ours == Strategy.Draw) return Move.Rock;
        if (ours == Strategy.Lose) return Move.Scissors;
        else throw new InvalidOperationException();
    }
    else if (theirs == Move.Scissors)
    {
        if (ours == Strategy.Win) return Move.Rock;
        if (ours == Strategy.Draw) return Move.Scissors;
        if (ours == Strategy.Lose) return Move.Paper;
        else throw new InvalidOperationException();
    }
    else
    {
        if (ours == Strategy.Win) return Move.Scissors;
        if (ours == Strategy.Draw) return Move.Paper;
        if (ours == Strategy.Lose) return Move.Rock;
        else throw new InvalidOperationException();
    }
}

enum Result {  Loss = -1, Draw = 0, Victory = 1 };

enum Move { Rock, Paper, Scissors };

enum Strategy { Lose, Draw, Win }