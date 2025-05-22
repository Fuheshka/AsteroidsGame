using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    public UFO UFOPrefab;
    public float spawnRate = 2.0f;
    public int spawnQuantity = 1;
    public float spawnDistance = 15.0f;
    public float trajectoryVariance = 15.0f;
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnQuantity; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            UFO ufO = Instantiate(this.UFOPrefab, spawnPoint, rotation);
            ufO.Size = Random.Range(ufO.minumumSize, ufO.maximumSize);
            ufO.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
