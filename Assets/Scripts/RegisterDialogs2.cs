using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// This class is only used to avoid having the first one being completely unreadable
/// I should really rework that into something better...
/// </summary>
public class RegisterDialogs2 : RegisterDialogs
{

    public new static void CreateDialogs()
    {
        Message begin;
        Message beginBis;
        Message b2;
        Message b3;
        Message b4;
        Message b5;
        Message b6;
        Message b7;
        Message b8;
        Message b9;
        Message b10;
        Message b11;
        Message b12;
        Message b13;
        Message b14;
        Message b15;
        //Message b16;
        Message b17;
        Message b18;
        Message b19;
        Message b20;
        Message b21;
        Message b22;
        Message b23;
        Message b24;
        Message b25;
        Message b26;
        Message b27;
        Message b28;
        Message b29;
        Message b30;
        Message b31;
        Message b32;
        Message end;
        /******************************************************Dialog to unlock handjobs for Nicole*************************************************************/
        {
            begin = new MessageBasic("OK, we're finally there. You don't need much more. You should be able to convince her to start \"handling\" clients... Got it? No? Come on, it wasn't that bad... ", NPCCode.NARRATOR);

            beginBis = new MessageBasicWithAction("Ok, I'll stop now. Go do your thing.", NPCCode.NARRATOR,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false);

                });
            b2 = new MessageBasic("<i>Ni hao</i>!", NPCCode.MAIN_CHARACTER);

            b3 = new MessageBasic("No.", NPCCode.ASSISTANT);

            b4 = new MessageBasic("What?", NPCCode.MAIN_CHARACTER);

            b6 = new MessageBasic("Based on previous experiences, you only speak to me in foreign languages when you want me to do something new in the booth. And, observing what the other girls do after using toys, I deduce that you are here to ask me to give handjobs. And the answer is no.", NPCCode.ASSISTANT);

            b8 = new MessageBasic("Damn, she's good!", NPCCode.NARRATOR);

            b9 = new MessageBasic("Damn, you're good!", NPCCode.MAIN_CHARACTER);

            b10 = new MessageBasic("I know. This won't make me change my mind though.", NPCCode.ASSISTANT);

            b11 = new MessageBasic("I'm sure of that. But, I don't see why you would refuse to give handjobs? It's not that complicated.", NPCCode.MAIN_CHARACTER);

            b12 = new MessageBasic("Talking from experience?", NPCCode.ASSISTANT);

            b5 = new MessageBasic("I mean, I've given plenty of handjobs. To myself. And I know it's not that complicated.", NPCCode.MAIN_CHARACTER);

            b13 = new MessageBasic("It's not a question of being complex. Moving my hand back and forth isn't exactly hard... No, you know exactly why I don't want to do it.", NPCCode.ASSISTANT);

            b14 = new MessageBasic("Oh, but giving a handjob is not only moving your hand. There is technique, knowledge, mastery even. I'm sure you're not that good anyway...", NPCCode.MAIN_CHARACTER);

            b15 = new MessageBasic("Really? You're trying that strategy? Do you really think I would fall for it? I'm not a 15 years old teenager...", NPCCode.ASSISTANT);

            b17 = new MessageBasic("No! She's not! She's clearly a consenting adult!", NPCCode.NARRATOR);

            b18 = new MessageBasic("That was close. Moving along.", NPCCode.NARRATOR);

            b19 = new MessageBasic("Well, it was worth a try. But really, there is no shame in giving handjobs. I'm sure you would enjoy the feeling of power it gives...", NPCCode.MAIN_CHARACTER);

            b20 = new MessageBasic("Power? What power?", NPCCode.ASSISTANT);

            b21 = new MessageBasic("Well, you're holding a man's junk in your hand. Sometimes, his balls too. You could basically crush him at any time. I mean, we do say \"having someone by the balls\". It's for a reason.", NPCCode.MAIN_CHARACTER);

            b22 = new MessageBasic("Never thought of that.", NPCCode.ASSISTANT);

            b23 = new MessageBasic("It's also a big show of trust from the client too. It takes quite a lot of it to allow someone to do something like that. Basically, if the clients want you to do that, not only does it mean that they desire you, but also that they trust you.", NPCCode.MAIN_CHARACTER);

            b24 = new MessageBasic("...", NPCCode.ASSISTANT);

            b25 = new MessageBasic("And I don't think you're the kind of person that would betray a man's trust. You're a straight up woman, hard working and smart. Despite what I falsely implied before, I know you would be great at handjobs. Plus, I'm sure you'd like it.", NPCCode.MAIN_CHARACTER);

            b26 = new MessageBasic("Damn, you're good...", NPCCode.ASSISTANT);

            b27 = new MessageBasic("I feel like I heard that before...", NPCCode.MAIN_CHARACTER);

