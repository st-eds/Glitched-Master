using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
        Vector2 movement = new Vector2();

        movement.x = stateMachine.InputReader.MovementValue.x;
        movement.y = stateMachine.InputReader.MovementValue.y;
        

        stateMachine.Controller.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero) { return; }

        
    }

    public override void Exit()
    {

    }
}
