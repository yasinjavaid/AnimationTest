using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

[Serializable]
struct Avatar
{
    public AvatarMask avatarMask;
    public AvatarMaskBodyPart playOtheranimationBodyPart;
}
public class AnimationsControl : MonoBehaviour
{
    private readonly string cheeringTrigger = "Cheering";
    private readonly string clappingTrigger = "Clapping";

    
    private int copyBodypart = -1;
    
    [SerializeField] private Button[] animationsButtons;
    [SerializeField] private Button[] ikBodyControlButtons;
    [SerializeField] private AvatarIKGoal ikPart;
    [SerializeField] private Avatar avatar;
    [SerializeField] private GameObject targetObject;
    
    private enum IKParts
    {
        LeftHand,
        RightHand,
        LeftFoot,
        RightFoot
    }


    private IKParts ikPartenum;
    private Animator animator;
    private bool isik = false;

    #region mono members

    private void Awake()
    {
        AddUIButtonsListeners();
       animator =  this.gameObject.GetComponent<Animator>();
     //  animator.SetIKPosition(av);
    }

    void Update()
    {
        UpdateAvatar();
        if (isik) SetBonePos(GetIKGoalFromEnum(ikPartenum));
    }

    #endregion
   

    #region private members

    private void AddUIButtonsListeners()
    {
        animationsButtons[0].onClick.AddListener(OnCheerButtonPress); 
        animationsButtons[1].onClick.AddListener(OnClampButtonPress);
        ikBodyControlButtons[0].onClick.AddListener(LeftHand);
        ikBodyControlButtons[1].onClick.AddListener(RightHand);
        ikBodyControlButtons[2].onClick.AddListener(LeftFoot);
        ikBodyControlButtons[3].onClick.AddListener(RightFoot);
    }

    private void OnCheerButtonPress()
    {
        animator.SetTrigger(cheeringTrigger);
    }

    private void OnClampButtonPress()
    {
        animator.SetTrigger(clappingTrigger);

    }

    private void LeftHand()
    {
        ikPartenum = IKParts.LeftHand;
        isik = true;
    }

    private void RightHand()
    {
        ikPartenum = IKParts.RightHand;
        isik = true;
    }

    private void LeftFoot()
    {
        ikPartenum = IKParts.LeftFoot;
        isik = true;
    }

    private void RightFoot()
    {
        ikPartenum = IKParts.RightFoot;
        isik = true;
    }

    private void UpdateAvatar()
    {
       
        var temp = (int) avatar.playOtheranimationBodyPart;
        var on = copyBodypart == temp;
        if (copyBodypart != -1)
        {
            avatar.avatarMask.SetHumanoidBodyPartActive(on ? 
                    avatar.playOtheranimationBodyPart :
                    (AvatarMaskBodyPart)copyBodypart
                , on);
        }
        copyBodypart = temp;
    }

    private AvatarIKGoal GetIKGoalFromEnum(IKParts parts)
    {
        AvatarIKGoal goal;
        switch (parts)
        {
            case IKParts.LeftHand:
                goal = AvatarIKGoal.LeftFoot;
                break;
            case IKParts.RightHand:
                goal = AvatarIKGoal.LeftFoot;
                break;
            case IKParts.LeftFoot:
                goal = AvatarIKGoal.LeftFoot;
                break;
            case IKParts.RightFoot:
                goal = AvatarIKGoal.LeftFoot;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(parts), parts, null);
        }

        return goal;
    }

    private void SetBonePos(AvatarIKGoal goalBone)
    {
        SetIKWeightZero();
        animator.SetIKPositionWeight(goalBone, 1);
        animator.SetIKPosition(goalBone, targetObject.transform.position);
    }
    public void SetIKWeightZero()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);

    }

    #endregion
}
