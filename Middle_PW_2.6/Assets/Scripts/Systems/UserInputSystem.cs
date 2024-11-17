using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInputSystem : ComponentSystem
{
    // ���������� (������ �� ��������)
    private EntityQuery _inputQuery;

    // ���������� (��� ����)
    private InputAction _moveAction;

    private InputAction _shootAction;

    private float2 _moveInput;

    private float _shootInput;

    /// <summary>
    /// �������� ������� (�������� ������: ���� ������� ���� ��� ������ � ������� ���� ��������� UserInputData (��� ������, ������� ����� Input)
    /// </summary>
    protected override void OnCreate()
    {        
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
    }

    /// <summary>
    /// ������ ������� (������� ����� InputAction)
    /// </summary>
    protected override void OnStartRunning()
    {
        _moveAction = new InputAction("move", binding: "<Gamepad>/rightStick");

        _moveAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        // ��������� ��� ��������� ��������� ���������� �����
        _moveAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };

        _moveAction.started += context => { _moveInput = context.ReadValue<Vector2>(); };

        _moveAction.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };

        _moveAction.Enable();

        // ��������
        _shootAction = new InputAction("shoot", binding: "<Keyboard>/Space");

        _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };

        _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };

        _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };

        _shootAction.Enable();
    }

    // ����������� ����� ����������� �������
    protected override void OnStopRunning()
    {
        _moveAction.Disable();

        _shootAction.Disable();
    }

    protected override void OnUpdate() 
    {
        Entities.With(_inputQuery).ForEach(
            (Entity entity, ref InputData inputData) => 
            { 
                inputData.Move = _moveInput; 

                inputData.Shoot = _shootInput;
            });
    }
}
