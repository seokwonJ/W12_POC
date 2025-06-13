using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "StageSO")]
public class StageSO : ScriptableObject
{
    public int world; // 월드 번호, 1-2 일 경우 1
    public int stage; // 스테이지 번호, 1-2 일 경우 2
    public bool isBossStage; // 보스 스테이지 여부

    public EnemyWaveSO[] enemyWave; // 각 웨이브의 적 구성
    public int[] waveCount; // 각 웨이브를 몇 번씩 소환할지 개수
    public float[] wavePreparationTime; // 각 웨이브를 소환하기 전 대기 시간
}
