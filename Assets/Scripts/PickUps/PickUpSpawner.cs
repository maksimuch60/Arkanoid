using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickUpSpawner : MonoBehaviour
{
    #region Variables

    [SerializeField] private List<PickUps> _pickUps;
    

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        Block.OnPickUpSpawned += SpawnPickUp;
    }

    private void OnDestroy()
    {
        Block.OnPickUpSpawned -= SpawnPickUp;
    }

    #endregion


    #region Private methods

    private void SpawnPickUp(Block block)
    {
        float pickUpChance = Random.Range(0f, 1f);
        Debug.Log($"{pickUpChance}");
        for (int i = 0; i < _pickUps.Count; i++)
        {
            if (_pickUps[i].Chance >= pickUpChance)
            {
                Debug.Log($"{_pickUps[i].PickUp.name}");
                Instantiate(_pickUps[i].PickUp, block.transform.position, Quaternion.identity);
                break;
            }
        }
    }

    #endregion
}