﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeCroCloneCharge : MonoBehaviour
{
    private GameObject Necromant;
    private GameObject Player;
    public GameObject Clone;
    private Rigidbody2D rigidbodyCharge;
    public float angle = 0; // угол 
    private float radius = 1f; // радиус
    private bool isCircle = true; // условие движения по кругу
    private Vector3 _PlayerPositon;

    void Start()
    {
        rigidbodyCharge = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("carPlayer");
        Necromant = GameObject.Find("NecroBos");
        Invoke("shootAtPlayer", 5f);
    }

    void Update()
    {
        MoveCharge();
        if (NeCroBos._isCanDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void MoveCharge()
    {
        if (isCircle)
        {
            angle += Time.deltaTime; // меняется плавно значение угла
            var turn = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, Player.transform.position - transform.position), Time.deltaTime * 500f);
            rigidbodyCharge.MoveRotation(turn.eulerAngles.z);
            var x = Mathf.Cos(angle * 4f) * radius;
            var y = Mathf.Sin(angle * 4f) * radius-0.5f;
            transform.position = new Vector2(x, y) + new Vector2(Necromant.transform.position.x, Necromant.transform.position.y);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _PlayerPositon, GameProperties.SpeedEnemy * Time.deltaTime);
            if (transform.position == _PlayerPositon)
            {
                Teleport();
            }
        }
    }

    private void shootAtPlayer()
    {
        _PlayerPositon = Player.transform.position;
        isCircle = false;

    }

    private void Teleport()
    {
        Instantiate(Clone, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(gameObject);
    }
}
