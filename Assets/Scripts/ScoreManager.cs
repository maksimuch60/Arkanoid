using System;

public class ScoreManager : SingletonMonoBehavior<ScoreManager>
{
    #region Properties

    public int Score { get; private set; }

    #endregion


    #region Events

    public event Action<int> OnScoreChanged;

    #endregion


    #region Public methods

    public void ChangeScore(int score)
    {
        Score += score;
        OnScoreChanged?.Invoke(Score);
    }

    public void ResetScore()
    {
        Score = 0;
    }

    #endregion
}