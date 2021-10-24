using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour, Menu
{
    public void ChangeTheScene(string Name)//파라미터로 씬 이름을 받아 이동 
    {
        SceneManager.LoadScene(Name); // Name으로 씬을 이동
    }

    public void Exit()// 게임종료
    {
        Application.Quit();
    }

    public void OnOffMenu(GameObject menu) // 메뉴 온오프
    {
        if(menu.activeSelf) // 메뉴창이 켜져있다면
        {
            menu.SetActive(false); // 끄고
        }

        else // 아니면
        {
            menu.SetActive(true); // 메뉴창을 킨다.
        }
    }
}