            b28 = new MessageBasic("Alright, I'll do it. You and your golden tongue...", NPCCode.ASSISTANT);

            b29 = new MessageBasic("Gotta have something for myself, you know.", NPCCode.MAIN_CHARACTER);

            b30 = new MessageBasic("I believe you have the best assistant a sex club owner could ever dream of... Now, you should leave before I change my mind.", NPCCode.ASSISTANT);

            b31 = new MessageBasicWithAction("Won't deny that! I'll see myself out then. Good day and thank you!", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_DOOR_TO_OFFICE_LATE_AT_NIGHT, true);

                });

            end = new MessageEnd("Are you sure you're not a wizard or something like that?", NPCCode.NARRATOR);

            begin.AddSuccessor(beginBis);
            beginBis.AddSuccessor(b2);
            b2.AddSuccessor(b3);
            b3.AddSuccessor(b4);
            b4.AddSuccessor(b6);
            b6.AddSuccessor(b8);
            b8.AddSuccessor(b9);
            b9.AddSuccessor(b10);
            b10.AddSuccessor(b11);
            b11.AddSuccessor(b12);
            b12.AddSuccessor(b5);
            b5.AddSuccessor(b13);
            b13.AddSuccessor(b14);
            b14.AddSuccessor(b15);
            b15.AddSuccessor(b17);
            b17.AddSuccessor(b18);
            b18.AddSuccessor(b19);
            b19.AddSuccessor(b20);
            b20.AddSuccessor(b21);
            b21.AddSuccessor(b22);
            b22.AddSuccessor(b23);
            b23.AddSuccessor(b24);
            b24.AddSuccessor(b25);
            b25.AddSuccessor(b26);
            b26.AddSuccessor(b27);
            b27.AddSuccessor(b28);
            b28.AddSuccessor(b29);
            b29.AddSuccessor(b30);
            b30.AddSuccessor(b31);
            b31.AddSuccessor(end);

            dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_UNLOCK_HANDJOB_DIALOG_ID, begin,
            delegate ()
            {
                if (StaticDialogElements.dialogData.launchAssistantUnlockHandjobDialog)
                {
                    SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_DOOR_TO_OFFICE_LATE_AT_NIGHT, true);

                    return true;
                }
                return false;
            },
            delegate ()
            {
                StaticDialogElements.specialBackground = "";
                StaticDialogElements.dialogSpecialPortraitIsOn = false;
                StaticDialogElements.specialPortraitSprite = null;
                StaticDialogElements.dialogSpecialBackgroundIsOn = false;
                StaticDialogElements.backgroundImage = null;
            }
            ));
        }
        /*******************************************************************************************************************/

        /*********************************************Dialog to unlock footjob for Nicole******************************************/
        {
            begin = new MessageBasic("Hey, I've got a good one! I believe you can easily get her to use her feet now. Because I can confidently say that from handjob to footjob, there is only one \"step\".", NPCCode.NARRATOR);

            beginBis = new MessageBasicWithAction("Damn, I'm good! Your turn now.", NPCCode.NARRATOR,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false);

                });
            b2 = new MessageBasic("...", NPCCode.MAIN_CHARACTER);

            b3 = new MessageBasic("Hello?", NPCCode.ASSISTANT);

            b4 = new MessageBasic("...", NPCCode.MAIN_CHARACTER);

            b6 = new MessageBasic("Yes?", NPCCode.ASSISTANT);

            b8 = new MessageBasic("...", NPCCode.MAIN_CHARACTER);

            b9 = new MessageBasic("Got something to say, or you're just here to admire me?", NPCCode.ASSISTANT);

            b10 = new MessageBasic("Hello.", NPCCode.MAIN_CHARACTER);

            b11 = new MessageBasic("Oh. That's it?", NPCCode.ASSISTANT);

            b12 = new MessageBasic("Well, I was looking for a greeting in another language, but I couldn't find one.", NPCCode.MAIN_CHARACTER);

            b5 = new MessageBasic("... <i>Konnichiwa</i>? <i>Buenos días</i>? <i>Merhaba</i>? <i>Lumela</i>?", NPCCode.ASSISTANT);

            b7 = new MessageBasic("Dear player, I have no idea if these words are correct... I don't speak those languages, and can't say for sure these greeting are grammatically correct. Therefore, the Venus's Club team declines all responsibility if using those words ends up poorly. You've been warned.", NPCCode.NARRATOR);

            b13 = new MessageBasic("Wow. Impressive.", NPCCode.MAIN_CHARACTER);

            b14 = new MessageBasic("Well, a good assistant can at least speak three languages. And say hello in ten.", NPCCode.ASSISTANT);

            b15 = new MessageBasic("Awesome! Goodbye!", NPCCode.MAIN_CHARACTER);

            b17 = new MessageBasic("Goodbye!", NPCCode.ASSISTANT);

            b18 = new MessageBasic("What?", NPCCode.NARRATOR);

            b19 = new MessageBasic("Wait, is that all?", NPCCode.ASSISTANT);

            b20 = new MessageBasic("Oh right, no. I forgot. Would you be fine giving footjobs to clients?", NPCCode.MAIN_CHARACTER);

            b21 = new MessageBasic("Just like that? No buttering me up?", NPCCode.ASSISTANT);

            b22 = new MessageBasic("I mean, it's not much more than giving handjobs. You'll just have to use your feet instead. It's not that big of a \"step\".", NPCCode.MAIN_CHARACTER);

            b23 = new MessageBasic("Pun thief!", NPCCode.NARRATOR);

            b24 = new MessageBasic("OK.", NPCCode.ASSISTANT);

            b25 = new MessageBasic("Just like that? No buttering you up needed?", NPCCode.MAIN_CHARACTER);

            b26 = new MessageBasic("No. As you said, it's not that different from handjobs anyway. It shouldn't be too hard to find my feet in this new activity.", NPCCode.ASSISTANT);

            b27 = new MessageBasic("Okay, I'm leaving this office. Too much foot puns. It's quickly becoming my \"sole\" problem.", NPCCode.MAIN_CHARACTER);

            b28 = new MessageBasic("Come on, I'm just trying to keep you on your toes around me!", NPCCode.ASSISTANT);

            b29 = new MessageBasic("Yep, I'm out. Goodbye! For real this time!", NPCCode.MAIN_CHARACTER);

            end = new MessageEnd("Bye!", NPCCode.ASSISTANT);

            begin.AddSuccessor(beginBis);
            beginBis.AddSuccessor(b2);
            b2.AddSuccessor(b3);
            b3.AddSuccessor(b4);
            b4.AddSuccessor(b6);
            b6.AddSuccessor(b8);
            b8.AddSuccessor(b9);
            b9.AddSuccessor(b10);
            b10.AddSuccessor(b11);
            b11.AddSuccessor(b12);
            b12.AddSuccessor(b5);
            b5.AddSuccessor(b7);
            b7.AddSuccessor(b13);
            b13.AddSuccessor(b14);
            b14.AddSuccessor(b15);
            b15.AddSuccessor(b17);
            b17.AddSuccessor(b18);
            b18.AddSuccessor(b19);
            b19.AddSuccessor(b20);
            b20.AddSuccessor(b21);
            b21.AddSuccessor(b22);
            b22.AddSuccessor(b23);
            b23.AddSuccessor(b24);
            b24.AddSuccessor(b25);
            b25.AddSuccessor(b26);
            b26.AddSuccessor(b27);
            b27.AddSuccessor(b28);
            b28.AddSuccessor(b29);
            b29.AddSuccessor(end);

            dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_UNLOCK_FOOTJOB_DIALOG_ID, begin,
            delegate ()
            {
                if (StaticDialogElements.dialogData.launchAssistantUnlockFootjobDialog)
                {
                    SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_DOOR_TO_OFFICE_LATE_AT_NIGHT, true);

                    return true;
                }
                return false;
            },
            delegate ()
            {
                StaticDialogElements.specialBackground = "";
                StaticDialogElements.dialogSpecialPortraitIsOn = false;
                StaticDialogElements.specialPortraitSprite = null;
                StaticDialogElements.dialogSpecialBackgroundIsOn = false;
                StaticDialogElements.backgroundImage = null;
            }
            ));
        }
        /*******************************************************************************************************************/

        /*********************************************Dialog to unlock titsjob for Nicole******************************************/
        {
            begin = new MessageBasic("Well, I'm guessing she should have no problem using her breasts to service clients. At least physically. She's clearly equipped for it. Now, she just needs a bit of convincing. So go! Be at your \"breast\"!", NPCCode.NARRATOR);

            beginBis = new MessageBasicWithAction("Dear player. I just want to reassure you by stating that this dialog will not contain terrible puns like the footjob ones. By the way, I wish to apologize for that. It was a terrible mistake that we won't reiterate. Probably.", NPCCode.NARRATOR,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false);

                });
            b2 = new MessageBasic("\"Gros nibard\" Nicole!", NPCCode.MAIN_CHARACTER);

            b3 = new MessageBasic("What? That doesn't mean hello...", NPCCode.ASSISTANT);

            b4 = new MessageBasic("I know. But since I'm out of greetings, I decided to switch to goodbyes.", NPCCode.MAIN_CHARACTER);

            b6 = new MessageBasic("Ehm, this is not a goodbye... At least not in french, which I'm guessing is the language you were trying to use.", NPCCode.ASSISTANT);

            b8 = new MessageBasic("Crap. And what does it mean?", NPCCode.MAIN_CHARACTER);

            b9 = new MessageBasic("Big tits.", NPCCode.ASSISTANT);

            b10 = new MessageBasic("That would explain why I got slapped so much back in Paris...", NPCCode.MAIN_CHARACTER);

            b11 = new MessageBasic("Yeah... The words you were trying to use are \"au revoir\", which means goodbye. The pronunciation is sort of close but, as you experienced, the meanings are drastically different.", NPCCode.ASSISTANT);

            b12 = new MessageBasic("Got it. Buuuut.. it fits you perfectly anyway!", NPCCode.MAIN_CHARACTER);

            b5 = new MessageBasic("Thanks? Don't greet me with it again please.", NPCCode.ASSISTANT);

            b13 = new MessageBasic("I'll think about it.", NPCCode.MAIN_CHARACTER);

            b14 = new MessageBasic("Please don't.", NPCCode.ASSISTANT);

            b15 = new MessageBasic("Well, this is a perfect segue into why I'm here.", NPCCode.MAIN_CHARACTER);

            b17 = new MessageBasic("My breasts?", NPCCode.ASSISTANT);

            b18 = new MessageBasic("Pretty much. And how clients want you to use them.", NPCCode.MAIN_CHARACTER);

            b19 = new MessageBasic("Come on! Are they not satisfied with my hands and feet?", NPCCode.ASSISTANT);

            b20 = new MessageBasic("Sort of. But they always want more, you know...", NPCCode.MAIN_CHARACTER);

            b21 = new MessageBasic("But where will it end? Aren't I doing enough?", NPCCode.ASSISTANT);

            b22 = new MessageBasic("Ehm, I'd rather not answer that...", NPCCode.MAIN_CHARACTER);

            b23 = new MessageBasic("You mean...?", NPCCode.ASSISTANT);

            b25 = new MessageBasic("Note: she's pointing at her crotch. You can thank me later.", NPCCode.NARRATOR);

            b26 = new MessageBasic("Yep.", NPCCode.MAIN_CHARACTER);

            b27 = new MessageBasic("Crap.", NPCCode.ASSISTANT);

            b28 = new MessageBasic("Yep.", NPCCode.MAIN_CHARACTER);

            b29 = new MessageBasic("It's not gonna happen, you know?", NPCCode.ASSISTANT);

            b30 = new MessageBasic("Well, for now, they only want your tits. So, put in perspective, it's not that bad, isn't it?", NPCCode.MAIN_CHARACTER);

            b31 = new MessageBasic("Sure. I might as well...", NPCCode.ASSISTANT);

            b32 = new MessageBasic("Perfect! Have a good day!", NPCCode.MAIN_CHARACTER);

            end = new MessageEnd("Bye...", NPCCode.ASSISTANT);

            begin.AddSuccessor(beginBis);
            beginBis.AddSuccessor(b2);
            b2.AddSuccessor(b3);
            b3.AddSuccessor(b4);
            b4.AddSuccessor(b6);
            b6.AddSuccessor(b8);
            b8.AddSuccessor(b9);
            b9.AddSuccessor(b10);
            b10.AddSuccessor(b11);
            b11.AddSuccessor(b12);
            b12.AddSuccessor(b5);
            b5.AddSuccessor(b13);
            b13.AddSuccessor(b14);
            b14.AddSuccessor(b15);
            b15.AddSuccessor(b17);
            b17.AddSuccessor(b18);
            b18.AddSuccessor(b19);
            b19.AddSuccessor(b20);
            b20.AddSuccessor(b21);
            b21.AddSuccessor(b22);
            b22.AddSuccessor(b23);
            b23.AddSuccessor(b25);
            b25.AddSuccessor(b26);
            b26.AddSuccessor(b27);
            b27.AddSuccessor(b28);
            b28.AddSuccessor(b29);
            b29.AddSuccessor(b30);
            b30.AddSuccessor(b31);
            b31.AddSuccessor(b32);
            b32.AddSuccessor(end);

            dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_UNLOCK_TITSJOB_DIALOG_ID, begin,
            delegate ()
            {
                if (StaticDialogElements.dialogData.launchAssistantUnlockTitsjobDialog)
                {
                    SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_DOOR_TO_OFFICE_LATE_AT_NIGHT, true);

                    return true;
                }
                return false;
            },
            delegate ()
            {
                StaticDialogElements.specialBackground = "";
                StaticDialogElements.dialogSpecialPortraitIsOn = false;
                StaticDialogElements.specialPortraitSprite = null;
                StaticDialogElements.dialogSpecialBackgroundIsOn = false;
                StaticDialogElements.backgroundImage = null;
            }
            ));
        }
        /*******************************************************************************************************************/
    }

}
