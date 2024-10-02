using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MysticShot.Utils;

public class EnemyAI : MonoBehaviour
{
    //настройки для бродить
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 10f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 2f;

    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startingPosition;

    private enum State 
    {
        //Idle, пока что не нужен, может потом пригодится 
        Roaming
    }

    //private void Start()
    //{
    //    startingPosition = transform.position; больше не нужна, как только входит в состояние бродяги, обновляет стартовую позицию
    //}

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
            //case State.Idle: пока что не нужен, может потом пригодится
            //    break;
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
        startingPosition = transform.position; //эта хуйня обновляет стартовую позицию, чтобы не бродить вокруг самой первой точки
        roamPosition = GetRoamingPosition(); //как только оно становится меньше нуля, то ищет новую точку
        ChangeFacingDirection(startingPosition, roamPosition);
        navMeshAgent.SetDestination(roamPosition); //и отправляет агента к этой точке
    }

    private Vector3 GetRoamingPosition() 
    {
        //новая точка = старое положение + направление движения * длина движения
        return startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }


    //поворачивает не только спрайт врага, а вообще его всего
    //принимает позицию врага и точку куда он движется
    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition) 
    {
        if (sourcePosition.x > targetPosition.x) //если х правее(больше), чем позиция в которую движется враг
        {
            transform.rotation = Quaternion.Euler(0, -180, 0); //то поворачиваем ему ебло сука
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
