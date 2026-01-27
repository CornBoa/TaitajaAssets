using UnityEngine;

public class Ability : MonoBehaviour
{
    public AbilityManager.AbilityType abilityType;

    private void OnDestroy()
    {
        FindFirstObjectByType<AbilityManager>().SetAbility(abilityType);
    }
}
