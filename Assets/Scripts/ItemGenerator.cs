using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour
{
    public GameObject upThorn; // 위 장애물
    public GameObject downThorn; // 아래 장애물
    public GameObject coin; // 돈

    double timer = 0; // 타이머
    public double limit; // 한계시간

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // 타이머는 시간계속더해줌
        if (timer > limit) // 만약 정한시간보다 타이머가 넘으면
        {
            timer = 0; // 타이머는 0으로 셋

            int random = Random.Range(1, 3); // 1~2까지 랜덤으로 위아래 장애물 선정

            switch (random)
            {
                case 1:
                    {
                        //->ObstacleManager obs = new ThornUp();이랑 같은뜻
                        ObstacleManager obs = this.gameObject.AddComponent<ThornUp>();
                        ThornUp thornup = obs as ThornUp; // obs를 ThornUp으로 형변환
                        if(thornup != null)
                        {
                            thornup.SetInfomation();//정보 셋
                            thornup.MakeObs(upThorn, thornup.Position.x, thornup.Position.y);// 위 뾰족이 만듬
                            Money money = this.gameObject.AddComponent<Money>(); // 머니 클래스 생성
                            money.SetInfomation();// 정보 셋
                            money.MakeObs(coin, thornup.Position.x, -1.5f);//머니 만듬
                        }
                        break;
                    }
                case 2:
                    {
                        ObstacleManager obs = this.gameObject.AddComponent<ThornDown>();
                        ThornDown thorndown = obs as ThornDown; // obs를 ThornDown으로 형변환
                        if (thorndown != null)
                        {
                            thorndown.SetInfomation(); // 정보 셋
                            thorndown.MakeObs(downThorn, thorndown.Position.x, thorndown.Position.y);//아래뾰족이 만듬
                            Money money = this.gameObject.AddComponent<Money>();//머니 클래스 생성
                            money.SetInfomation(); // 정보 셋
                            money.MakeObs(coin, thorndown.Position.x, 0.5f); // 머니 만듬
                        }
                        break;
                    }

            }
        }
    }
}
