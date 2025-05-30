using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "EnemyWaveSO")]
public class EnemyWaveSO : ScriptableObject
{
    public GameObject[] enemyPrefabs; // 소환할 적
    public int[] enemyCount; // 소환할 적 개수
    public ESpawnPositionType[] spawnPositionType; // 소환 위치 타입
}

public enum ESpawnPositionType
{
    Random, // 랜덤 위치
    RightSideRandom, // 오른쪽 면에서 랜덤 위치
    EvenlySpaced // 균등한 간격으로 배치하기. 1개면 중앙에 배치하고 2개면 위부터 1/3, 2/3 위치에 배치
                 // 가급적 EvenlySpaced할 적을 먼저 배치해주시기 바랍니다.
}