using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnArcher : MonoBehaviour
{
    [SerializeField] List<ArcherSpawn> playerPrefabs;
    [SerializeField] Camera _camera;

    public Archer RespawnPlayerAtPos(RespawnPosition respawnPosition = RespawnPosition.Stand)
    {
        Archer archer;
        archer = playerPrefabs.Find(x => x.position == respawnPosition).archer;
        archer.GetComponent<BowAimControl>().SetCamera(_camera);
        return archer;
    }
}

[Serializable]
public class ArcherSpawn
{
    public Archer archer;
    public RespawnPosition position;
}

public enum RespawnPosition
{
    Stand = 0, Kneel = 1, Lying = 2
}
