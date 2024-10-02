using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MysticShot.Utils;

public class EnemyAI : MonoBehaviour
{
    //настройки для бродить
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 2f;

    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startingPosition;

    private enum State 
    {
        Idle,
        Roaming
    }

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = startingState;
    }

    //проверяет в каком состоянии находится объект
    private void Update() 
    {
        switch (state) 
        {
            //если находится в состоянии бродить, то выполняет логику
            default:
            case State.Idle:
                break;
            case State.Roaming:
                roamingTime -= Time.deltaTime; //постоянно уменьшает время бродить
                if (roamingTime < 0) //как только оно становится меньше нуля, то ищет новую точку
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }
                break;
        }
    }

    private void Roaming() 
    {
        roamPosition = GetRoamingPosition(); //как только оно становится меньше нуля, то ищет новую точку
        navMeshAgent.SetDestination(roamPosition); //и отправляет агента к этой точке
    }

    private Vector3 GetRoamingPosition() 
    {
        //новая точка = старое положение + направление движения * длина движения
        return startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }
}
