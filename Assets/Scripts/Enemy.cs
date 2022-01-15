using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Spine.Unity;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform enemy;
    public SkeletonAnimation skeletonAnimation;
    private float m_speed = 1f;
    public Transform target;

    private string m_statu = "idle";

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation.state.SetAnimation(0, "idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        float verticalMove = 0;
        float horizontalMove = 0;
        string curStatu = "idle";

        Vector3 distance = target.transform.position - this.transform.position;
        if (Math.Abs(distance.x) > 1)
        {
            curStatu = "walk";
            if (distance.x < 0)
            {
                horizontalMove = -2;
                enemy.transform.SetLocalScaleX(-0.01f);
            }
            else
            {
                horizontalMove = 2;
                enemy.transform.SetLocalScaleX(0.01f);
            }
        }

        if (Math.Abs(distance.y) > 0.5)
        {
            curStatu = "walk";
            if (distance.y < 0) verticalMove = -1;
            else verticalMove = 1;
        }

        if (verticalMove == 0 && horizontalMove == 0 && m_statu == "walk")
        {
            curStatu = "idle";
        }
        else
        {
            this.transform.Translate((new Vector3(horizontalMove, verticalMove, 0)) * m_speed * Time.deltaTime);
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