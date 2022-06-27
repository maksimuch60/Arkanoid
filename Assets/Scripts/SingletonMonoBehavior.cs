using UnityEngine;

public class SingletonMonoBehavior<T> : MonoBehaviour
{
    #region Variables

    private static T _instance;

    #endregion


    #region Properties

    public static T Instance => _instance;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        if (_instance!= null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = gameObject.GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }

    #endregion
}