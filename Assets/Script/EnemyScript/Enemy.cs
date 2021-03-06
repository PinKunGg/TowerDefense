﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float f_enemySpeed = 3f;
    public static float MaxHp = 20f;
    public static float BossHp = 1100;
    public bool isBoss;
    public Slider EnemyHp;

    public float Piority;

    Transform trans_target; //Waypoint to go
    int i_waypointIndex = 0;
    static float f_index = 0f; //idex of Enemy Prefab (when Debug)
    void Start() //Set WayPoint to 0
    {
        if(isBoss == false)
        {
            EnemyHp.maxValue = MaxHp;
            EnemyHp.value = MaxHp;
            print("Ground Hp = " +EnemyHp.value);
        }
        else
        {
            EnemyHp.maxValue = BossHp;
            EnemyHp.value = BossHp;
            print("Ground Hp = " +EnemyHp.value);
        }
        trans_target = Waypoint.point[0];
    }

    private void OnEnable() //Make Enemy Add to GameMode_Controller List to Check if it on Sceen yet?
    {
        f_index++;
        GameMode_Controller.L_EnemyPrefabSpawn.Add(this.gameObject);
    }

    private void OnDestroy() //Remove themself in GameMode_Controller List when Destroy
    {
        GameMode_Controller.L_EnemyPrefabSpawn.Remove(this.gameObject);
    }

    void Update()
    {
        Vector3 dir = trans_target.position - transform.position; //Make Path from themself to current WayPoint
        this.transform.LookAt(trans_target);

        transform.Translate(dir.normalized * f_enemySpeed * Time.deltaTime, Space.World); //Make Enemy Walk

        if(Vector3.Distance(transform.position, trans_target.position) <= 0.2f) //Check distance between themself and current WayPoint
        {
            GetNextWayPoint();
        }

        if(EnemyHp.value <= 0) //when Hp = 0 Destroy
        {
            Destroy(gameObject);
        }
    }

    void GetNextWayPoint()
    {
        if(i_waypointIndex >= Waypoint.point.Length - 1) //Check if there are any WayPoint left to go, if not Destroy themself
        {
            //Destroy(this.gameObject);
            return;
        }

        i_waypointIndex++;
        trans_target = Waypoint.point[i_waypointIndex];
    }
    public void HpCal(float value)
    {
        EnemyHp.value -= value;
    }
}
