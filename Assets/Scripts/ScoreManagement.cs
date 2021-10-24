using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public struct ScoreData // 이름과 점수를 갖는 스트럭트
{
    public ScoreData(string name, string score)
    {
        Name = name; Score = score;
    }

    public string Name, Score;
}


public class ScoreManagement : MonoBehaviour
{
    List<ScoreData> scoreList = new List<ScoreData>();// 스코어 리스트
    string filePath; // 파일 위치
    public Text[] scoreText = new Text[3];//스코어 텍스트들
    const int savenum = 3; // 저장할 스코어 수
    public GameObject highscorePanel;//신기록 창
    public InputField input;//인풋필드
    string m_name;//이름
    bool CheckRecord = false; // 신기록인지 아닌지 체크

    void Start()
    {
        filePath = Application.persistentDataPath + "/Score.txt"; //파일 위치
        Debug.Log(filePath);
        LoadFile(); // 시작 시 파일을 로드
        if (SceneManager.GetActiveScene().name == "GameOver")// 게임오버 씬이면
        {
            //달성한 점수가 신기록이면 checkrecord 트루로 바뀜
            NewRecord(PlayerPrefs.GetInt("score"), out CheckRecord); 
            if (CheckRecord) // 달성한 스코어가 순위 안에 들면
            {
                highscorePanel.SetActive(true); //신기록 달성 창 염
                CheckRecord = false;
            }
            else
                PrintScore(); // 스코어 출력
        }
    }
    public void SaveScore() // 점수 저장 버튼 클릭 시
    {
        m_name = input.text;//이름받아옴

        SaveFile(m_name, PlayerPrefs.GetInt("score"));//확인누르면 이름이랑 스코어 저장

        highscorePanel.SetActive(false);// 창닫음

        PrintScore();// 스코어 출력
    }
    public void SaveFile(string name, int score) // 이름과 점수를 파라미터로 받아옴
    {
        //처음 기록 정렬
        scoreList.Sort((x, y) => Int32.Parse(y.Score).CompareTo(Int32.Parse(x.Score)));//내림차순으로 정렬
        
        scoreList.RemoveAt(2);//가장 작은 점수를 삭제

        scoreList.Add(new ScoreData(name, score.ToString()));//이름과 함께 점수를 리스트에 삽입

        var sortList = from content in scoreList
                       orderby Int32.Parse(content.Score) descending // linq로 내림차순 정렬
                       select content;

        if (File.Exists(filePath)) // 스코어파일이 존재하면
        {
            try
            {
                StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Score.txt"); //스코어파일에 쓰기
               
                for(int i =0;i< sortList.ToList().Count;i++)
                {
                    sw.WriteLine(sortList.ToList()[i].Name + '\t' + sortList.ToList()[i].Score + '\t'); // 이름과  스코어 쓰기

                    if (i == sortList.ToList().Count - 1) // 마지막 루프때 닫아줌
                    {
                        sw.Close();//닫음
                    }
                }
            }

            catch (Exception ex) // 예외처리
            {
                Debug.Log(ex);
            }
        }

    }
    void PrintScore()
    {
        StreamReader sr = new StreamReader(Application.persistentDataPath + "/Score.txt");//읽기

        for (int i = 0; i < savenum; i++) // 스코어 출력
        {
            scoreText[i].text = sr.ReadLine(); // 스코어 텍스트 출력
        }
        sr.Close(); // 닫기
    }
    public void LoadFile()
    {
        //파일이 없을 시 초기화해서 저장
        if (!File.Exists(filePath))
        {
            // 이 위치에 스코어 텍스트파일 생성
            StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Score.txt"); 
            for (int i = 0; i < savenum; i++) // 최대 3개만 점수 저장
            {
                scoreList.Add(new ScoreData("미정", "0")); // 초기값 설정
            }

            for (int i = 0; i < scoreList.Count; i++) // 스코어 리스트 만큼 돌림
            {
                try // 오류없을 시 이름과 스코어 저장
                {
                    sw.WriteLine(scoreList[i].Name + '\t' + scoreList[i].Score + '\t'); // 이름과 스코어 저장

                    if (i == scoreList.Count - 1) // 마지막 루프때 닫아줌
                        sw.Close();
                }

                catch (Exception ex) // 안닫혔거나 오류가 났을 때
                {
                    StreamWriter error = new StreamWriter(Application.persistentDataPath + "/ErrorLog.txt"); //에러 로그 텍스트 생성
                    error.Write(ex); // 오류 작성
                    error.Close(); // 닫음
                }
            }
        }

        //이미 파일이 있음
        else
        {
            string[] line; // 한줄안에 컨텐츠 담을거

            StreamReader sr = new StreamReader(Application.persistentDataPath + "/Score.txt"); // 스코어 파일 불러옴

            while (sr.EndOfStream == false) // 끝줄까지
            {
                line = sr.ReadLine().Split('\t'); // 탭으로 스트링 구분

                scoreList.Add(new ScoreData(line[0], line[1])); // 이름과 점수를 리스트에 더함
            }

            sr.Close(); // 닫음
        }
    }
    //파일안에 있는 점수와 비교해서 가지고 온 점수가 더 크다면 점수를 바꿔서 저장함
    public void NewRecord(int score, out bool newRecord)
    {
        newRecord = false; // 기록을 세웠는지

        //리스트 길이만큼 돌림
        for (int i = 0; i < scoreList.Count; i++)
        {
            if (score > Int32.Parse(scoreList[i].Score))// 스코어와 저장된 스코어 비교, 새로 얻은 점수가 더 크다면
            {
                newRecord = true; // 신기록 달성 체크
                if (newRecord)
                    break; // 루프끝
            }
        }

    }

}
