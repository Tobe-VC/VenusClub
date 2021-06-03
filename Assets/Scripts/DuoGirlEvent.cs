using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class DuoGirlEvent : IEvent
{

    public string girl1;

    public string girl2;

    public VideoClip clip;

    public UnityAction action;

    private Func<bool> extraTrigger;

    //The minimal openness that girl 1 must have to be able tol trigger this event
    private float minOpennessGirl1;    
    
    //The minimal openness that girl 2 must have to be able tol trigger this event
    private float minOpennessGirl2;

    //public delegate void SpecialEffect();

    public DuoGirlEvent(string girl1, string girl2, string title, string description, VideoClip clip, UnityAction action,
        float minOpennessGirl1, float minOpennessGirl2)
    {
        this.girl1 = girl1;
        this.girl2 = girl2;
        this.title = title;
        this.description = description;
        this.clip = clip;
        this.action = action;
        this.minOpennessGirl1 = minOpennessGirl1;
        this.minOpennessGirl2 = minOpennessGirl2;

        extraTrigger = delegate () { return true; };
    }

    public DuoGirlEvent(string girl1, string girl2, string title, string description, VideoClip clip, UnityAction action,
        float minOpennessGirl1, float minOpennessGirl2, Func<bool> extraTrigger)
    {
        this.girl1 = girl1;
        this.girl2 = girl2;
        this.title = title;
        this.description = description;
        this.clip = clip;
        this.action = action;
        this.minOpennessGirl1 = minOpennessGirl1;
        this.minOpennessGirl2 = minOpennessGirl2;

        this.extraTrigger = extraTrigger;
    }

    public override void Effect(GameObject eventPopup, Text eventPopupTitle, Text eventPopupDescription,
        VideoPlayer eventPopupVideoPlayer)
    {
            eventPopup.SetActive(true);
            eventPopupTitle.text = title;
            eventPopupDescription.text = description;
            eventPopupVideoPlayer.clip = clip;
            eventPopupVideoPlayer.Prepare();
            eventPopupVideoPlayer.Play();
            action();
        
    }

    //This event is possible if both girls are in the recruited girls list
    public override bool Trigger()
    {
        GirlClass g1 = GMRecruitmentData.recruitedGirlsList.Find(x => x.name.Equals(girl1));
        GirlClass g2 = GMRecruitmentData.recruitedGirlsList.Find(x => x.name.Equals(girl2));
        return g1 != null
            && g2 != null
            && g1.GetOpenness() >= minOpennessGirl1
            && g2.GetOpenness() >= minOpennessGirl2
            && extraTrigger();
    }
}
