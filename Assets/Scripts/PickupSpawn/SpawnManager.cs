using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabsToSpawn;
    [SerializeField] private int maxSpawnAmount;
    public static SpawnManager Instance { get; private set; }

    private BoxCollider _boxCollider;
    private int _spawnedPickups;
    private float _spawnDelay = 3f;
    private bool _spawnInProcess;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (TryGetComponent<BoxCollider>(out var boxCollider))
        {
            _boxCollider = boxCollider;
        }
        if (_boxCollider == null)
        {
            Debug.LogError(this + " cant find spawn area");
        }
    }
    private void LateUpdate()
    {
        if (_spawnedPickups < maxSpawnAmount && !_spawnInProcess)
        {
            StartCoroutine(TrySpawnRandomPickup());
        }
    }

    private IEnumerator TrySpawnRandomPickup()
    {
        _spawnInProcess = true;
        
        var randomIndex = Random.Range(0, prefabsToSpawn.Count);
        var selectedPrefab = prefabsToSpawn[randomIndex];
        
        yield return new WaitForSeconds(_spawnDelay);
        if (_spawnedPickups >= maxSpawnAmount) yield break;
        
        Spawn(selectedPrefab);
        _spawnInProcess = false;
    }

    private void Spawn(GameObject prefab)
    {
        var spawnPos = GetRandomPositionInSpawnArea();
        Instantiate(prefab, spawnPos, Quaternion.identity, gameObject.transform);
        _spawnedPickups++;
    }

    private Vector3 GetRandomPositionInSpawnArea()
    {
        var center = _boxCollider.bounds.center;
        var size = _boxCollider.bounds.size;
        var yOffset = 0.5f;
        
        var x = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        var y = yOffset;
        var z = Random.Range(center.z - size.z / 2f, center.z + size.z / 2f);

        return new Vector3(x, y, z);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
