using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "StageSO")]
public class StageSO : ScriptableObject
{
    public int world; // 월드 번호, 1-2 일 경우 1
    public int stage; // 스테이지 번호, 1-2 일 경우 2
    public float stagePlayTime; // 스테이지의 총 플레이 시간

    public EnemyWaveSO[] enemyWave; // 각 웨이브의 적 구성
    public int[] WaveCount; // 각 웨이브를 몇 번씩 소환할지 개수
    public float[] WaveInterval; // 각 웨이브 사이의 시간 간격
}
