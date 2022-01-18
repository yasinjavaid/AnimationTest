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
    public AvatarMaskBodyPart BodyPart;
}
public class AnimationsControl : MonoBehaviour
{
    private readonly string cheeringTrigger = "Cheering";
    private readonly string clappingTrigger = "Clapping";

    private int copyBodypart = -1;
    
    [SerializeField] private Button[] gameplayButtons;
    [SerializeField] private Avatar avatar;
  

    private Animator animator;

    #region mono members

    private void Awake()
    {
       gameplayButtons[0].onClick.AddListener(OnCheerButtonPress); 
       gameplayButtons[1].onClick.AddListener(OnClampButtonPress);
       animator =  this.gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        UpdateAvatar();
    }

    #endregion
   

    #region private members

    private void OnCheerButtonPress()
    {
        animator.SetTrigger(cheeringTrigger);
    }

    private void OnClampButtonPress()
    {
        animator.SetTrigger(clappingTrigger);

    }

    private void UpdateAvatar()
    {
       
        var temp = (int) avatar.BodyPart;
        var on = copyBodypart == temp;
        if (copyBodypart != -1)
        {
            avatar.avatarMask.SetHumanoidBodyPartActive(on ? 
                    avatar.BodyPart :
                    (AvatarMaskBodyPart)copyBodypart
                , on);
        }
        copyBodypart = temp;
    }

    #endregion
}
