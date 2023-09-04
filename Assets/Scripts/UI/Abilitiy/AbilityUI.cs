using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private Image _skillRechargeImage;
    [SerializeField] private AbilityBase _ability;

    private void Start()
    {
        _ability.OnUseAbility += RechardSkill;
    }

    private void RechardSkill(float duration)
    {
        _skillRechargeImage.DOFillAmount(0, duration).From();
    }
}
