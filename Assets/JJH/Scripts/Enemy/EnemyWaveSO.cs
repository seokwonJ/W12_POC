using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "EnemyWaveSO")]
public class EnemyWaveSO : ScriptableObject
{
    public GameObject[] enemyPrefabs; // 소환할 적
    public ESpawnPositionType[] spawnPositionType; // 소환 위치 타입
    public float waveInterval; // 웨이브 소환 사이 시간
    public bool isBossWave; // 보스 웨이브 여부
}

public enum ESpawnPositionType
{
    OffScreenRandom, // 화면 밖 랜덤 위치
    OnScreenRandom, // 화면 안 랜덤 위치
    GlobalRandom, // 화면 밖과 안 모두 랜덤 위치
    RightSideCenter, // 오른쪽 중앙에 배치하기

    // 아래는 곧 지워야합니다.
    RightSideRandom, // 오른쪽 면에서 랜덤 위치
    EvenlySpaced, // 균등한 간격으로 배치하기. 1개면 중앙에 배치하고 2개면 위부터 1/3, 2/3 위치에 배치
                 // 가급적 EvenlySpaced할 적을 먼저 배치해주시기 바랍니다.
            
}