using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// bpm을 json 파일로 저장하는 스크립트 

[System.Serializable]
public class BpmData
{
    public List<string> bpm = new List<string>();   // bpm을 담을 List
}

public static class BpmSave
{
    private static string SavePath => Application.dataPath + "/saves/"; // json 파일 저장 경로

    public static void Save(BpmData bpmData, string saveFileName)   // bpm 데이터와 파일 이름 받음
    {
        if(!Directory.Exists(SavePath))     // 저장 경로가 존재하지 않으면
        {
            Directory.CreateDirectory(SavePath);    // 저장 경로 생성
        }
        
        string saveJson = JsonUtility.ToJson(bpmData, true);    // bpm List json 형식으로 변환

        string saveFilePath = SavePath + saveFileName + ".json";    // 파일 저장 경로 최종

        File.WriteAllText(saveFilePath, saveJson);              // json 형식을 파일에 저장
        Debug.Log("Save Success: " + saveFilePath);             // 정상적으로 작동되었는지 확인
    }
}
