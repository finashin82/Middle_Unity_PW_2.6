using Unity.Entities;
using UnityEngine;

public class CharacterBurstSystem : ComponentSystem
{
    private EntityQuery _burstQuery;

    protected override void OnCreate()
    {
        _burstQuery = GetEntityQuery(ComponentType.ReadOnly<BurstData>(), ComponentType.ReadOnly<UserInputData>(), ComponentType.ReadOnly<InputData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_burstQuery).ForEach(
            (Entity entity, UserInputData input) =>
            {
                if (input.BurstAction != null && input.BurstAction is ISkill skill)
                {
                    skill.ExecuteBurst();                    
                }                
            });
    }
}
