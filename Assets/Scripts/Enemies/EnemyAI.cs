using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // ���������� ���������� ��� ������ � NavMeshAgent
using MysticShot.Utils; // ���������� ���������������� ��������� ����� ��� ��������� ��������� �����������

public class EnemyAI : MonoBehaviour
{
    // ���������� ��������� ��� ��������� "�����"
    [SerializeField] private float attackDistance = 5f; // ���������, �� ������� ���� �������� ��������� ������

    private NavMeshAgent navMeshAgent; // ��������� NavMeshAgent ��� ��������������� ������������ �� �����
    private Transform playerTransform; // ������ �� ������������� ������

    // ����� Awake ���������� ��� ������������� �������
    private void Awake()
    {
        // �������� ��������� NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        // ������� ������ ������ �� ���� (���������, ��� � ������ ���������� ������ ���)
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // ����� Update ���������� ������ ����
    private void Update()
    {
        // ��������� ���������� �� ������
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // ���� ����� �����, ��� attackDistance, �������
        if (distanceToPlayer < attackDistance)
        {
            // ������������ � ������
            // ������������ � ������
            navMeshAgent.SetDestination(playerTransform.position);
            ChangeFacingDirection(transform.position, playerTransform.position); // ������������ ����� � ������
        }
    }

    // ����� ��� ��������� �����������, � ������� "�������" ����
    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.x > targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0); // ������������ ����� �����
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // ������������ ����� ������
        }
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using MysticShot.Utils;

//public class EnemyAI : MonoBehaviour
//{
//    //��������� ��� �������
//    [SerializeField] private State startingState;
//    [SerializeField] private float roamingDistanceMax = 10f;
//    [SerializeField] private float roamingDistanceMin = 3f;
//    [SerializeField] private float roamingTimerMax = 2f;

//    private NavMeshAgent navMeshAgent;
//    private State state;
//    private float roamingTime;
//    private Vector3 roamPosition;
//    private Vector3 startingPosition;

//    private enum State 
//    {
//        //Idle, ���� ��� �� �����, ����� ����� ���������� 
//        Roaming
//    }

//    //private void Start()
//    //{
//    //    startingPosition = transform.position; ������ �� �����, ��� ������ ������ � ��������� �������, ��������� ��������� �������
//    //}

//    private void Awake()
//    {
//        navMeshAgent = GetComponent<NavMeshAgent>();
//        navMeshAgent.updateRotation = false;
//        navMeshAgent.updateUpAxis = false;
//        state = startingState;
//    }

//    //��������� � ����� ��������� ��������� ������
//    private void Update() 
//    {
//        switch (state) 
//        {
//            //���� ��������� � ��������� �������, �� ��������� ������
//            default:
//            //case State.Idle: ���� ��� �� �����, ����� ����� ����������
//            //    break;
//            case State.Roaming:
//                roamingTime -= Time.deltaTime; //��������� ��������� ����� �������
//                if (roamingTime < 0) //��� ������ ��� ���������� ������ ����, �� ���� ����� �����
//                {
//                    Roaming();
//                    roamingTime = roamingTimerMax;
//                }
//                break;
//        }
//    }

//    private void Roaming() 
//    {
//        startingPosition = transform.position; //��� ����� ��������� ��������� �������, ����� �� ������� ������ ����� ������ �����
//        roamPosition = GetRoamingPosition(); //��� ������ ��� ���������� ������ ����, �� ���� ����� �����
//        ChangeFacingDirection(startingPosition, roamPosition);
//        navMeshAgent.SetDestination(roamPosition); //� ���������� ������ � ���� �����
//    }

//    private Vector3 GetRoamingPosition() 
//    {
//        //����� ����� = ������ ��������� + ����������� �������� * ����� ��������
//        return startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
//    }


//    //������������ �� ������ ������ �����, � ������ ��� �����
//    //��������� ������� ����� � ����� ���� �� ��������
//    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition) 
//    {
//        if (sourcePosition.x > targetPosition.x) //���� � ������(������), ��� ������� � ������� �������� ����
//        {
//            transform.rotation = Quaternion.Euler(0, -180, 0); //�� ������������ ��� ���� ����
//        }
//        else
//        {
//            transform.rotation = Quaternion.Euler(0, 0, 0);
//        }
//    }
//}
