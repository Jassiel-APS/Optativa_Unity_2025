using System;
using UnityEngine;

public class CubeStateMachine : MonoBehaviour, IStateMachine
{
    public IState CurrentState { get; set; }

    private void Start() => ChangeState(new IdleState(this));

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }
    
    void Update() => CurrentState?.Tick(Time.deltaTime);
}

public struct IdleState : IState
{
    public CubeStateMachine StateMachine { get; set; }

    public IdleState(CubeStateMachine stateMachine) => StateMachine = stateMachine;

    public void Enter() => Debug.Log("Enter Idle State");

    public void Tick(float deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.Space)) StateMachine.ChangeState(new RotatingState(StateMachine));
        if (Input.GetKeyDown(KeyCode.M)) StateMachine.ChangeState(new MovingState(StateMachine));
        if (Input.GetKeyDown(KeyCode.D)) StateMachine.ChangeState(new DuplicateState(StateMachine));
    }

    public void Exit() => Debug.Log("Exit Idle State");
}

public struct RotatingState : IState
{
    public CubeStateMachine StateMachine { get; set; }

    public RotatingState(CubeStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public void Enter() => Debug.Log("Enter Rotating State");

    public void Tick(float deltaTime)
    {
        StateMachine.transform.Rotate(0f, 360f * deltaTime, 0f);
        if (Input.GetKeyDown(KeyCode.Space)) StateMachine.ChangeState(new IdleState(StateMachine));
    }

    public void Exit() => Debug.Log("Exit Rotating State");
}

public struct MovingState : IState
{
    public CubeStateMachine StateMachine { get; set; }

    public MovingState(CubeStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public void Enter() => Debug.Log("Enter Moving State");

    public void Tick(float deltaTime)
    {
        // Mover el cubo a la izquierda 1 unidad por segundo
        StateMachine.transform.Translate(-1f * deltaTime, 0f, 0f);
        
        // Regresar al IdleState al presionar M
        if (Input.GetKeyDown(KeyCode.M)) StateMachine.ChangeState(new IdleState(StateMachine));
    }

    public void Exit() => Debug.Log("Exit Moving State");
}

public struct DuplicateState : IState
{
    public CubeStateMachine StateMachine { get; set; }
    private GameObject duplicatedCube;

    public DuplicateState(CubeStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        duplicatedCube = null;
    }

    public void Enter() 
    {
        Debug.Log("Enter Duplicate State");
        // Duplicar el cubo con GameObject.Instantiate
        duplicatedCube = GameObject.Instantiate(StateMachine.gameObject);
        // Posicionar la copia un poco a la derecha para que se vea
        duplicatedCube.transform.position = StateMachine.transform.position + Vector3.right * 2f;
    }

    public void Tick(float deltaTime)
    {
        // Salir del estado al presionar D
        if (Input.GetKeyDown(KeyCode.D)) StateMachine.ChangeState(new IdleState(StateMachine));
    }

    public void Exit() 
    {
        Debug.Log("Exit Duplicate State");
        // Destruir la copia al salir del estado
        if (duplicatedCube != null) 
        {
            GameObject.Destroy(duplicatedCube);
            duplicatedCube = null;
        }
    }
}

/* Ejercicio 1 - COMPLETADO */
/* 1.-Crear un nuevo estado MovingState
2.- Implementar la interfaz IState
3.- Mover el cubo a la izquierda  1 unidad por segundo
4,. Para cambiar del IdleState al MovingState presionar la tecla M
5.- Para regresar del MovingState al IdleState presionar la tecla M 
*/

/* Ejercicio 2 - COMPLETADO */
/* DuplicateState implementado con tecla D */