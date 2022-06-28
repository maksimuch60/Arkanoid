using System;

public class ScoreManager : SingletonMonoBehavior<ScoreManager>
{
    #region Variables

    private int _score;
    private Action<int> _callback;

    #endregion


    #region Public methods

    public void SetListener(Action<int> setScoreLabelText)
    {
        _callback = setScoreLabelText;
    }

    public void IncrementScore(int score)
    {
        _score += score;
        _callback?.Invoke(_score);
    }

    #endregion
}