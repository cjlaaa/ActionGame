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
    public Transform target;
    public Transform uiHp;

    private float m_speed = 1f;
    private string m_statu = "idle";
    private float hp = 200;
    private float MAX_HP = 200;
    private int atk = 10;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation.state.Complete += delegate
        {
            if (m_statu == "parried")
            {
                m_statu = "idle";
            }
            // if (m_statu != "idle" && m_statu != "walk")
            // {
            //     m_statu = "idle";
            // }
        };
        skeletonAnimation.state.SetAnimation(0, "idle", true);
    }

    public void Hit(int damage)
    {
        hp -= damage;
        m_statu = "parried";
        skeletonAnimation.state.SetAnimation(0, m_statu, false);

        uiHp.SetLocalScaleX(hp / MAX_HP * 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_statu == "parried")
        {
            return;
        }

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

        if (verticalMove == 0 && horizontalMove == 0)
        {
            curStatu = "attack";
        }
        else
        {
            this.transform.Translate((new Vector3(horizontalMove, verticalMove, 0)) * m_speed * Time.deltaTime);
        }

        // 根据当前状态处理动作
        if (m_statu != curStatu)
        {
            m_statu = curStatu;
            skeletonAnimation.state.SetAnimation(0, m_statu, true);
        }
    }
}