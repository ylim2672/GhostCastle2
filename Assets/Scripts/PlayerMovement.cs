using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour, Coin //인터페이스
{
    int coin = 0; // 코인
    float score = 0; // 점수
    public Text cointext; // 코인 표시 텍스트
    public Text scoretext; // 점수 표시 텍스트
    //플립 체크
    bool filp = true;

    public AudioClip[] effectSound; //이펙트 사운드
    //플립을 위한 스프라이트 렌더러
    SpriteRenderer rend;

    //기본 포지션
    Vector2 Position = new Vector2(-7.6f, 0.55f);
    //플립 포지션
    Vector2 flipPosition = new Vector2(-7.6f, -1.55f);
    void Start()
    {
        if (PlayerPrefs.HasKey("coin")) // 스트링 코인을 가지고 있으면
            coin = PlayerPrefs.GetInt("coin"); // 코인 값에 넣어줌
        rend = gameObject.transform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        score += Time.deltaTime; // 시간만큼 점수 더하기
        //점수 반올림 한 뒤 스트링으로 바꾸고 텍스트로 표현
        scoretext.text = "Score: " + Mathf.Round(score).ToString();
        if (Input.GetMouseButtonDown(0)) // 마우스클릭 시
        {
            SoundManager.Inst.EffectSound(effectSound[0]); // 클릭 사운드 이펙트 넣어줌 싱글톤 사용
            
            if (filp) //플립 시
            {
                rend.flipY = true; // y축으로 뒤집기
                gameObject.transform.position = new Vector3(flipPosition.x, flipPosition.y, 0); // 플립 포지션으로 조정
                
                filp = !filp; // 되돌리기
            }

            else
            {
                rend.flipY = false;
                gameObject.transform.position = new Vector3(Position.x, Position.y, 0); // 원래포지션 셋
                filp = !filp;
            }
        }

        cointext.text = "Coin: " + CoinFunction(); //코인 값 보여주기
        PlayerPrefs.SetInt("coin", CoinFunction()); // 스트링 코인에 코인값 저장
    }
    public void CoinFunction(int value) // 코인을 값만큼 더함, 오버로딩
    {
        coin += value;
    }

    public int CoinFunction() // 코인값을 리턴, 오버로딩
    {
        return coin;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Thorn") // 장애물과 부딪히면
        {
            PlayerPrefs.SetInt("score", (int)Mathf.Round(score));
            SceneManager.LoadScene("GameOver");//  게임오버로 돌아감
        }

        if (other.gameObject.tag == "Coin") // 코인과 부딪히면
        {
            SoundManager.Inst.EffectSound(effectSound[1]);
            Destroy(other.gameObject);//코인을 없앰
            CoinFunction(10); // 코인 얻음
        }
    }
}
