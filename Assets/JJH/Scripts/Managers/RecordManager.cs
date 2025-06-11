using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RecordManager // 적에게 가한 피해, 적 처치한 수 등을 기록
{
    Dictionary<ECharacterType, int> totalDamageRecord = new Dictionary<ECharacterType, int>();
    Dictionary<ECharacterType, int> stageDamageRecord = new Dictionary<ECharacterType, int>();


    public void AddStageDamgeRecord(ECharacterType eCharacterType, int damage)
    {
        if (stageDamageRecord.ContainsKey(eCharacterType))
        {
            stageDamageRecord[eCharacterType] += damage; // 기존 기록에 추가
        }
        else
        {
            stageDamageRecord[eCharacterType] = damage; // 처음 기록
        }
    }

    public void AddTotalDamageRecord() // 스테이지가 끝날 때마다 총 데미지 레코드에 기록을 더하기
    {
        foreach (var kvp in stageDamageRecord)
        {
            if (totalDamageRecord.ContainsKey(kvp.Key))
            {
                totalDamageRecord[kvp.Key] += kvp.Value; // 기존 기록에 추가
            }
            else
            {
                totalDamageRecord[kvp.Key] = kvp.Value; // 처음 기록
            }
        }
    }

    public void ClearStageDamageRecord() // 다음 스테이지가 시작되면 스테이지 데미지 레코드 초기화
    {
        stageDamageRecord.Clear();
    }

    public int GetDamageRecord(bool isStage, ECharacterType eCharacterType) // isStage가 true면 스테이지에서 가한 피해량, false면 총 가한 피해 기록을 반환
    {
        ref Dictionary<ECharacterType, int> damageRecord = ref (isStage ? ref stageDamageRecord : ref totalDamageRecord);
        if (damageRecord.TryGetValue(eCharacterType, out int damage))
        {
            return damage; // 해당 캐릭터의 피해 기록 반환
        }
        return 0; // 기록이 없으면 0 반환
    }

    public void PrintAllDamageRecord(bool isStage) // isStage가 true면 스테이지에서 가한 피해량, false면 총 가한 피해 기록을 출력
    {
        foreach (var kvp in (isStage ? stageDamageRecord : totalDamageRecord))
        {
            Debug.Log($"{(isStage ? "스테이지" : "총")} {kvp.Key} 피해 기록: {kvp.Value}");
        }
    }
}
