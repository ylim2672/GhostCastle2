using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class ObstacleManager : MonoBehaviour
{
    public struct Vector //포지션을 지정할 구조체
    {
        public float x;
        public float y;

        public Vector(float _x, float _y)
        {
            x = _x;
            y = _y;
        }
    };

    //장애물 위치 초기화
    public Vector Position;
    
    public string obsName; //장애물 이름
    //장애물 이름과 위치 정보 셋 함수
    public virtual void SetInfomation() { obsName = ""; Position = new Vector(0f, 0f); } 

    public void MakeObs(GameObject obs, float x, float y) // 장애물 생성 함수
    {
        GameObject Curobs = Instantiate(obs) as GameObject; // 장애물 생성
       
        Curobs.transform.position = new Vector3(x, y, 0); // 장애물 위치 지정
    }
}
class ThornUp : ObstacleManager
{
    public override void SetInfomation() // 부모 클래스 가상메소드 오버라이드
    {
        obsName = "위 뾰족"; // 이름 지정
        Position = new Vector(10f, 0.17f); // 포지션 지정
    }
}

class ThornDown : ObstacleManager
{
    public override void SetInfomation() // 부모 클래스 가상메소드 오버라이드
    {
        obsName = "아래 뾰족"; // 이름 지정
        Position = new Vector(10f, -1.22f); // 포지션 지정
    }
}

class Money : ObstacleManager
{
    public override void SetInfomation() // 부모 클래스 가상메소드 오버라이드
    {
        obsName = "동전"; // 이름 지정
    }
}
