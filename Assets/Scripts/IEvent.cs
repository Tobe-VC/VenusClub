using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public abstract class IEvent
{

    protected string title;

    protected string description;

    public abstract bool Trigger();

    public abstract void Effect(GameObject eventPopup, Text eventPopupTitle, Text eventPopupDescription,
        VideoPlayer eventPopupVideoPlayer);


}
