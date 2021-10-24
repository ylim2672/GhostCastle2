using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObstacleController : MonoBehaviour
{
    float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0); // 오브젝트들이 왼쪽으로 움직임

        if (transform.position.x < -10) // 일정구간이 지나면 사라짐
        {
            if (gameObject != null)
                Destroy(gameObject);
        }
    }
}
