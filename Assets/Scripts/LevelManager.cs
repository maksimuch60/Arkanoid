using System;

public class LevelManager : SingletonMonoBehavior<LevelManager>
{
    #region Variables

    private int _blockCount;

    #endregion


    #region Events

    public event Action OnAllBlocksDestroyed;

    #endregion


    #region Unity lifecycle

    private new void Awake()
    {
        base.Awake();
        Block.OnCreated += BlockCreate;
        Block.OnDestroyed += BlockDestroyed;
    }

    private void OnDestroy()
    {
        Block.OnCreated -= BlockCreate;
        Block.OnDestroyed -= BlockDestroyed;
    }

    #endregion


    #region Private methods

    private void BlockCreate()
    {
        _blockCount++;
    }

    private void BlockDestroyed()
    {
        _blockCount--;

        if (_blockCount == 0)
        {
            OnAllBlocksDestroyed?.Invoke();
        }
    }

    #endregion
}