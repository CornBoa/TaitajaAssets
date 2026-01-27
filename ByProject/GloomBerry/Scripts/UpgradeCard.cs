using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
    public enum UpgradeType
    {
        Speed,
        Firerate,
        DMG,
        HP,
        Pellets,
        Range
    }
    public float maxUp, minUp;
    public TextMeshProUGUI description;
    public GameObject panel;
    bool hasDownSide;
    UpgradeType type, badType;
    float upValue;
    public void Selected()
    {
        Time.timeScale = 1f;
        switch (type)
        {
            case UpgradeType.Speed:
                FindFirstObjectByType<PlayerMovement>().speed *= upValue;
                break;
            case UpgradeType.Firerate:
                FindFirstObjectByType<Shooting>().shootCooldown *= upValue;
                break;
            case UpgradeType.DMG:
                FindFirstObjectByType<Shooting>().DMG *= (int)upValue;
                break;
            case UpgradeType.HP:
                FindFirstObjectByType<PlayerMovement>().maxHP *= (int)upValue ;
                break;
            case UpgradeType.Pellets:
                FindFirstObjectByType<Shooting>().bulletAmount *= (int)upValue;
                break;
            case UpgradeType.Range:
                FindFirstObjectByType<Shooting>().maxEnemyDist *= upValue;
                break;
        }
        panel.SetActive(false);
    }
    private void OnEnable()
    {
        ResetCard();
    }
    public void ResetCard()
    {
        hasDownSide = Random.Range(0, 100f) > 80f;
        upValue = Random.Range(minUp, maxUp);
        if(hasDownSide)
        {
            upValue *= 1.20f;
            badType = (UpgradeType)Random.Range(0, System.Enum.GetValues(typeof(UpgradeType)).Length);
        }
        else
        {
            badType = UpgradeType.Speed;
        }
        type = (UpgradeType)Random.Range(0, System.Enum.GetValues(typeof(UpgradeType)).Length);
        if(hasDownSide)description.text = "Upgrade " + type.ToString() + " by " + upValue.ToString("0.0") + (hasDownSide ? "\nDecrease " + type.ToString() + " by " + (upValue / 3).ToString("0.0") : "");
        else description.text = "Upgrade " + type.ToString() + " by " + upValue.ToString("0.0");
    }
}
