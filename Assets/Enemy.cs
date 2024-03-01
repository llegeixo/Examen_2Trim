using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    enum State
    {
        Patrolling,
        Chasing,
        Attacking
    }

    private State _currentState;
    private NavMeshAgent _agent;
    
    private Transform _player;
    [SerializeField] Transform[] _patrolBases;
    [SerializeField] float _detectionRange = 15;
    [SerializeField] float _attackingRange = 5;
    
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        SetRandomPoint();
        _currentState = State.Patrolling;
    }

    
    void Update()
    {
        switch (_currentState)
        {
            case State.Patrolling:
                Patrol();
            break;
            case State.Chasing:
                Chase();
            break;
            case State.Attacking:
                Attack();
            break;
        }

    }

    void Patrol()
    {
        if(IsInRange(_detectionRange) == true)
        {
            _currentState = State.Chasing;
        }

        if(_agent.remainingDistance < 2f)
        {
            SetRandomPoint();
        }
    }

    void Chase()
    {
        if(IsInRange(_detectionRange) == false)
        {
            _currentState = State.Patrolling;
        }

        if(IsInRange(_attackingRange) == true)
        {
            _currentState = State.Attacking;
        }

        _agent.destination = _player.position;
    }

    void Attack()
    {
        Debug.Log("Ataque");
        _currentState = State.Chasing;
    }

    void SetRandomPoint()
    {
        _agent.destination = _patrolBases[Random.Range(0, _patrolBases.Length)].position;

    }

    bool IsInRange(float range)
    {
        if(Vector3.Distance(transform.position, _player.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
