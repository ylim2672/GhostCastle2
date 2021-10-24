using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Inst; // 싱글톤

    AudioSource myaudio; // 내 오디오소스

    // Start is called before the first frame update
    void Start()
    {
        if (Inst == null) //인스턴스가 널이면
        {
            Inst = this;//자기자신 넣어줌
        }

        myaudio = gameObject.AddComponent<AudioSource>(); //오디오소스불러옴
    }

    public void EffectSound(AudioClip clip) // 효과음 넣기 함수
    {
        myaudio.PlayOneShot(clip); // 클립 한번만 재생
    }
}
