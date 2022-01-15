using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform player;
    public SkeletonAnimation skeletonAnimation;
    private float m_speed = 3f;

    private string m_statu = "idle";

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation.state.End += delegate
        {
            if (m_statu != "idle" && m_statu != "walk")
            {
                m_statu = "idle";
            }
        };
        skeletonAnimation.state.SetAnimation(0, m_statu, true);
    }

    // Update is called once per frame
    void Update()
    {
        MoveControlByTranslate();
    }

    void MoveControlByTranslate()
    {
        if (m_statu == "attack")
        {
            return;
        }

        float verticalMove = 0;
        float horizontalMove = 0;

        string curStatu = "idle";
        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow)) //前
        {
            verticalMove += 1;
            curStatu = "walk";
        }

        if (Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow)) //后
        {
            verticalMove -= 1;
            curStatu = "walk";
        }

        if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)) //左
        {
            horizontalMove -= 2;
            player.transform.SetLocalScaleX(-0.01f);
            curStatu = "walk";
        }

        if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow)) //右
        {
            horizontalMove += 2;
            player.transform.SetLocalScaleX(0.01f);
            curStatu = "walk";
        }

        if (verticalMove == 0 && horizontalMove == 0 && m_statu == "walk")
        {
            curStatu = "idle";
        }
        else
        {
            this.transform.Translate((new Vector3(horizontalMove, verticalMove, 0)) * m_speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.J) && m_statu != "attack")
        {
            curStatu = "attack";
        }

        // 根据当前状态处理动作
        if (m_statu != curStatu)
        {
            m_statu = curStatu;
            if (m_statu == "attack")
            {
                skeletonAnimation.state.SetAnimation(0, m_statu, false);
            }
            else
            {
                skeletonAnimation.state.SetAnimation(0, m_statu, true);
            }
        }
    }
}