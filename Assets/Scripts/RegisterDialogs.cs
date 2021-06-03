using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RegisterDialogs : MonoBehaviour
{
    public static List<Dialog> dialogs = new List<Dialog>();

    public static void CreateDialogs()
    {
        Message begin;
        Message follower;
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
        Message b16;
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

        //*************************************************************************************************************************************//
        begin = new MessageBasic(StaticStrings.ASSISTANT_INTRO[0], NPCCode.ASSISTANT);

        follower = new MessageEndWithResponses(StaticStrings.ASSISTANT_INTRO[1], NPCCode.ASSISTANT,
            new List<string> { "I don't know anything about management. I need help.",
                "I've been managing clubs since before you were born. I'll be OK." },
            delegate (int code)
            {
                switch (code)
                {
                    case 0: SceneManager.LoadScene(StaticStrings.CLUB_SCENE); break;
                    case 1: StaticBooleans.tutorialIsOn = false; break;
                }
            }
        );

        begin.AddSuccessor(follower);


        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.TUTORIAL_OPEN_DIALOG_ID, begin,
            delegate () {

                return StaticBooleans.tutorialIsOn;
            }));

        //*************************************************************************************************************************************//

        begin = new MessageBasic("Boss, we're going to need some staff. We currently have only one girl, and that's clearly not enough. She will quickly overwork herself and when she's out of energy, she won't work anymore.", NPCCode.ASSISTANT);
        follower = new MessageBasic("Solving that is simple though, recruit more girls. For the moment, I'd say 3 would be enough. In the meantime, I'll search the drawers of this desk. I'm pretty sure I've seen some money lying around. We could use that.", NPCCode.ASSISTANT);
        b2 = new MessageEnd("Go then. And don't forget to come back here once you have recruited those girls and signal to me that you have by clicking the \"Finish\" button next to the description of the mission. See you in a bit!", NPCCode.ASSISTANT);

        begin.AddSuccessor(follower);
        follower.AddSuccessor(b2);
        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.FIRST_MISION_OPEN_DIALOG_ID, begin, delegate () { return !StaticBooleans.tutorialIsOn; }));

        begin = new MessageBasic("Good job hiring those girls! This should help with the income. And good news, I found this money I was talking about. Turned out, there were 500" + StaticStrings.MONEY_SIGN + " hanging around in the desk... Yeah, the previous owners were not very good with money. Let's try doing better.", NPCCode.ASSISTANT);
        follower = new MessageEnd("That said, did you notice that our clients couldn't get the little things they wanted? We need to fix that. I suggest we build the bar, pharmacy, condom dispenser and cigarette dispenser. That ought to do the trick. Once you've done that, come back here and confirm it. I bet it will help with our reputation too!", NPCCode.ASSISTANT);

        begin.AddSuccessor(follower);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.SECOND_MISION_OPEN_DIALOG_ID, begin, delegate ()
        {
            //True if the first mission is done
            return StaticFunctions.FindMissionWithID(GMGlobalNumericVariables.gnv.FIRST_MISSION_ID).IsDone();
        }));


        begin = new MessageBasic("It seems that people are noticing us and our brand new equipments. Our reputation is growing. Keep on improving the club!", NPCCode.ASSISTANT);
        follower = new MessageEnd("Now that we have some new stuff and new clients, we need girls able to cater to their needs. They need to be more open to requests. Bring at least one of them to 35 openness.", NPCCode.ASSISTANT);

        begin.AddSuccessor(follower);
        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.THIRD_MISION_OPEN_DIALOG_ID, begin, delegate ()
        {
            //True if the second mission is done
            return StaticFunctions.FindMissionWithID(GMGlobalNumericVariables.gnv.SECOND_MISSION_ID).IsDone();
        }));


        //Old dialog, must keep it because it bugs if not...
        begin = new MessageBasic("Nicely done! Now I don't know about you but seeing what this girl does makes me hot. I think I'll lose the jacket. I'm sure you'll enjoy that.", NPCCode.ASSISTANT);
        follower = new MessageEnd("Better. Now, we are starting to build a reputation here. If we want to be noticed by the important people, we're going to have to build upon that. Maybe we'll get some interesting visitors at some point.", NPCCode.ASSISTANT);

        begin.AddSuccessor(follower);
        dialogs.Add(new Dialog(-10000000, begin, delegate ()
        {
            //True if the second mission is done
            return false;
        }));




        begin = new MessageEnd("You've reached the end of current story content. The dev is working on expanding it, and there should be more in the next update. If you have any remark, do not hesitate to tell him. The \"story\" bits might be finished, but you can still enjoy the game. Have fun!", NPCCode.ASSISTANT);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.END_OF_CONTENT_DIALOG_ID, begin, delegate ()
        {
            //True if the second mission is done
            //return StaticFunctions.FindMissionWithID(GMGlobalNumericVariables.gnv.UNLOCK_ANY_SERVICE_MISSION_ID).IsDone();
            return false;
        }));

        //Old dialog, must keep it because it bugs if not...
        begin = new MessageBasic("Um, boss ? We may have a problem. Or maybe not? I'm not sure...", NPCCode.ASSISTANT);
        Message beginBis = new MessageBasic(" It seems that our recently acquired reputation attracted some potentially dangerous attention.", NPCCode.ASSISTANT);
        b2 = new MessageBasic("To be clear, we have an unsavory visitor down in the parking lot. She asked to talk to you. I have no idea what she wants but I have a feeling it's not good. Let me take you there.", NPCCode.ASSISTANT);
        b3 = new MessageWithResponses("Hmmmph hmphmmh hmmphph, mmmph phmmphm, mmphp !", NPCCode.CRIMINAL_NICOLETTE_SHEA, new List<int> { 0, 1 },
            new List<string> { "Um, could you take off that bandana? I can't understand a word of what you're saying...",
                "Nice tits! You might want to put on some looser clothes in the future though. Not that I complain." },
            delegate (int i)
            {
                StaticDialogElements.dialogData.nicoletteLargePortraitIndex = 1;

            });
        b4 = new MessageBasic("Sorry, I always forget I can't really talk with this on my face. The guys insist that I keep it on, they say it's \"standard uniform\". Bunch of liars, I don't see them wearing it...", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b5 = new MessageBasic("Very funny! I forgot the damn thing in the dryer and didn't have time to buy another one. Plus the guys seem to like it that way, so...", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b6 = new MessageBasic("Anyways, I was saying that I'm here to give you a message and an offer from " + StaticStrings.CRIMINALS_MAFIA_BOSS_TITLE + " " + StaticStrings.CRIMINALS_MAFIA_BOSS_NAME + ". He saw how well you're doing and he wants to greet you in the neighbourhood and offer our organization's services. Don't worry, no \"protection\" tax.", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b7 = new MessageBasic("He's giving you access to our local \"office\". In there, you'll be able to rent some of our services. But you'll need to earn our trust for each of them, so you should treat our guys coming here well. They better be getting out with a giant smile on their face and empty balls.", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b8 = new MessageBasic("Mostly the second part though, I'm seriously tired of doing it myself. There are clearly not enough women in the mob...", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b9 = new MessageBasicWithAction("Anyway, it was a pleasure. Don't forget to come visit us. Oh, by the way, the name's Nicolette Shea. Nice to meet you.", NPCCode.CRIMINAL_NICOLETTE_SHEA, delegate () { StaticDialogElements.dialogData.nicoletteNameKnown = true; });
        b10 = new MessageEnd("And seriously, be sure that your girls work their magic on the guys. My hands are hurting. And my legs. And my mouth. And my ass. And pretty much anything they can use... It's not the worst activity, but it gets tiring, after a while.", NPCCode.CRIMINAL_NICOLETTE_SHEA);

        begin.AddSuccessor(beginBis);
        beginBis.AddSuccessor(b2);
        b2.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b3.AddSuccessor(b5);
        b4.AddSuccessor(b6);
        b5.AddSuccessor(b6);
        b6.AddSuccessor(b7);
        b7.AddSuccessor(b8);
        b8.AddSuccessor(b9);
        b9.AddSuccessor(b10);

        dialogs.Add(new Dialog(-10000000, begin, delegate ()
        {
            return false;
        },
        delegate ()
        {
        }
        ));

        begin = new MessageBasic("Welcome back, boss. I see you're in one piece. That's good!", NPCCode.ASSISTANT);
        beginBis = new MessageWithResponses("So, how did it go? Did she make you an offer you can't refuse? Are we going to have to pay them?", NPCCode.ASSISTANT, new List<int> { 0, 1 },
    new List<string> { "It went fine. No worries. We just got new business partners.",
                "Yep, we're screwed. They asked me for a hundred thousand or your ass. And since we don't have the money..." },
    delegate (int i) { }
    );
        b2 = new MessageBasic("Oh, OK then. I was affraid they'd ask a ridiculous amount of money and you'd have to send me to work for them to try and pay.", NPCCode.ASSISTANT);
        b3 = new MessageBasic("Seriously! Crap, given how these mobster guys are, I'm gonna be sore all over...", NPCCode.ASSISTANT);
        b4 = new MessageBasic("Wait, you're kidding, right? Dick move, boss. I felt my butt clench for a second. It's usually the other way around.", NPCCode.ASSISTANT);
        end = new MessageEnd("What do you say we go see what they have to offer? We should now be able to access their office from the club.", NPCCode.ASSISTANT);

        begin.AddSuccessor(beginBis);
        beginBis.AddSuccessor(b2);
        beginBis.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b4.AddSuccessor(end);
        b2.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.FOURTH_MISION_CLOSE_DIALOG_ID, begin, delegate ()
        {
            //True if the fourth mission is done
            if (StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.FOURTH_MISION_MIDDLE_DIALOG_ID).done
            && SceneManager.GetActiveScene().name.Equals(StaticStrings.OFFICE_SCENE))
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false);
                return true;
            }
            return false;
        },
        delegate ()
        {
            //Make the crime office available
            StaticFunctions.crimeOfficeAvailable = 1;
            if (SceneManager.GetActiveScene().name == StaticStrings.CLUB_SCENE)
            {
                SceneManager.LoadScene(StaticStrings.CLUB_SCENE);
            }
            StaticDialogElements.specialBackground = "";
            StaticDialogElements.dialogSpecialPortraitIsOn = false;
            StaticDialogElements.specialPortraitSprite = null;
            StaticDialogElements.dialogSpecialBackgroundIsOn = false;
            StaticDialogElements.backgroundImage = null;
        }
        ));

        begin = new MessageBasic("Welcome, welcome to our humble office! If you are here to get some interesting and not always legal services, you are in the right place!", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b2 = new MessageWithResponses("But before we continue, do you want an explanation of how this works?", NPCCode.CRIMINAL_NICOLETTE_SHEA,
            new List<int> { 0, 1 }, new List<string> { "I'm at a loss here, teach me the basics.", "No thanks, I already know all I need." },
            delegate (int i) { });
        b3 = new MessageBasic("In here, you can subscribe to different services that will help when your girls work. You'll pay a fixed amount every day for each service you subscribe to. You can subscribe or unsubscribe at any moment.", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b4 = new MessageBasic("But you're gonna need to unlock those services first. For that, you need to show us that we can trust you. Starting now, you'll see some of us coming into your establishment to get some relaxation with your girls.", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b5 = new MessageBasic("When that happens, if the client gets out happy, you'll earn some points with us. You can then spend those points here to unlock the services you want. After that, you'll be able to subscribe to the service you just unlocked.", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b6 = new MessageBasic("From now on, you should also be able to recruit girls who want for you to have some influence. Go check your candidates, there should be new ones.", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b7 = new MessageEnd("Now that you're up-to-date, want to take a look at what we offer?", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b8 = new MessageEnd("Perfect! Want to take a look at what we offer then?", NPCCode.CRIMINAL_NICOLETTE_SHEA);

        begin.AddSuccessor(b2);
        b2.AddSuccessor(b3);
        b2.AddSuccessor(b8);
        b3.AddSuccessor(b4);
        b4.AddSuccessor(b5);
        b5.AddSuccessor(b6);
        b6.AddSuccessor(b7);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.CRIME_SERVICE_TUTORIAL_DIALOG_ID, begin,
            delegate ()
            {
                if (SceneManager.GetActiveScene().name.Equals(StaticStrings.CRIME_SERVICES_SCENE))
                {
                    SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_CRIME_OFFICE, false, true,
                        StaticStrings.CRIMINALS_DIRECTORY + StaticStrings.CRIMINALS_NICOLETTE_SHEA_DIRECTORY + "portrait_2_no_background");
                    return true;
                }
                return false;
            },
            delegate () {
                StaticDialogElements.specialBackground = "";
                StaticDialogElements.dialogSpecialPortraitIsOn = false;
                StaticDialogElements.specialPortraitSprite = null;
                StaticDialogElements.dialogSpecialBackgroundIsOn = false;
                StaticDialogElements.backgroundImage = null;
            }
        ));


        begin = new MessageBasic("Interesting woman. She is pretty nice, and good looking with that. Far from your cliché gangster...", NPCCode.ASSISTANT);
        beginBis = new MessageWithResponses("Do you think the short clothes are part of her uniform? \nOddly enough, I don't imagine this would bother her...", NPCCode.ASSISTANT,
           new List<int> { 0, 1 }, new List<string> { "I hope it is! This is the kind of business partner I want!", "Honestly, not a fan. A good business relationship requires stylish and fitting clothes." }, delegate (int i) { });
        b2 = new MessageBasic("Yeah, I figured. Don't get your hopes up though, I won't dress like that at work!", NPCCode.ASSISTANT);
        b3 = new MessageBasic("Business before pleasure eh? I can get behind that. Although, \"behind\" stuff is not my specialty...", NPCCode.ASSISTANT);
        end = new MessageEnd("In any case, our new partners seem to provide interesting options. What do you think of trying one of their services? I'll add it to our todo list.", NPCCode.ASSISTANT);

        begin.AddSuccessor(beginBis);
        beginBis.AddSuccessor(b2);
        beginBis.AddSuccessor(b3);
        b2.AddSuccessor(end);
        b3.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.CRIME_SERVICE_TUTORIAL_DIALOG_CLOSE_ID, begin,
            delegate ()
            {
                //True if the current scene is the crime office
                if (!SceneManager.GetActiveScene().name.Equals(StaticStrings.CRIME_SERVICES_SCENE)
                    && StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.CRIME_SERVICE_TUTORIAL_DIALOG_ID).done)
                {
                    SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_MAIN_CLUB, false, false);
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

        begin = new MessageBasic("I hope this is the beginning of a long and profitable relationship. And that they don't end up stabbing us and taking control of the club...", NPCCode.ASSISTANT);
        b2 = new MessageWithResponses("Hey, don't worry, I'm sure you'll bring us way more money alive than dead, so no reason to give you a tour of the bottom of the river!", NPCCode.CRIMINAL_NICOLETTE_SHEA,
            new List<int> { 0, 1 }, new List<string> { "That's reassuring...", "What are you doing here?" }, delegate (int i) { });
        b3 = new MessageBasic("See? We're not so bad, so long as we like you. And hotties like you both are easily likeable, so don't worry!", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b4 = new MessageBasic("Nothing in particular, I saw the light so I came in. Nice place, by the way. A bit messy but it feels cozy.", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b5 = new MessageBasic("Would you mind getting out of my office? This is an employee only area.", NPCCode.ASSISTANT);
        b6 = new MessageBasic("Sure, but only if you show me some skin!", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b7 = new MessageBasic("What? Is this a joke?", NPCCode.ASSISTANT);
        b8 = new MessageBasic("Depends. Would you show me some if it wasn't?", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b9 = new MessageBasic("Get out, please.", NPCCode.ASSISTANT);
        b10 = new MessageBasic("Sure, sorry to bother you, miss prude. Have a good one!", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b11 = new MessageWithResponses("Would you believe that...", NPCCode.ASSISTANT,
            new List<int> { 0, 1 }, new List<string> { "I mean she's not totally wrong, you could show some more skin.", "Erm, sure unbelievable." }, delegate (int i) { });
        b12 = new MessageEnd("No. I think you should get out too!", NPCCode.ASSISTANT);
        b13 = new MessageEnd("Somehow, you don't seem that convinced... Maybe you should get out too?", NPCCode.ASSISTANT);


        begin.AddSuccessor(b2);
        b2.AddSuccessor(b3);
        b2.AddSuccessor(b4);
        b3.AddSuccessor(b5);
        b4.AddSuccessor(b5);
        b5.AddSuccessor(b6);
        b6.AddSuccessor(b7);
        b7.AddSuccessor(b8);
        b8.AddSuccessor(b9);
        b9.AddSuccessor(b10);
        b10.AddSuccessor(b11);
        b11.AddSuccessor(b12);
        b11.AddSuccessor(b13);


        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.UNLOCK_ANY_SERVICE_MISSION_CLOSE_DIALOG_ID, begin,
    delegate ()
    {
        //True if the current scene is the crime office
        if (StaticFunctions.FindMissionWithID(GMGlobalNumericVariables.gnv.UNLOCK_ANY_SERVICE_MISSION_ID).isDone)
        {
            SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);

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

        //************************************* First late night meeting dialog ******************************************//
        begin = new MessageBasicWithAction("After the last work night, you decide to take a tour of the club to check that everything is alright before closing.", NPCCode.NARRATOR,
            delegate ()
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_DOOR_TO_OFFICE_LATE_AT_NIGHT, true);
            }
            );
        b2 = new MessageBasicWithAction("Everything seems to be fine, except for Nicole's office. The door is slightly open and a small ray of light is shining through the crack. Wondering what she might be doing at such an hour of the night, you decide to go in and check on her.", NPCCode.NARRATOR,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0004", true);
                }
            );
        b3 = new MessageBasicWithAction("Hey Nicole! I see you're working late tonight. A lot of work today?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0003", true);
                }
            );
        b4 = new MessageBasicWithAction("Oh, hey boss. Yeah, big day today. I wanted to finish a few things before going home.", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0007", true);
                }
              );
        b5 = new MessageBasicWithAction("Good. Don't overwork yourself though. I wouldn't want you to get bags under those beautiful eyes.", NPCCode.MAIN_CHARACTER,
                delegate ()
                    {
                        SetPortraitAndBackgroundForNextMessage(
                            StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0009", true);
                    }
                );
        b6 = new MessageWithResponses("Are you sure you care about my eyes? It's not usually this pair that yours are glued on...", NPCCode.ASSISTANT,
            new List<int> { 0, 1 }, new List<string> { "Make a joke", "Compliment her" }, delegate (int i)
            {
                SetPortraitAndBackgroundForNextMessage(
                    StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0017", true);
            });

        b7 = new MessageBasicWithAction("I mean, I like all kind of pairs, arms, legs, eyes, it's all good. But you're right I tend to admire your arms too much. I'll spend more time looking somewhere else. Is there a specific place you'd like me to ogle at ? ", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0010", true);
                });
        b8 = new MessageBasicWithAction("Oh, shut up you. You know what I mean. Your eyes tend to focus on a very specific, rounded part of me. How can you even pay attention to mine with so many beautiful and mostly naked girls around? I would think you'd be too busy eyeing them...", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0029", true);
                });

        b9 = new MessageBasicWithAction("Can you blame me though? It's hard not to look at them. They basically are a shining beacon of beauty and sexiness. Almost world wonders, really.", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0022", true);
                });
        b10 = new MessageBasicWithAction("Aha, if mine are almost world wonders, then most of the girls here have godesslike breasts... Which makes me think, how can you even look at mine so much when there are so many beautiful and mostly naked girls around?", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0029", true);
                }
                );

        b11 = new MessageBasicWithAction("That's a good question! But you're underselling yourself you know. You're as beautiful as those girls, maybe even more. Add to that the attraction of mystery, and you're suddenly the sexiest and most desirable woman here.", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0021", true);
                }
                );
        b12 = new MessageBasicWithAction("It's clearly a lie but I'll take the compliment. And I guess I'll also keep the mystery!", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0022", true);
                }
                );
        b13 = new MessageBasicWithAction("Too bad, I was hoping I could uncover it a little today! Well, that's me going home defeated and demoralized. See you tomorrow!", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0020", true);
                }
                );
        b14 = new MessageEnd("See you tomorrow. And don't stay demoralized too long, we'll need you in top shape! ", NPCCode.ASSISTANT);

        begin.AddSuccessor(b2);
        b2.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b4.AddSuccessor(b5);
        b5.AddSuccessor(b6);
        b6.AddSuccessor(b7);
        b6.AddSuccessor(b9);

        b7.AddSuccessor(b8);

        b9.AddSuccessor(b10);

        b8.AddSuccessor(b11);
        b10.AddSuccessor(b11);
        b11.AddSuccessor(b12);
        b12.AddSuccessor(b13);
        b13.AddSuccessor(b14);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_ID, begin,
        delegate ()
        {
            if (GMClubData.day >= GMGlobalNumericVariables.gnv.FIRST_LATE_NIGHT_MEETING_DAY && StaticBooleans.dayRecapOver)
            {

                StaticDialogElements.specialBackground = StaticStrings.DIALOG_BACKGROUNDS_NAME_MAIN_CLUB;
                StaticDialogElements.dialogSpecialPortraitIsOn = true;
                StaticDialogElements.specialPortraitSprite = Resources.Load<Sprite>(StaticStrings.EMPTY_PORTRAIT);
                return true;
            }
            return false;
        },
        delegate ()
        {
            StaticBooleans.dayRecapOver = false;
            StaticDialogElements.dialogData.assistantFirstLateNightMeetingSeen = true;
            StaticDialogElements.specialBackground = "";
            StaticDialogElements.dialogSpecialPortraitIsOn = false;
            StaticDialogElements.specialPortraitSprite = null;
            StaticDialogElements.dialogSpecialBackgroundIsOn = false;
            StaticDialogElements.backgroundImage = null;
        }
        ));

        //************************************************************************************************************************//

        //********************************************************* Second late night meeting dialog **********************************//
        begin = new MessageBasicWithAction("Today too you decide to check the club before closing.", NPCCode.NARRATOR,
    delegate ()
    {
        SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_DOOR_TO_OFFICE_LATE_AT_NIGHT, true);
    }
    );
        b2 = new MessageBasicWithAction("And again you find Nicole's office slightly open and the light turned on.", NPCCode.NARRATOR,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0034", true);
                }
            );
        b3 = new MessageBasicWithAction("Hello Nicole. Working late again? Today wasn't too busy.", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0035", true);
                }
            );
        b4 = new MessageBasicWithAction("Yes, well, I still had some papers to finish. You know, the usual administrative paperwork. Don’t worry for my pretty eyes, I’ll get my beauty sleep.", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0029", true);
                }
              );
        b5 = new MessageBasicWithAction("Are you sure? You seem tired…", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0043", true);
                }
                );
        b6 = new MessageWithResponses("Don’t worry boss. I appreciate your concern but I’m fine.", NPCCode.ASSISTANT,
            new List<int> { 0, 1 }, new List<string> { "Insist", "Let it go" }, delegate (int i)
            {
                if (i == 0)
                {
                    StaticDialogElements.dialogData.assistantSecondLateNightMeetingGoodAnswer = true;
                }
                else
                {
                    StaticDialogElements.dialogData.assistantSecondLateNightMeetingGoodAnswer = false;
                }
                SetPortraitAndBackgroundForNextMessage(
                    StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0033", true);
            });


        b7 = new MessageBasicWithAction("Listen, I don’t want to be insistent but I care about you and want to be sure that everything is OK. I’ve seen you working very late pretty often and I don’t want you to get sick. I do care for my employees and want them to…", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0061", true);
                });
        b8 = new MessageBasicWithAction("…", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0070", true);
                });

        b9 = new MessageBasicWithAction("Wait, what are you doing?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0072", true);
                });
        b10 = new MessageBasicWithAction("Shutting you up. I’m fine, OK. Don’t worry about me. I’ll finish this paperwork and go right back home, alright?", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0075", true);
                }
                );

        b11 = new MessageBasicWithAction("OK, then. I mean, you have a good point. Two of them, really. I’m leaving you alone. Good night!", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0076", true);
                }
                );

        b12 = new MessageBasicWithAction("Good night, boss!", NPCCode.ASSISTANT,
            delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_MAIN_CLUB, true);
        }
                );

        b18 = new MessageBasicWithAction("You've unlocked the assistant store! You can now buy various things for Nicole, including new outfits, dates and dialogs, with Nicole Points.", NPCCode.NARRATOR,
        delegate ()
        {

            SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_MAIN_CLUB, true);
        }
        );

        b19 = new MessageBasicWithAction("For the moment, there is only one dialog to unlock, but more will appear after progressing the story. Go to Nicole's office and click the \"To assistant store\" button to see more.", NPCCode.NARRATOR,
        delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_MAIN_CLUB, true);
        }
);
        b20 = new MessageEnd("Earning points will be explained later, after advancing the story (that means buying and seeing this new dialog!).", NPCCode.NARRATOR);


        b13 = new MessageBasicWithAction("OK then. Sorry to bother you.", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_FIRST_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0029", true);
                }
                );
        b14 = new MessageBasicWithAction("Listen, I’m fine, OK? No need to worry about me. Thank you for caring though.", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0035", true);
                }
            );
        b15 = new MessageBasicWithAction("Very well. I’ll see you tomorrow then. Good night!", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0034", true);
                }
            );

        b16 = new MessageBasicWithAction("Good night boss!", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_MAIN_CLUB, true);
                }
            );

        b17 = new MessageEnd("You’ve been nice here, but there is something more to do. Try again later!", NPCCode.NARRATOR);


        begin.AddSuccessor(b2);
        b2.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b4.AddSuccessor(b5);
        b5.AddSuccessor(b6);

        b6.AddSuccessor(b7);
        b6.AddSuccessor(b13);

        b7.AddSuccessor(b8);
        b8.AddSuccessor(b9);
        b9.AddSuccessor(b10);
        b10.AddSuccessor(b11);
        b11.AddSuccessor(b12);
        b12.AddSuccessor(b18);
        b18.AddSuccessor(b19);
        b19.AddSuccessor(b20);

        b13.AddSuccessor(b14);
        b14.AddSuccessor(b15);
        b15.AddSuccessor(b16);
        b16.AddSuccessor(b17);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_ID, begin,
        delegate ()
        {
            if (GMClubData.day >= GMGlobalNumericVariables.gnv.FIRST_LATE_NIGHT_MEETING_DAY + GMGlobalNumericVariables.gnv.SECOND_LATE_NIGHT_MEETING_DAY_DELAY
            //At least two days after first late night meeting
            && StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.FOURTH_MISION_OPEN_DIALOG_ID).done //If the lose jacket dialog has been seen
            && !StaticDialogElements.dialogData.stopTestingSecondLateNightMeeting
            && StaticBooleans.dayRecapOver)//And the day recap is over
            {
                // 1/2 chance to start the dialog
                if (StaticFunctions.Random(1) == 1)
                {
                    StaticDialogElements.specialBackground = StaticStrings.DIALOG_BACKGROUNDS_NAME_MAIN_CLUB;
                    StaticDialogElements.dialogSpecialPortraitIsOn = true;
                    StaticDialogElements.specialPortraitSprite = Resources.Load<Sprite>(StaticStrings.EMPTY_PORTRAIT);
                    return true;
                }
                else
                {
                    StaticDialogElements.dialogData.stopTestingSecondLateNightMeeting = true;
                }

            }
            return false;
        },
        delegate ()
        {
            StaticBooleans.dayRecapOver = false;
            StaticDialogElements.specialBackground = "";
            StaticDialogElements.dialogSpecialPortraitIsOn = false;
            StaticDialogElements.specialPortraitSprite = null;
            StaticDialogElements.dialogSpecialBackgroundIsOn = false;
            StaticDialogElements.backgroundImage = null;
            if (!StaticDialogElements.dialogData.assistantSecondLateNightMeetingGoodAnswer)
            {
                Dialog aux = StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_ID);
                aux.done = false;
                aux.seen = false;
                aux.currentMessage = aux.firstMessage;
                //If the right answer wasn't chosen, reset this dialog
            }
            else
            {
                StaticAssistantData.data.assistantPoints += 1;
                StaticAssistantData.data.assistantStoreUnlocked = true;
            }
        }
        ));

        //************************************************************************************************************************//

        //********************************************************* Third late night meeting dialog **********************************//
        begin = new MessageBasicWithAction("Another late night tour of the club. You find nothing except for Nicole's office open. You know the drill.", NPCCode.NARRATOR,
    delegate ()
    {
        SetPortraitAndBackgroundForNextMessage(
            StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0075", true);
    }
    );
        b2 = new MessageBasicWithAction("Hello Nicole! How are you tonight? Working late again?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0076", true);
                }
            );
        b3 = new MessageWithResponses("Hey boss... Yes, another late night. And what about you? You keep worrying about me working late but you're here too, right?", NPCCode.ASSISTANT,
            new List<int> { 0, 1 }, new List<string> { "Joke", "You're about to go home" }, delegate (int i)
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0075", true);
        });

        b4 = new MessageBasicWithAction("Oh, but I don't need to sleep or rest. You see, I'm a robot from the future here to ensure that my future creator will be rich by inheriting this club when it's prosperous. So I haven't been programmed with sleeping or resting, really.", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0083", true);
                }
              );
        b5 = new MessageBasicWithAction("Oh, you're the Terminator for sex clubs then? The Sexminator?", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0075", true);
                }
                );
        b6 = new MessageBasicWithAction("I'LL BE BACK! Problem is, I'm pretty sure this is the name of a porn movie parody.", NPCCode.MAIN_CHARACTER,
            delegate ()
            {
                SetPortraitAndBackgroundForNextMessage(
                    StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0099", true);
            });


        b7 = new MessageBasicWithAction("Oh yeah, I know. That's why I'm just about to end the day, you know, one last tour before going back home. Plus, I live just above the club, so I don't have a very long travel time.", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0083", true);
                });
        b8 = new MessageBasicWithAction("Well, that's convenient.", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0075", true);
                });

        b9 = new MessageBasicWithAction("Perks of being the owner, I guess.", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0099", true);
                });



        b10 = new MessageBasicWithAction("The discussion dies down, and you take advantage of that to ask the question on everybody's mind:", NPCCode.NARRATOR,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0075", true);
                }
                );

        b11 = new MessageBasicWithAction("Say, I also have a question: why are you in your underwear?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0076", true);
                }
                );
        b12 = new MessageBasicWithAction("I was hoping you wouldn't point that out... It's just more comfortable, you know. Plus nobody's here at this hour, usually.", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0099", true);
                });



        b13 = new MessageBasicWithAction("See? There is a reason behind her being in her underwear. It's not because we want you to see some sexy stuff. No, no, no it completely makes logical sense. We swear. We don't just show tits for the heck of it. We have standards here, writing standards...", NPCCode.NARRATOR,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0101", true);
                }
                );
        b14 = new MessageBasicWithAction("Oh, OK. Seems logical. I guess. But you know, you could dress like that any time here. It's not like it would be unusual...", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0100", true);
                }
            );
        b15 = new MessageBasicWithAction("Well, I don't enjoy showing myself like that. It's just a comfort thing. Plus I feel like it wouldn't be very professional for your assistant to walk around half-naked...", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0099", true);
                }
            );

        b16 = new MessageBasicWithAction("In any other establishment it wouldn't, but here, I don't think this would be a problem. But hey, at least, you seem comfortable that way with me, right?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
    StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0101", true);
                }
            );

        b17 = new MessageBasicWithAction("Not really. I'm kind of embarassed, right now...", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
    StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0100", true);
                }
            );

        b18 = new MessageBasicWithAction("Don't be! You have a great body, you should be proud of showing it, not ashamed!", NPCCode.MAIN_CHARACTER,
        delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
    StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0112", true);
        }
    );

        b19 = new MessageBasicWithAction("Thank you. This sounds like the kind of bullshit you say to all the girls here, but I'll take it...", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
    StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0110", true);
}
);
        b20 = new MessageBasicWithAction("I mean, yeah, this is the kind of stuff I say to all the girls here... But it's also true for all of them. They all have great bodies and should be proud of showing them...", NPCCode.MAIN_CHARACTER,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0111", true);
}
);

        b21 = new MessageBasicWithAction("They are hot, are they? You do have a talent at finding beauty...", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0101", true);
}
);

        b22 = new MessageBasicWithAction("I do, so when I say you're beautiful, you can assume that it means you're at least as good looking as them. But you also have something more.", NPCCode.MAIN_CHARACTER,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0104", true);
}
);

        b23 = new MessageBasicWithAction("Oh? And what would that be?", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0105", true);
}
);

        b24 = new MessageBasicWithAction("You're smart. Really smart. That clearly adds to your charm. A lot, in fact.", NPCCode.MAIN_CHARACTER,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0109", true);
}
);

        b25 = new MessageBasicWithAction("Smart enough to not fall for your compliments, maybe?", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0112", true);
}
);

        b26 = new MessageBasicWithAction("Certainly enough to not fall for insincere ones, that's for sure. But since mine are genuine, I do believe you can see that I tell the truth and only the truth.", NPCCode.MAIN_CHARACTER,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0111", true);
}
);

        b27 = new MessageBasicWithAction("Sure, why not. It's pleasant to think you are, anyway...", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0110", true);
}
);

        b28 = new MessageBasicWithAction("Let me prove it to you. What about a date with me sometimes? We'll go grab a coffee or spend some time in a bar after work. That'll make you stop working and you'll see what I really think.", NPCCode.MAIN_CHARACTER,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0099", true);
}
);

        b29 = new MessageBasicWithAction("Sure, why not. Pass by my office and we'll set up a time and a place. That should be fun, at the very least.", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0100", true);
}
);

        b30 = new MessageBasicWithAction("With pleasure! I'll leave you to your work now. See you tomorrow!", NPCCode.MAIN_CHARACTER,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0116", true);
}
);

        b31 = new MessageBasicWithAction("Good night boss!", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_FOLDER + "0120", true);
}
);

        b32 = new MessageBasic("Sweet moves! You managed to convince her to go on a date. You're good at this, aren't you?", NPCCode.NARRATOR);


        end = new MessageEnd("You can now go on a date with her. Go to her office and click the \"Date\" button. Be careful though, you can only do it once per day.", NPCCode.NARRATOR);


        begin.AddSuccessor(b2);
        b2.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b3.AddSuccessor(b7);

        b4.AddSuccessor(b5);
        b5.AddSuccessor(b6);
        b6.AddSuccessor(b10);

        b7.AddSuccessor(b8);
        b8.AddSuccessor(b9);
        b9.AddSuccessor(b10);
        b10.AddSuccessor(b11);
        b11.AddSuccessor(b12);
        b12.AddSuccessor(b13);
        b13.AddSuccessor(b14);
        b14.AddSuccessor(b15);
        b15.AddSuccessor(b16);
        b16.AddSuccessor(b17);
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
        b31.AddSuccessor(b32);
        b32.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_ID).done
            //If the second late meeting dialog has been seen
            && StaticBooleans.dayRecapOver//And the day recap is over
            && StaticAssistantData.data.thirdMeetingDialogBought //And the third dialog has been bought
            )
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_DOOR_TO_OFFICE_LATE_AT_NIGHT, true);
                return true;
            }
            return false;
        },
        delegate ()
        {
            StaticBooleans.dayRecapOver = false;
            StaticDialogElements.specialBackground = "";
            StaticDialogElements.dialogSpecialPortraitIsOn = false;
            StaticDialogElements.specialPortraitSprite = null;
            StaticDialogElements.dialogSpecialBackgroundIsOn = false;
            StaticDialogElements.backgroundImage = null;

        }
        ));

        //************************************************************************************************************************//

        //********************************************************* First café date dialog **********************************//
        begin = new MessageBasicWithAction("Ready for your first date with Nicole, you join her in her office.", NPCCode.NARRATOR,
    delegate ()
    {
        SetPortraitAndBackgroundForNextMessage(
           StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, true);
    }
    );
        b2 = new MessageBasicWithAction("Hey Nicole! Ready for tonight?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, true,
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER + StaticStrings.ASSISTANT_UNDRESS_1_PORTRAIT);
                }
            );
        b3 = new MessageBasicWithAction("About to be. Give me 5 minutes to change into something more suitable.", NPCCode.ASSISTANT, delegate ()
            {
                SetPortraitAndBackgroundForNextMessage(
                    StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, true,
                    StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER + StaticStrings.ASSISTANT_UNDRESS_1_PORTRAIT);
            });

        b4 = new MessageBasicWithAction("Oh? You're not going with that outfit?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, true,
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER + StaticStrings.ASSISTANT_UNDRESS_1_PORTRAIT);
                }
              );
        b5 = new MessageBasicWithAction("I figured wearing my work clothes wouldn't be fitting for a date. Be right back.", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, true);
                }
                );
        b6 = new MessageBasicWithAction("She leaves her office and comes back 5 minutes later in something... great.", NPCCode.NARRATOR,
            delegate ()
            {
                SetPortraitAndBackgroundForNextMessage(
                    StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, true,
                    StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                    + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE);
            });


        b7 = new MessageWithResponses("So, what do you think?", NPCCode.ASSISTANT, new List<int> { 0, 1 }, new List<string> { "Wow!", "Too much clothes" },
                delegate (int i)
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, true,
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                        + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE);
                });
        b8 = new MessageBasicWithAction("I mean... wow. Not much else to say.", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, true,
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                        + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_SMILING_POSE);
                });

        b9 = new MessageBasicWithAction("Thanks. Shall we go then? Where are we going by the way?", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, true,
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                        + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_SMILING_POSE);
                });



        b10 = new MessageBasicWithAction("A small bar closeby. I bet the waiters' minds will just explode when you get in. I can't wait to see that!", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, true,
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                        + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE);
                }
                );

        b11 = new MessageBasicWithAction("Haha, a litteral mind blowing. I kinda want to see that too.", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, true,
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                        + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE);
                }
                );
        b12 = new MessageBasicWithAction("Let's go then. ", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, true);
                });



        b13 = new MessageBasicWithAction("Too much clothes. Why not take something off?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, true,
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                        + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_ANGRY_POSE);
                }
                );
        b14 = new MessageBasicWithAction("Seriously? Isn't that enough?", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, true,
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                        + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_ANGRY_POSE);
                }
            );
        b15 = new MessageBasicWithAction("I'm just joking! Sorry, it was pretty bad. My apologies. Shall we go now ?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, true,
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                        + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE);
                }
            );

        b16 = new MessageBasicWithAction("Sure, lead the way. Where are we going?", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, true,
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                        + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE);
                }
            );

        b17 = new MessageBasicWithAction("A small bar closeby. It's a nice place, I go there after work from time to time.", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, true,
                        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                        + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_ANGRY_POSE);
                }
            );

        b18 = new MessageBasicWithAction("Let's go then.", NPCCode.ASSISTANT,
        delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, true);
        }
    );

        b19 = new MessageBasicWithAction("5 minutes later...", NPCCode.NARRATOR,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, false, true,
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                        + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE
        );
}
);
        b20 = new MessageBasicWithAction("It's a nice place. Very cozy.", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, false, true,
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                        + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE
        );
}
);

        b21 = new MessageBasicWithAction("I told you. Come, have a seat. So...", NPCCode.MAIN_CHARACTER,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, true);
}
);

        b22 = new MessageBasicWithAction("So, the date begins. Now, this is not your normal, run of the mill, question/answer thing. You already now how to do that. No, your main objective is to avoid being a jerk and try to be charming.", NPCCode.NARRATOR,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, true);
}
);

        b23 = new MessageBasicWithAction("To do that, you'll have to try looking in her eyes and not her other... assets. I know, very hard to do.", NPCCode.NARRATOR,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_TUTORIAL, true);
}
);

        b24 = new MessageBasicWithAction("Here is how: First, take a look at the images being currently displayed. The leftmost icon represents your gaze. You're gonna have to avoid looking (colliding) at her breasts (second icon), and look into her eyes (third icon). You'll see, it's pretty easy to understand. Move with WASD or the arrow keys.", NPCCode.NARRATOR,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_TUTORIAL, true);
}
);

        b25 = new MessageBasicWithAction("Sometimes, a special dress icon will appear (currently displayed as the rightmost icon). If you manage to collect it, she will either change her pose or lose a piece of clothing for this date and all the future dates here. Isn't it nice?", NPCCode.NARRATOR,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_TUTORIAL, true);
}
);

        b26 = new MessageBasicWithAction("Finally, the date will end after you either collide with 3 wrong items, look 30 times in her eyes or " + GMGlobalNumericVariables.gnv.DATE_GAME_TIME + " seconds pass.", NPCCode.NARRATOR,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_TUTORIAL, true);
}
);

        b27 = new MessageBasicWithAction("If you unfortunately looked too much at her breasts, you will only get the minimum amount of points for that date (which is 5). If you reach 30 gazes collected, you will get 30 points. If the timer ends, you will get a number of points equal to the number of gazes you collected (with a minimum of 5).", NPCCode.NARRATOR,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_TUTORIAL, true);
}
);

        b30 = new MessageWithResponses("Those points are called \"Nicole Points\" and you can spend them to buy things related to Nicole. You can also exchange money for those points. You'll find Nicole's \"shop\" and \"money exchanger\" in her office. Got it?", NPCCode.NARRATOR,
    new List<int> { 0, 1 }, new List<string> { "Yes", "No" },
delegate (int i)
{
    SetPortraitAndBackgroundForNextMessage(
    StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_TUTORIAL, true);
}
);

        b28 = new MessageEnd("Good. Let's start then. Good luck.", NPCCode.NARRATOR);

        b29 = new MessageBasicWithAction("Okay, let me explain again.", NPCCode.NARRATOR, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, true);
        });

        begin.AddSuccessor(b2);
        b2.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b3.AddSuccessor(b7);

        b4.AddSuccessor(b5);
        b5.AddSuccessor(b6);
        b6.AddSuccessor(b7);

        b7.AddSuccessor(b8);
        b7.AddSuccessor(b13);

        b8.AddSuccessor(b9);
        b9.AddSuccessor(b10);
        b10.AddSuccessor(b11);
        b11.AddSuccessor(b12);
        b12.AddSuccessor(b19);

        b13.AddSuccessor(b14);
        b14.AddSuccessor(b15);
        b15.AddSuccessor(b16);
        b16.AddSuccessor(b17);
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
        b27.AddSuccessor(b30);
        b30.AddSuccessor(b28);
        b30.AddSuccessor(b29);

        b29.AddSuccessor(b22);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_FIRST_CAFE_DATE_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticDialogElements.dialogData.launchFirstCafeDateDialog)
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


            //Load the date game
            RegisterDates.currentDate = RegisterDates.dates[0];
            SceneManager.LoadScene(StaticStrings.DATE_SCENE);

        }
        ));

        //********************************************************* Secretary first undress dialog redone **********************************//
        begin = new MessageBasicWithAction("Nicely done! Now I don't know about you but seeing what this girl does makes me hot. I think I'll lose the jacket. I'm sure you'll enjoy that.", NPCCode.ASSISTANT,
            delegate ()
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false,
                    true, StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER + "portrait_undress_1");
                RegisterAssistantCostumes.FindAssistantCostumeWithName(StaticStrings.ASSISTANT_STARTING_OUTFIT).currentLevel++;
            }
        );
        follower = new MessageEnd("Better. Now, we are starting to build a reputation here. If we want to be noticed by the important people, we're going to have to build upon that. Maybe we'll get some interesting visitors at some point.", NPCCode.ASSISTANT);

        begin.AddSuccessor(follower);
        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.FOURTH_MISION_OPEN_DIALOG_ID, begin, delegate ()
        {
            //True if the second mission is done
            return StaticFunctions.FindMissionWithID(GMGlobalNumericVariables.gnv.THIRD_MISSION_ID).IsDone();
        }));

        //******************************************************************************************************************

        begin = new MessageBasicWithAction("One night, during work hours, you find Nicole in her office.", NPCCode.NARRATOR,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
       StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, true);
}
);
        b2 = new MessageBasicWithAction("Hello Nicole! How are you doing tonight ?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );
        b3 = new MessageBasicWithAction("I'm doing great, thank you! Tell me, what can I do for you?", NPCCode.ASSISTANT, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
        });

        b4 = new MessageBasicWithAction("How did you know I wanted to ask you something? Am I that predictable?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
              );
        b5 = new MessageBasicWithAction("Well, you generally come here either to ask me on a date, or to ask for something. And since we are still open, I assume you don't want to go have a drink. So, what is it?", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
                );
        b6 = new MessageBasicWithAction("Impressive. Well, you're right, I've got something to ask of you. It's kind of a difficult topic, however.", NPCCode.MAIN_CHARACTER,
            delegate ()
            {
                SetPortraitAndBackgroundForNextMessage(
                    StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
            });


        b7 = new MessageBasicWithAction("Go ahead, shoot. I won't bite. Maybe.", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                });
        b8 = new MessageBasicWithAction("Hopefully you won't... Ok, here it is: you know that sometimes we have nights where we could use having more girls to satisfy all the customers?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                });

        b9 = new MessageBasicWithAction("Yes? During those nights, some clients always ask me to \"relieve\" them while they wait. Not my favorite moments...", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                });



        b10 = new MessageBasicWithAction("Exactly. And that's why I was thinking that maybe you would accept to, ehm, actually do that?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
                );

        b11 = new MessageBasicWithAction("What? No! I'm not a whore!", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
                );
        b12 = new MessageBasicWithAction("Wait, wait, wait, not like that! Just, you know, dancing a little. Give them a good show, nothing extreme, no sex, just keeping them happy for a while. You're gorgeous and I'm sure that wouldn't be very difficult for you. Just look sexy, which you already do, and boom, done.", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                });



        b13 = new MessageBasicWithAction("...", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
                );
        b14 = new MessageBasicWithAction("I know it's a lot to ask, but it would just be some nights, when we're understaffed! Please?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );
        b15 = new MessageBasicWithAction("... alright. But no sex stuff, OK?!", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );

        b16 = new MessageBasicWithAction("No, no, I promise! Just a little dance, you don't even need to take off your clothes. I mean, you can if you want to, but...", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );

        b17 = new MessageBasicWithAction("But not. I'll do it, but I'll keep my goods to me.", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );

        b18 = new MessageBasicWithAction("Thank you! You're the best.", NPCCode.MAIN_CHARACTER,
        delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
        }
    );

        b19 = new MessageBasicWithAction("Sure. Now, leave my office please. I've got work.", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
}
);
        b20 = new MessageBasicWithAction("Of course. Thank you again. Good night!", NPCCode.MAIN_CHARACTER,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
}
);

        b21 = new MessageBasicWithAction("'Night.", NPCCode.MAIN_CHARACTER,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
}
);

        end = new MessageEnd("Good job! You can now select Nicole for the day's work. Be aware that she will only accept to do basic dancing for now...", NPCCode.NARRATOR);

        begin.AddSuccessor(b2);
        b2.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b4.AddSuccessor(b5);
        b5.AddSuccessor(b6);
        b6.AddSuccessor(b7);
        b7.AddSuccessor(b8);
        b8.AddSuccessor(b9);
        b9.AddSuccessor(b10);
        b10.AddSuccessor(b11);
        b11.AddSuccessor(b12);
        b12.AddSuccessor(b13);
        b13.AddSuccessor(b14);
        b14.AddSuccessor(b15);
        b15.AddSuccessor(b16);
        b16.AddSuccessor(b17);
        b17.AddSuccessor(b18);
        b18.AddSuccessor(b19);
        b19.AddSuccessor(b20);
        b20.AddSuccessor(b21);
        b21.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_UNLOCK_BASIC_DANCE_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticDialogElements.dialogData.launchAssistantUnlockBasicDanceDialog)
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
        //************************************************************************************************************************//

        //********************************************************* Dance closer unlock dialog **********************************//
        begin = new MessageBasicWithAction("Every time Nicole has been working in the booths, it was a total success. Most clients leave completely happy. So you figured you'd ask her for a little bit more. You find her in her office, ready for what might be a difficult conversation...", NPCCode.NARRATOR,
    delegate ()
    {
        SetPortraitAndBackgroundForNextMessage(
           StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, true);
    }
    );
        b2 = new MessageBasicWithAction("Hello Nicole! How are you doing tonight ?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );
        b3 = new MessageBasicWithAction("Again? You should really change your ice breaker, you know. It's almost always the same...", NPCCode.ASSISTANT, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
        });

        b4 = new MessageBasicWithAction("What? I never paid attention to that...", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
              );
        b5 = new MessageBasicWithAction("Doesn't matter. So, what can I do for you?", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
                );
        b6 = new MessageBasicWithAction("Well, I wanted to tell you that you are rocking it in the booth. Clients are praising you like crazy!", NPCCode.MAIN_CHARACTER,
            delegate ()
            {
                SetPortraitAndBackgroundForNextMessage(
                    StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
            });


        b7 = new MessageBasicWithAction("Thanks? I'm not sure I should be happy about it though...", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                });
        b8 = new MessageBasicWithAction("Why not? It shows how good you are!", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                });

        b9 = new MessageBasicWithAction("Sure, but it might also incentivise them to ask for more... Which I'm not sure I want.", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                });



        b10 = new MessageBasicWithAction("You're not sure you want it? So that also means you're not sure that you don't want it, right?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
                );

        b11 = new MessageBasicWithAction("Maybe. While I'm not a big fan of dancing for these guys, I kinda like the way they look at me sometimes...", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
                );
        b12 = new MessageBasicWithAction("Good!", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                });



        b13 = new MessageBasicWithAction("Good? And why would that be \"good\" for you?", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
                );
        b14 = new MessageBasicWithAction("Oops... Well, I'm not gonna lie, I was also coming here to ask something of you regarding that little aspect of your work...", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );
        b15 = new MessageBasicWithAction("Oh, great. You want me to go further, don't you?", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );

        b16 = new MessageBasicWithAction("Damn, I forgot how perceptive you are... Yes. The client are truly thrilled by your work, and they want more. And I figured you might agree to it. ", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );

        b17 = new MessageBasicWithAction("I... I'm not sure. This isn't the kind of thing I wanted to do when I started this job. Sure I was expecting there would be some sexy stuff, but not that...", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );

        b18 = new MessageBasicWithAction("Come on, I'm sure it would allow you to check that feeling of yours. You know, maybe working closer with them would confirm it...", NPCCode.MAIN_CHARACTER,
        delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
        }
    );

        b19 = new MessageBasicWithAction("Fine. But just as an experiment for now. Just once, and I'll see how it goes. If I enjoy it I'll continue. If not, I'll stop.", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
}
);
        b20 = new MessageBasicWithAction("Very well! I'm sure you'll love it. A wonderful woman like you can only be worshipped by our clients, and I'm sure it'll please you. Thank you very much!", NPCCode.MAIN_CHARACTER,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
}
);

        b21 = new MessageBasicWithAction("Sure. Now, can you leave me alone a bit? I have things I need to think about...", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
}
);

        b22 = new MessageBasicWithAction("Of course. Think all you need. Good night!", NPCCode.MAIN_CHARACTER,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
    StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
}
);

        end = new MessageEnd("OK, you got her again. She'll now accept to dance closer to the clients. Good work!", NPCCode.NARRATOR);

        begin.AddSuccessor(b2);
        b2.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b4.AddSuccessor(b5);
        b5.AddSuccessor(b6);
        b6.AddSuccessor(b7);
        b7.AddSuccessor(b8);
        b8.AddSuccessor(b9);
        b9.AddSuccessor(b10);
        b10.AddSuccessor(b11);
        b11.AddSuccessor(b12);
        b12.AddSuccessor(b13);
        b13.AddSuccessor(b14);
        b14.AddSuccessor(b15);
        b15.AddSuccessor(b16);
        b16.AddSuccessor(b17);
        b17.AddSuccessor(b18);
        b18.AddSuccessor(b19);
        b19.AddSuccessor(b20);
        b20.AddSuccessor(b21);
        b21.AddSuccessor(b22);
        b22.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_UNLOCK_CLOSER_DANCE_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticDialogElements.dialogData.launchAssistantUnlockCloserDanceDialog)
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

        //********************************************************* Dance unlock dialog **********************************//
        begin = new MessageBasicWithAction("As expected, the clients are now asking more of Nicole. So, again, you go meet her hoping you'll be able to convince her to give them more...", NPCCode.NARRATOR,
    delegate ()
    {
        SetPortraitAndBackgroundForNextMessage(
           StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, true);
    }
    );
        b2 = new MessageBasicWithAction("Nicole! How's my favorite assistant doing?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );
        b3 = new MessageBasicWithAction("I see you changed your line. Good.", NPCCode.ASSISTANT, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
        });

        b4 = new MessageBasicWithAction("You were right, I always start the same, so I figured I'd change this time.", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
              );
        b5 = new MessageBasicWithAction("Good for you. So what is it this time?", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
                );
        b6 = new MessageBasicWithAction("Well, let's cut to the chase: the clients are loving what you do in the booth, and they want more. Again.", NPCCode.MAIN_CHARACTER,
            delegate ()
            {
                SetPortraitAndBackgroundForNextMessage(
                    StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
            });


        b7 = new MessageBasicWithAction("Really? Come on, can't these guys give me a break. So you want me to actually do more?", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                });
        b8 = new MessageBasicWithAction("Yes. Listen, they want to see you naked. Or at least partially so. You know, they fell in love with your chest and want to see it in all its glory...", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                });

        b9 = new MessageBasicWithAction("Yeah, I expected that...", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                });



        b10 = new MessageBasicWithAction("Is that so? How?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
                );

        b11 = new MessageBasicWithAction("Pretty simple really. Most of them couldn't get their eyes away from my boobs, it's like they're magnetic...", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
                );
        b12 = new MessageBasicWithAction("Well, I mean, they are. As we speak, I'm having trouble not looking at them.", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                });



        b13 = new MessageBasicWithAction("You too? Come on, control yourself already...", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
                );
        b14 = new MessageBasicWithAction("I am, but it's difficult... Anyway, what do you say? Will you do it?", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );
        b15 = new MessageBasicWithAction("Sure. Why not at this point. I'm already dancing on their laps, I think I can take my top off too...", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );

        b16 = new MessageBasicWithAction("Wonderful. You'll see, it's a liberating experience!", NPCCode.MAIN_CHARACTER,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );

        b17 = new MessageBasicWithAction("And how would you know that?", NPCCode.ASSISTANT,
                delegate ()
                {
                    SetPortraitAndBackgroundForNextMessage(
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
                }
            );

        b18 = new MessageBasicWithAction("Personal experience.", NPCCode.MAIN_CHARACTER,
        delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
        }
    );

        b19 = new MessageBasicWithAction("Really? You often expose your boobs to the world?", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
}
);
        b20 = new MessageBasicWithAction("Every monday, in front of the church, after my weekly sex change. You know, the normal stuff.", NPCCode.MAIN_CHARACTER,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
}
);

        b21 = new MessageBasicWithAction("Of course... But seriously, how do you know that?", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
}
);

        b22 = new MessageBasicWithAction("Pretty simple: that's what many girls working here told me. At first it's difficult and akward, but it soons become freeing. Don't ask me why, but that's what they said...", NPCCode.MAIN_CHARACTER,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
    StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
}
);

        b23 = new MessageBasicWithAction("OK, I buy it. But like last time, I'll try, and if I don't enjoy it, I'll stop. Deal?", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
    StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
}
);

        b24 = new MessageBasicWithAction("Perfect! You're the best!", NPCCode.MAIN_CHARACTER,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
}
);

        b25 = new MessageBasicWithAction("I know.", NPCCode.ASSISTANT,
delegate ()
{
    SetPortraitAndBackgroundForNextMessage(
    StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false, false);
}
);

        end = new MessageEnd("And that's done. Pretty good! She will now acept to dance topless during work!", NPCCode.NARRATOR);

        begin.AddSuccessor(b2);
        b2.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b4.AddSuccessor(b5);
        b5.AddSuccessor(b6);
        b6.AddSuccessor(b7);
        b7.AddSuccessor(b8);
        b8.AddSuccessor(b9);
        b9.AddSuccessor(b10);
        b10.AddSuccessor(b11);
        b11.AddSuccessor(b12);
        b12.AddSuccessor(b13);
        b13.AddSuccessor(b14);
        b14.AddSuccessor(b15);
        b15.AddSuccessor(b16);
        b16.AddSuccessor(b17);
        b17.AddSuccessor(b18);
        b18.AddSuccessor(b19);
        b19.AddSuccessor(b20);
        b20.AddSuccessor(b21);
        b21.AddSuccessor(b22);
        b22.AddSuccessor(b23);
        b23.AddSuccessor(b24);
        b24.AddSuccessor(b25);
        b25.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_UNLOCK_TOPLESS_DANCE_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticDialogElements.dialogData.launchAssistantUnlockToplessDanceDialog)
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

        //*************************************************************************************************************************************//

        //****************************************************** Nicolette arrival dialog redone ***********************************************/

        begin = new MessageBasicWithAction("Um, boss ? We may have a problem. Or maybe not? I'm not sure...", NPCCode.ASSISTANT,
            delegate () {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false);

            }
            );
        beginBis = new MessageBasicWithAction(" It seems that our recently acquired reputation attracted some potentially dangerous attention.", NPCCode.ASSISTANT,
            delegate () {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false);

            });
        b2 = new MessageBasicWithAction("To be clear, we have an unsavory visitor down in the parking lot. She asked to talk to you. I have no idea what she wants but I have a feeling it's not good. Let me take you there.", NPCCode.ASSISTANT,
            delegate () {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_UNDERGROUND_CARPARK, false,
    true, StaticStrings.CRIMINALS_DIRECTORY + StaticStrings.CRIMINALS_NICOLETTE_SHEA_DIRECTORY + "portrait_1_no_background");

            });
        b3 = new MessageWithResponses("Hmmmph hmphmmh hmmphph, mmmph phmmphm, mmphp !", NPCCode.CRIMINAL_NICOLETTE_SHEA, new List<int> { 0, 1 },
            new List<string> { "Um, could you take off that bandana? I can't understand a word of what you're saying...",
                "Nice tits! You might want to put on some looser clothes in the future though. Not that I complain." },
            delegate (int i)
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_UNDERGROUND_CARPARK, false,
                    true, StaticStrings.CRIMINALS_DIRECTORY + StaticStrings.CRIMINALS_NICOLETTE_SHEA_DIRECTORY + "portrait_2_no_background");

                StaticDialogElements.dialogData.nicoletteLargePortraitIndex = 1;
            });
        b4 = new MessageBasicWithAction("Sorry, I always forget I can't really talk with this on my face. The guys insist that I keep it on, they say it's \"standard uniform\". Bunch of liars, I don't see them wearing it...", NPCCode.CRIMINAL_NICOLETTE_SHEA,
            delegate () {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_UNDERGROUND_CARPARK, false,
    true, StaticStrings.CRIMINALS_DIRECTORY + StaticStrings.CRIMINALS_NICOLETTE_SHEA_DIRECTORY + "portrait_2_no_background");

            });
        b5 = new MessageBasicWithAction("Very funny! I forgot the damn thing in the dryer and didn't have time to buy another one. Plus the guys seem to like it that way, so...", NPCCode.CRIMINAL_NICOLETTE_SHEA,
            delegate () {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_UNDERGROUND_CARPARK, false,
    true, StaticStrings.CRIMINALS_DIRECTORY + StaticStrings.CRIMINALS_NICOLETTE_SHEA_DIRECTORY + "portrait_2_no_background");

            });
        b6 = new MessageBasic("Anyways, I was saying that I'm here to give you a message and an offer from " + StaticStrings.CRIMINALS_MAFIA_BOSS_TITLE + " " + StaticStrings.CRIMINALS_MAFIA_BOSS_NAME + ". He saw how well you're doing and he wants to greet you in the neighbourhood and offer our organization's services. Don't worry, no \"protection\" tax.", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b7 = new MessageBasic("He's giving you access to our local \"office\". In there, you'll be able to rent some of our services. But you'll need to earn our trust for each of them, so you should treat our guys coming here well. They better be getting out with a giant smile on their face and empty balls.", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b8 = new MessageBasic("Mostly the second part though, I'm seriously tired of doing it myself. There are clearly not enough women in the mob...", NPCCode.CRIMINAL_NICOLETTE_SHEA);
        b9 = new MessageBasicWithAction("Anyway, it was a pleasure. Don't forget to come visit us. Oh, by the way, the name's Nicolette Shea. Nice to meet you.", NPCCode.CRIMINAL_NICOLETTE_SHEA, delegate () { StaticDialogElements.dialogData.nicoletteNameKnown = true; });
        b10 = new MessageEnd("And seriously, be sure that your girls work their magic on the guys. My hands are hurting. And my legs. And my mouth. And my ass. And pretty much anything they can use... It's not the worst activity, but it gets tiring, after a while.", NPCCode.CRIMINAL_NICOLETTE_SHEA);

        begin.AddSuccessor(beginBis);
        beginBis.AddSuccessor(b2);
        b2.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b3.AddSuccessor(b5);
        b4.AddSuccessor(b6);
        b5.AddSuccessor(b6);
        b6.AddSuccessor(b7);
        b7.AddSuccessor(b8);
        b8.AddSuccessor(b9);
        b9.AddSuccessor(b10);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.FOURTH_MISION_MIDDLE_DIALOG_ID, begin, delegate ()
        {
            //True if the fourth mission is done
            if (StaticFunctions.FindMissionWithID(GMGlobalNumericVariables.gnv.FOURTH_MISSION_ID).IsDone())
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false);

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
        }

        ));



        //**********************************************************************************************************************************//

        begin = new MessageBasic("So, she's dancing now. Going topless and all. Now, obviously you gotta get her to do more. You know like the other girls. Maybe some full nakedness would help. Good idea, right? Ok then, good luck.", NPCCode.NARRATOR);

        beginBis = new MessageBasicWithAction("Therefore, armed with some good enough arguments, you go to see her in her office.", NPCCode.NARRATOR,
            delegate () {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false);

            });
        b2 = new MessageBasic("Hello Nicole! Good day to you!", NPCCode.MAIN_CHARACTER);

        b3 = new MessageBasic("Hello boss. Good day to you too I guess?", NPCCode.ASSISTANT);

        b4 = new MessageBasic("How are you doing in this fine day?", NPCCode.MAIN_CHARACTER);

        b5 = new MessageBasic("Yeah you should totally keep on with the over the top intro. This will clearly help you and not raise suspicion at all...", NPCCode.NARRATOR);

        b6 = new MessageBasic("Good? Why the over the top attitude?", NPCCode.ASSISTANT);

        b7 = new MessageBasic("Told you.", NPCCode.NARRATOR);

        b8 = new MessageBasic("Hrm! I'm here to present you with a humble request, one that the people, the almighty and powerful people asked me to present to your never ending wisdom.", NPCCode.MAIN_CHARACTER);

        b9 = new MessageBasic("I feel like I should be afraid. What is it?", NPCCode.ASSISTANT);

        b10 = new MessageBasic("Boobs. 'Tis about boobs. And the rest too.", NPCCode.MAIN_CHARACTER);

        b11 = new MessageBasic("Given our field of work, I could have guessed. And more precisely?", NPCCode.ASSISTANT);

        b12 = new MessageBasic("'Tis about your boobs. And your \"rest\" too.", NPCCode.MAIN_CHARACTER);

        b13 = new MessageBasic("Oh. Does it mean what I think it means?", NPCCode.ASSISTANT);

        b14 = new MessageBasic("If what you think is that I am here to ask you to get completely naked in the booth and show off to our clients, then you would be right, I'm afraid.", NPCCode.MAIN_CHARACTER);

        b15 = new MessageBasic("As I feared. Listen, it's starting to be a bit much. I'm slowly reaching the end of my comfort zone here. And honestly it already has been stretched by a lot.", NPCCode.ASSISTANT);

        b16 = new MessageBasic("I understand that. But look, you're already showing yourself topless. Why not go fully nude? Would showing those extra bits make that much of a difference? Think about it.", NPCCode.MAIN_CHARACTER);

        b17 = new MessageBasic("...", NPCCode.ASSISTANT);

        b18 = new MessageBasic("Plus, the clients would really love that. They already are captivated by you, now they could be mesmerized by your beauty.", NPCCode.MAIN_CHARACTER);

        b19 = new MessageBasic("...", NPCCode.ASSISTANT);

        b20 = new MessageBasic("Please? For me? I would also love it.", NPCCode.MAIN_CHARACTER);

        b21 = new MessageBasic("Alright. I guess I could do it. Plus with the clothes I usually wear in the booth I might as well get naked...", NPCCode.ASSISTANT);

        b22 = new MessageBasic("Nice! Thank you very much!", NPCCode.MAIN_CHARACTER);

        b23 = new MessageBasic("Yeah, yeah, you're welcome... ", NPCCode.ASSISTANT);

        b24 = new MessageBasic("Good night! You're the greatest assistant I could have wished for!", NPCCode.MAIN_CHARACTER);

        b25 = new MessageBasic("Sure. Keep the sweet talk and you might even convince me to do more...", NPCCode.ASSISTANT);

        b26 = new MessageBasic("For real?", NPCCode.MAIN_CHARACTER);

        b27 = new MessageBasic("We'll see. Goodbye!", NPCCode.ASSISTANT);

        b28 = new MessageEnd("OK, I gotta say, that's not bad. Though the intro wasn't that good, the rest was top notch. Good job! Now go enjoy her work!", NPCCode.NARRATOR);

        begin.AddSuccessor(beginBis);
        beginBis.AddSuccessor(b2);
        b2.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b4.AddSuccessor(b5);
        b5.AddSuccessor(b6);
        b6.AddSuccessor(b7);
        b7.AddSuccessor(b8);
        b8.AddSuccessor(b9);
        b9.AddSuccessor(b10);
        b10.AddSuccessor(b11);
        b11.AddSuccessor(b12);
        b12.AddSuccessor(b13);
        b13.AddSuccessor(b14);
        b14.AddSuccessor(b15);
        b15.AddSuccessor(b16);
        b16.AddSuccessor(b17);
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

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_UNLOCK_POSE_NAKED_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticDialogElements.dialogData.launchAssistantUnlockPoseNakedDialog)
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

        /*******************************************************************************************************************/

        begin = new MessageBasic("OK, you got her to go full nude. Great! But now, now the fun part starts. We want her to start doing something. But we should go slowly. Don't frighten her. Slow and steady wins the race, and all that jazz. Now go, big guy!", NPCCode.NARRATOR);

        beginBis = new MessageBasicWithAction("Oh, yes. You go to her office of course. I thought you would know the story by now.", NPCCode.NARRATOR,
            delegate () {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false);

            });
        b2 = new MessageBasic("<i>Bonjour</i>  Nicole!How are you in this fine day ?", NPCCode.MAIN_CHARACTER);

        b3 = new MessageBasic("Hey boss. You're speaking french now?", NPCCode.ASSISTANT);

        b4 = new MessageBasic("Sort of. Kinda. Well, I know one word. Well, two in fact.", NPCCode.MAIN_CHARACTER);

        b6 = new MessageBasic("And what would they be? <i>Bonjour</i>  and what else?", NPCCode.ASSISTANT);

        b8 = new MessageBasic("<i>Baguette</i>.", NPCCode.MAIN_CHARACTER);

        b9 = new MessageBasic("I should have known... So, quickly moving on, what can I do for you?", NPCCode.ASSISTANT);

        b10 = new MessageBasic("For me, nothing. I've got a few dozen clients that would love for you to do something for them however...", NPCCode.MAIN_CHARACTER);

        b11 = new MessageBasic("What!? I already told you, I'm not a whore! I won't do that sort of thing!", NPCCode.ASSISTANT);

        b12 = new MessageBasic("No, no, no, you misunderstood me! That's not what I meant!", NPCCode.MAIN_CHARACTER);

        b5 = new MessageBasic("For now, at least...", NPCCode.NARRATOR);

        b13 = new MessageBasic("It's just that all of them asked me if you could just, you know, give them a \"deeper\" show. By, hrm, touching yourself a little...", NPCCode.MAIN_CHARACTER);

        b14 = new MessageBasic("And how is that any better than having sex with them!?", NPCCode.ASSISTANT);

        b15 = new MessageBasic("Well, hrm, good question. No penetration, no contact, no fluids inside and over you? ", NPCCode.MAIN_CHARACTER);

        b17 = new MessageBasic("...", NPCCode.ASSISTANT);

        b18 = new MessageBasic("And, you know, a relaxing and exciting moment for you. Little bit of fun, little bit of pleasure.", NPCCode.MAIN_CHARACTER);

        b19 = new MessageBasic("...", NPCCode.ASSISTANT);

        b20 = new MessageBasic("And a big help to the club. At this point, it seems like you're the most demanded woman amongst our clients...", NPCCode.MAIN_CHARACTER);

        b21 = new MessageBasic("Really?", NPCCode.ASSISTANT);

        b22 = new MessageBasic("Well, I don't have the exact numbers, but that's what it feels like to me anyway.", NPCCode.MAIN_CHARACTER);

        b23 = new MessageBasic("Then maybe... maybe I'll think about it. Next time I'm in the booth I'll give it a try...", NPCCode.ASSISTANT);

        b24 = new MessageBasic("Really?", NPCCode.MAIN_CHARACTER);

        b25 = new MessageBasic("Yes. Close your mouth, you'll end up drooling on the floor if you keep on like that.", NPCCode.ASSISTANT);

        b26 = new MessageBasic("Wow. I'll be honest with you here, I didn't expect you to actually say yes.", NPCCode.MAIN_CHARACTER);

        b27 = new MessageBasic("Well, maybe you're a good talker. Or you don't know me as well as you thought. Pick your favorite explanation.", NPCCode.ASSISTANT);

        b28 = new MessageBasic("I'll go with being very convincing. Better for my ego. Thank you!", NPCCode.MAIN_CHARACTER);

        end = new MessageEnd("And with that, you got her touching herself. You're getting closer to having her \"interacting\" with the clients! Keep up the good work!", NPCCode.NARRATOR);

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
        b28.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_UNLOCK_SOLO_FINGERING_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticDialogElements.dialogData.launchAssistantUnlockSoloFingeringDialog)
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

        /*******************************************************************************************************************/

        begin = new MessageBasic("Just a little nudge! She needs just a little nudge for the next step. And then, then, finally, she will start using toys! Yes, I know, a bit underwhelming... But, if you keep up like that, she will start actually touching the clients. And that's the goal here. Don't you ever lose your focus on that! Go!", NPCCode.NARRATOR);

        beginBis = new MessageBasicWithAction("Go, I said! Who cares where this all happens! Get fired up and convince her!", NPCCode.NARRATOR,
            delegate () {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false);

            });
        b2 = new MessageBasic("<i>Guten Tag</i> Nicole!", NPCCode.MAIN_CHARACTER);

        b3 = new MessageBasic("German now? What, you're starting to be at a loss for greetings? Do you intend to greet me in all possible languages? And do you even speak german, or is it just a word or two again?", NPCCode.ASSISTANT);

        b4 = new MessageBasic("In order: Yes, yes, I'm thinking about it, yes.", NPCCode.MAIN_CHARACTER);

        b6 = new MessageBasic("... Give me a minute here. \"Yes\" ... \"yes\" ... \"I'm thinking about it\" and \"yes\".Got it. I'm guessing I should ask you what's the other german word you know?", NPCCode.ASSISTANT);

        b8 = new MessageBasic("<i>Kartoffeln</i>...", NPCCode.MAIN_CHARACTER);

        b9 = new MessageBasic("Which means?", NPCCode.ASSISTANT);

        b10 = new MessageBasic("Potato...", NPCCode.MAIN_CHARACTER);

        b11 = new MessageBasic("I would like to sincerely apologize to any german player. Or french. And I would also like to prematurely apologize to any other non English speaker. Hopefully, this kind of joke will stop in the future, but you never know...", NPCCode.NARRATOR);

        b12 = new MessageBasic("Just stop... What can I do for you?", NPCCode.ASSISTANT);

        b5 = new MessageBasic("OK, so, you know how you accepted to actually touch yourself in the booth?", NPCCode.MAIN_CHARACTER);

        b13 = new MessageBasic("Yes? This is oddly pleasant by the way. Thanks for convincing me to do it.", NPCCode.ASSISTANT);

        b14 = new MessageBasic("You're welcome. So now, I wanted to ask you to... Wait. You really enjoy it?", NPCCode.MAIN_CHARACTER);

        b15 = new MessageBasic("Yeah. I enjoy more and more having people lusting over me, and actually masturbating helps me to release the tension I feel in there.", NPCCode.ASSISTANT);

        b17 = new MessageBasic("Perfect! So you would be OK with using some non-organic help with that?", NPCCode.MAIN_CHARACTER);

        b18 = new MessageBasic("What?", NPCCode.ASSISTANT);

        b19 = new MessageBasic("Toys. I'm speaking about toys.", NPCCode.MAIN_CHARACTER);

        b20 = new MessageBasic("At this point I'm willing to try. But only if I can pick the ones I want, don't have to pay for them and that they are clean.", NPCCode.ASSISTANT);

        b21 = new MessageBasic("Of course! Look into the room with the dildos painted on the door and pick your favorites. And don't worry about hygiene, cause even if the whole club is  thoroughly cleaned, the toys are even cleaner. At this point, the only way to make them cleaner would be to plunge them in a bath of acid...", NPCCode.MAIN_CHARACTER);

        b22 = new MessageBasic("Yeah I know. Might I remind you that I'm the one who fills the papers to pay the cleaners and to maintain the building in working condition? I don't remember ordering to paint a dildo on a door though...", NPCCode.ASSISTANT);

        b23 = new MessageBasic("Personal touch. People find it funnny and it clearly indicates the purpose of the place.", NPCCode.MAIN_CHARACTER);

        b24 = new MessageBasic("... Sure. You know you're weird sometimes?", NPCCode.ASSISTANT);

        b25 = new MessageBasic("Really?", NPCCode.MAIN_CHARACTER);

        b26 = new MessageBasic("Yes. But funny weird, not creepy weird. But that doesn't mean you should paint more dildos on the doors, OK?", NPCCode.ASSISTANT);

        b27 = new MessageBasic("Aye, aye, Madam.", NPCCode.MAIN_CHARACTER);

        end = new MessageEnd("You passed with flying colors. Great job. But a dildo-painted door, really? That's a bit over the top...", NPCCode.NARRATOR);

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
        b27.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_UNLOCK_TOYS_MASTURBATION_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticDialogElements.dialogData.launchAssistantUnlockToysMasturbationDialog)
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

        /*******************************************************************************************************************/

        begin = new MessageBasicWithAction("You've been going to the same café with Nicole a few times already and it would be good to go to a new place. Luckily, she seems like a sporty type, so you figure she would enjoy to go for a tennis match with you.", NPCCode.NARRATOR,
            delegate ()
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE, false);
            }
            );

        b2 = new MessageBasic("Hey Nicole! Ready for today's date?", NPCCode.MAIN_CHARACTER);

        b3 = new MessageBasic("Hello boss. Yes, I finished my work a few minutes ago.", NPCCode.ASSISTANT);

        b4 = new MessageBasic("I see you're starting to anticipate my arrival. You even manage to stop working for a moment!", NPCCode.MAIN_CHARACTER);

        b6 = new MessageBasic("Well, you always come at the same time and on the same days, so it's easy to know when you will. Plus, I'm having a good time in those little dates with you, so I tend to remember when you come to ask me for one.", NPCCode.ASSISTANT);

        b8 = new MessageBasic("Soooo, you're having a good time eh? That's nice to hear.", NPCCode.MAIN_CHARACTER);

        b9 = new MessageBasic("Don't get too cocky about it. There are still a lot of things you could improve upon...", NPCCode.ASSISTANT);

        b10 = new MessageBasic("Oh? And what would that be?", NPCCode.MAIN_CHARACTER);

        b11 = new MessageBasic("For starter, maybe try to look in my eyes more than you look at my breasts...", NPCCode.ASSISTANT);

        b12 = new MessageBasic("Well, I'm trying... But as I told you, it's pretty difficult. They feel like a gaze-magnet. If I lose my focus for half a second I can feel my eyes moving toward that wonderful goal.", NPCCode.MAIN_CHARACTER);

        b5 = new MessageBasic("I've noticed. And that's why I tolerate it, since I can see that you're trying, at least. It's more than most men I know, so I'll take it. But keep working on it!", NPCCode.ASSISTANT);

        b13 = new MessageBasic("Aye, aye Madam! Wouldn't want to displease you!", NPCCode.MAIN_CHARACTER);

        b14 = new MessageBasic("Thank you. So, are we going then?", NPCCode.ASSISTANT);

        b15 = new MessageBasic("Yes. But this time, we're going someplace else. The café is nice, but I'm thinking that something outside would be a good idea for once.", NPCCode.MAIN_CHARACTER);

        b17 = new MessageBasic("What do you have in mind ?", NPCCode.ASSISTANT);

        b18 = new MessageBasic("Well, it depends. What do you think about tennis?", NPCCode.MAIN_CHARACTER);

        b19 = new MessageBasic("Tennis? I played it once or twice. I'm actually OK at it.", NPCCode.ASSISTANT);

        b20 = new MessageBasic("In that case, I'm taking you to a court. What do you think about a match?", NPCCode.MAIN_CHARACTER);

        b21 = new MessageBasic("Sure, I'll take you on. Ready to get crushed?", NPCCode.ASSISTANT);

        b22 = new MessageBasic("I was born ready. Wait, that's wrong...", NPCCode.MAIN_CHARACTER);

        b23 = new MessageBasic("I concur, you were born ready to lose against me. Let's go!", NPCCode.ASSISTANT);

        b24 = new MessageBasic("After you, milady.", NPCCode.MAIN_CHARACTER);

        b25 = new MessageBasic("Obviously...", NPCCode.ASSISTANT);

        b26 = new MessageBasic("You know, I believe this cockiness would deserve a pinch to the butt.", NPCCode.MAIN_CHARACTER);

        b27 = new MessageBasic("Well, I might let you give me one if you beat me...", NPCCode.ASSISTANT);

        end = new MessageEnd("And off you go. To the great wide world. Or to the tennis court to be precise... Lucky you, you earned the right to see her in her tennis outift!", NPCCode.NARRATOR);

        begin.AddSuccessor(b2);
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
        b27.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_UNLOCK_TENNIS_DATE_DIALOG_ID, begin,
        delegate ()
        {
            if (RegisterDates.dates.Find(x => x.directoryName == StaticStrings.DATE_TENNIS_NAME).bought)
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

        /*******************************************************************************************************************/

        begin = new MessageBasicWithAction("Another day, another successful date. Or not. Who cares! Something important is happening, so focus!", NPCCode.NARRATOR, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, false, true,
                StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                                + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE
                );
        });

        b3 = new MessageBasic("So that's how I ended up half-naked on top of a fountain in the middle of a park. Weird day...", NPCCode.ASSISTANT);

        b4 = new MessageBasic("I'd say good day! Hopefully it wasn't too cold though...", NPCCode.MAIN_CHARACTER);

        b6 = new MessageBasic("It was in the middle of summer so no, it wasn't. It was rather hot, to be honest.", NPCCode.ASSISTANT);

        b8 = new MessageBasic("Oh? Was it the summer day or the \"naked in public\" part?", NPCCode.MAIN_CHARACTER);

        b9 = new MessageBasic("Who knows? I can't remember...", NPCCode.ASSISTANT);

        b10 = new MessageBasic("Hot indeed. I would have loved to be there, you know, enjoying the night...", NPCCode.MAIN_CHARACTER);

        b11 = new MessageBasic("Sure, \"enjoying the night\"... Say you know what? I think I can show you. Would you be interested?", NPCCode.ASSISTANT);

        b12 = new MessageBasic("Seriously? For real? Am I in a dream right now? Have you been abducted and replaced with a daring alien robot?", NPCCode.MAIN_CHARACTER);

        b5 = new MessageBasic("Maybe... Do you care though? You're gonna enjoy my little reliving of this night anyway.", NPCCode.ASSISTANT);

        b13 = new MessageBasic("Ever the rational one, aren't you? Alright then, my pleasure!", NPCCode.MAIN_CHARACTER);

        b14 = new MessageBasic("Buuut, not in public.", NPCCode.ASSISTANT);

        b15 = new MessageBasic("Oh, too bad... Wait, do you mean what I think you mean?", NPCCode.MAIN_CHARACTER);

        b17 = new MessageBasic("Maybe. Follow me!", NPCCode.ASSISTANT);

        b18 = new MessageBasicWithAction("She then proceeds to guide you to a lovely appartment she seems to be owning.", NPCCode.NARRATOR, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, false, true,
                StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                                + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE);
        });

        b19 = new MessageBasic("Welcome to my humble abode! Come in!", NPCCode.ASSISTANT);

        b20 = new MessageBasicWithAction("Have a seat on the couch, I'll be right back.", NPCCode.ASSISTANT,
            delegate ()
            {
                SetPortraitAndBackgroundForNextMessage(
                    StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, true);
            });

        end = new MessageEnd("You seat in the couch and start waiting. Luckily, it's not very long before she comes back, dressed in a lovely outfit and starts putting on a show. Enjoy!", NPCCode.NARRATOR);

        begin.AddSuccessor(b3);
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
        b20.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_FIRST_VIDEO_UNLOCK_DIALOG_ID, begin,
        delegate ()
        {
            if (RegisterDates.dates.Find(x => x.id == GMGlobalNumericVariables.gnv.CAFE_DATE_ID).videoLevel == 1)
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, true);

                return true;
            }
            return false;
        },
        delegate ()
        {

            DialogSceneBehavior.pathToClip = StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_VIDEOS_FOLDER + StaticStrings.ASSISTANT_FOLDER_VIDEOS_DATES + StaticStrings.ASSISTANT_FOLDER_VIDEOS_DATES_BAR + "Bar 1";
            DialogSceneBehavior.startVideo = true;
            DialogSceneBehavior.startingSceneName = StaticStrings.CLUB_SCENE;
            DialogSceneBehavior.pathToImageBackground = StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY + StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT;

            //Display the loading screen
            GameObject.FindGameObjectWithTag("LoadingScreen").GetComponentInChildren<Image>(true).enabled = true;

            SceneManager.LoadScene(StaticStrings.DIALOG_SCENE);

            StaticDialogElements.specialBackground = "";
            StaticDialogElements.dialogSpecialPortraitIsOn = false;
            StaticDialogElements.specialPortraitSprite = null;
            StaticDialogElements.dialogSpecialBackgroundIsOn = false;
            StaticDialogElements.backgroundImage = null;


        }
        ));

        /*******************************************************************************************************************/

        begin = new MessageBasicWithAction("Once she is done, she goes back to her room and changes into her usual clothes. You stay on the couch, looking in the air in a completely shocked state.", NPCCode.NARRATOR,
             delegate ()
             {
                 SetPortraitAndBackgroundForNextMessage(
                     StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, false, true,
                StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                                + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE
                     );
             });

        b3 = new MessageBasic("So, what do you think?", NPCCode.ASSISTANT);

        b4 = new MessageBasic("...", NPCCode.MAIN_CHARACTER);

        b6 = new MessageBasic("Hello? Anyone home?", NPCCode.ASSISTANT);

        b8 = new MessageBasic("...", NPCCode.MAIN_CHARACTER);

        b9 = new MessageBasic("You might want to do something, I think your nose is bleeding a little.", NPCCode.ASSISTANT);

        b10 = new MessageBasic("What? Oh yes, nosebleed. Don't worry, it's probably just that my brain melted while seeing that. Damn, that was awesome.", NPCCode.MAIN_CHARACTER);

        b11 = new MessageBasic("Thanks! But I'm surprised to see there is still blood in your nose, given how much went in the bottom part of you...", NPCCode.ASSISTANT);

        b12 = new MessageBasic("I mean, there's barely anything left. But enough to get out through my nose, apparently. Now, if you don't mind, I'm gonna need a rest. Unless you want to help me with my bottom part situation.", NPCCode.MAIN_CHARACTER);

        b5 = new MessageBasic("Not tonight. You've got enough for the day I'd say. Maybe next time, boss.", NPCCode.ASSISTANT);

        b13 = new MessageBasic("OK, no complaint here. Now, to get up without tripping on my own feet.", NPCCode.MAIN_CHARACTER);

        b14 = new MessageBasic("You stand up, stumble a little bit, and miraculously manage to reach the door without falling on your face.", NPCCode.NARRATOR);

        b15 = new MessageBasic("See you next time. Good night Nicole.", NPCCode.MAIN_CHARACTER);

        end = new MessageEnd("Good night boss.", NPCCode.ASSISTANT);

        begin.AddSuccessor(b3);
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
        b15.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_FIRST_VIDEO_UNLOCK_PART_2_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_FIRST_VIDEO_UNLOCK_DIALOG_ID).done)
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, true);

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

        /*******************************************************************************************************************/
        /************************************************ First video unlocked ***************************************************/

        begin = new MessageBasicWithAction("Congratulations! You reached max level for one of Nicole's date outfit!", NPCCode.NARRATOR,
             delegate ()
             {
                 SetPortraitAndBackgroundForNextMessage(
                     StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_TUTORIAL_2, false
                     );
             });

        b2 = new MessageBasic("But, good news, this is not the end! It's only the begining of the good stuff! From now on, instead of dress tokens, there will be a new special one, the one currently being displayed.", NPCCode.NARRATOR);

        b3 = new MessageBasic("This one will unlock special scenes with Nicole, now that she's starting to really like you!", NPCCode.NARRATOR);

        b4 = new MessageBasic("Each new scene will need more tokens to unlock. The first one will start after collecting one, the second one will take two more, the third one three more... The total number of tokens collected is kept between dates of the same type.", NPCCode.NARRATOR);

        b5 = new MessageBasic("Additionally, this token will only appear if you manage to increase the current date's score by " + GMGlobalNumericVariables.gnv.DATE_GAME_SCORE_STREAK_TO_REACH_FOR_VIDEO_TOKEN + " without looking at her breasts. Each time you manage that, this new token will appear, up to the current number of tokens you need to unlock the next video.", NPCCode.NARRATOR);

        end = new MessageEnd("Now, go on your next date and start exploring this new and exciting relationship with her!", NPCCode.NARRATOR);

        begin.AddSuccessor(b2);
        b2.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b4.AddSuccessor(b5);
        b5.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_DATE_VIDEO_TOKEN_AVAILABLE_DIALOG_ID, begin,
        delegate ()
        {
            foreach (DateData date in RegisterDates.dates)
            {
                if (date.currentPortraitLevel >= date.maxPortraitLevel - 1)
                {
                    SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_MAIN_CLUB, true);

                    return true;
                }
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


        /*******************************************************************************************************************/

        begin = new MessageBasic("You are once again enjoying a fine date with Nicole, talking and laughing together, when suddenly...", NPCCode.NARRATOR);

        b3 = new MessageBasic("Absolutely nothing of interest happens. I mean, you're just talking. In a bar. With Nicole. At this point, you've done it enough times to know how it goes...", NPCCode.NARRATOR);

        b4 = new MessageBasicWithAction("And so it ends. But fear not! There is indeed something interesting happening right at the end of the date. After all,you didn't collect all those tokens for nothing!", NPCCode.NARRATOR, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, false, true,
                StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                                + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE
                );
        }
        );

        b6 = new MessageBasic("Whew, that was a good night. Thanks again for this. You're really good at this.", NPCCode.ASSISTANT);

        b8 = new MessageBasic("I try...", NPCCode.MAIN_CHARACTER);

        b9 = new MessageBasic("Wow, a semblance of modesty? Did something happen to you?", NPCCode.ASSISTANT);

        b10 = new MessageBasic("Oh, sorry. I mean \"I know, I'm the best at it.\". Is that better?", NPCCode.MAIN_CHARACTER);

        b11 = new MessageBasic("Haha, yes. More like you anyway.", NPCCode.ASSISTANT);

        b12 = new MessageBasic("Well, if that's who I am, what do you think about going to your home again and redoing your little trip to memory lane of the last time?", NPCCode.MAIN_CHARACTER);

        b5 = new MessageBasic("Oh? You sure would enjoy that, wouldn't you?", NPCCode.ASSISTANT);

        b13 = new MessageBasic("Me? Maybe. But, you know, I ask this for you, since you had so much fun last time...", NPCCode.MAIN_CHARACTER);

        b14 = new MessageBasic("Did I now? Well, maybe I did. Tell you what, we might do that again. Or maybe, I could do a little more for you...", NPCCode.ASSISTANT);

        b15 = new MessageBasic("Oooh. Me likes it. What will it be?", NPCCode.MAIN_CHARACTER);

        b17 = new MessageBasic("Be patient, you'll see. Let's go to my appartment first!", NPCCode.ASSISTANT);

        b18 = new MessageBasicWithAction("I can't wait!", NPCCode.MAIN_CHARACTER, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, true);
        });

        b19 = new MessageBasicWithAction("You both walk together to her home, lightly talking and flirting all the way.", NPCCode.NARRATOR, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, false, true,
                StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                                + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE);
        });

        b20 = new MessageBasic("OK, you know the drill. Sit down, relax, I'll be right back.", NPCCode.ASSISTANT);

        b21 = new MessageBasic("Wait, you're not gonna tell me what you're going to do?", NPCCode.MAIN_CHARACTER);

        b22 = new MessageBasic("Nope! You'll have to wait and hope!", NPCCode.ASSISTANT);

        b23 = new MessageBasicWithAction("I can do that. I can definitely do that!", NPCCode.MAIN_CHARACTER, delegate()
        {
            SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, true);
        });

        end = new MessageEnd("You seat in the couch and start waiting. Luckily, it's not very long before she comes back, but this time dressed with a cute little choker. And nothing else. A great moment in perspective!", NPCCode.NARRATOR);

        begin.AddSuccessor(b3);
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
        b23.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_SECOND_VIDEO_UNLOCK_DIALOG_ID, begin,
        delegate ()
        {
            if (RegisterDates.dates.Find(x => x.id == GMGlobalNumericVariables.gnv.CAFE_DATE_ID).videoLevel == 2)
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, true);

                return true;
            }
            return false;
        },
        delegate ()
        {

            DialogSceneBehavior.pathToClip = StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_VIDEOS_FOLDER + StaticStrings.ASSISTANT_FOLDER_VIDEOS_DATES + StaticStrings.ASSISTANT_FOLDER_VIDEOS_DATES_BAR + "Bar 2";
            DialogSceneBehavior.startVideo = true;
            DialogSceneBehavior.startingSceneName = StaticStrings.CLUB_SCENE;
            DialogSceneBehavior.pathToImageBackground = StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY + StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT;

            //Display the loading screen
            GameObject.FindGameObjectWithTag("LoadingScreen").GetComponentInChildren<Image>(true).enabled = true;

            SceneManager.LoadScene(StaticStrings.DIALOG_SCENE);

            StaticDialogElements.specialBackground = "";
            StaticDialogElements.dialogSpecialPortraitIsOn = false;
            StaticDialogElements.specialPortraitSprite = null;
            StaticDialogElements.dialogSpecialBackgroundIsOn = false;
            StaticDialogElements.backgroundImage = null;


        }
        ));

        /*******************************************************************************************************************/

        begin = new MessageBasicWithAction("Once she is done, she goes back to her room and changes into her usual clothes. You stay on the couch, looking in the air in a completely shocked state. Again. You're not very resistant to this sort of thing, for a sex club owner...", NPCCode.NARRATOR,
             delegate ()
             {
                 SetPortraitAndBackgroundForNextMessage(
                     StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, false, true,
                StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                                + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE
                     );
             });

        b3 = new MessageBasic("Sooo? Did you like the surprise?", NPCCode.ASSISTANT);

        b4 = new MessageBasic("...", NPCCode.MAIN_CHARACTER);

        b6 = new MessageBasic("Again? Come on, wake up! ", NPCCode.ASSISTANT);

        b8 = new MessageBasic("Just kidding. I managed to stay conscious this time. Barely though.", NPCCode.MAIN_CHARACTER);

        b9 = new MessageBasic("Am I that good?", NPCCode.ASSISTANT);

        b10 = new MessageBasic("Better. I think. It's all a blur, really. Maybe you could show me again, just to check?", NPCCode.MAIN_CHARACTER);

        b11 = new MessageBasic("Haha, nice try! But no, I wouldn't want to spoil you!", NPCCode.ASSISTANT);

        b12 = new MessageBasic("Damn! Another time maybe?", NPCCode.MAIN_CHARACTER);

        b5 = new MessageBasic("We'll see...", NPCCode.ASSISTANT);

        b13 = new MessageBasic("Oooh, I can't wait!", NPCCode.MAIN_CHARACTER);

        b14 = new MessageBasic("You stay seated a few minutes, still talking to her, and you realize that it's time to go back home.", NPCCode.NARRATOR);

        b15 = new MessageBasic("OK, time for me to leave. Thanks again for today. That was awesome.", NPCCode.MAIN_CHARACTER);

        end = new MessageEnd("Thanks to you. Good night!", NPCCode.ASSISTANT);

        begin.AddSuccessor(b3);
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
        b15.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_SECOND_VIDEO_UNLOCK_PART_2_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_SECOND_VIDEO_UNLOCK_DIALOG_ID).done)
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, true);

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

        /****************************************Bar date third video unlock part 1********************************************************************/

        begin = new MessageBasic("Another date in the café. As usual, you both are having fun, talking and flirting together. But, this time, the flirting is a little bit more intense. Maybe because of what happened last time, or maybe something else, but you both feel the slight sexual tension in the air. ", NPCCode.NARRATOR);

        b3 = new MessageBasicWithAction("At least you do. And rather than in the air, it would be in your pants. But hey, there is tension here. A noticeable one...", NPCCode.NARRATOR, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, false, true,
                StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                                + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE
                );
        });

        b4 = new MessageBasic("You know, you could at least try to hide it...", NPCCode.ASSISTANT);

        b6 = new MessageBasic("What?", NPCCode.MAIN_CHARACTER);

        b8 = new MessageBasic("Your boner. It's quite noticeable. Even from across the table. You could maybe cross your legs or something.", NPCCode.ASSISTANT);

        b9 = new MessageBasic("Oh. Sorry. I mean, if little me wants to express himself, I let him. At least as much as decency allows it.", NPCCode.MAIN_CHARACTER);

        b10 = new MessageBasic("I should have known. Not bothered by being like that in public and in plain view?", NPCCode.ASSISTANT);

        b11 = new MessageBasic("Why would I? It's only natural. Plus, this would be a compliment to my wonderful date. I get like that because I'm relaxed and happy to be here.", NPCCode.MAIN_CHARACTER);

        b12 = new MessageBasic("And something else maybe?", NPCCode.ASSISTANT);

        b5 = new MessageBasic("Well yeah, something else... But, you know, it's not always about sex.", NPCCode.MAIN_CHARACTER);

        b13 = new MessageBasic("Really? It's strange in the mouth of the owner of a sex club...", NPCCode.ASSISTANT);

        b14 = new MessageBasic("Well, work is work and personal life is personal life. So, outside of work, it's not all about sex.", NPCCode.MAIN_CHARACTER);

        b15 = new MessageBasic("So, dating your assistant, your work assistant, is it work or personal life?", NPCCode.ASSISTANT);

        b17 = new MessageBasic("Clearly personal life. Here, we are, two adults only linked by the fact that we both enjoy each other's company. Am I right?", NPCCode.MAIN_CHARACTER);

        b18 = new MessageBasic("Sure. And we currently are both \"enjoying\" your boner...", NPCCode.ASSISTANT);

        b19 = new MessageBasic("Not sure I'm enjoying it. At least, not until it is \"released\"...", NPCCode.MAIN_CHARACTER);

        b20 = new MessageBasic("...", NPCCode.ASSISTANT);

        b21 = new MessageBasic("You know what? I think I can help with that. You've been real nice during those dates, I think you deserve a little something. Show you that I'm not just a great assistant...", NPCCode.ASSISTANT);

        b22 = new MessageBasic("My pleasure!", NPCCode.MAIN_CHARACTER);

        b23 = new MessageBasic("What? No smart retort, no fake amazement, nothing?", NPCCode.ASSISTANT);

        b24 = new MessageBasic("Nope. I don't want to waste a golden opportunity.", NPCCode.MAIN_CHARACTER);

        b25 = new MessageBasicWithAction("Smart boy. Follow me home...", NPCCode.ASSISTANT, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
            StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, true);
        });

        b26 = new MessageBasicWithAction("You go to her appartment. With a raging boner all the way. Why not, after all?", NPCCode.NARRATOR, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
            StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, true);
        });

        b27 = new MessageBasic("Once you're there, she sits you down on her couch, and starts working her magic immediatly. Lucky man!", NPCCode.NARRATOR);

        end = new MessageEnd("By the way, don't pay attention to the fact that her appartment keeps changing between scenes. Let's say she has a knack for decorating. Reaaal fast. Yep...", NPCCode.NARRATOR);

        begin.AddSuccessor(b3);
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
        b27.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_THIRD_VIDEO_UNLOCK_DIALOG_ID, begin,
        delegate ()
        {
            if (RegisterDates.dates.Find(x => x.id == GMGlobalNumericVariables.gnv.CAFE_DATE_ID).videoLevel == 3)
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, true);

                return true;
            }
            return false;
        },
        delegate ()
        {

            DialogSceneBehavior.pathToClip = StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_VIDEOS_FOLDER + StaticStrings.ASSISTANT_FOLDER_VIDEOS_DATES + StaticStrings.ASSISTANT_FOLDER_VIDEOS_DATES_BAR + "Bar 3";
            DialogSceneBehavior.startVideo = true;
            DialogSceneBehavior.startingSceneName = StaticStrings.CLUB_SCENE;
            DialogSceneBehavior.pathToImageBackground = StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY + StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT;

            //Display the loading screen
            GameObject.FindGameObjectWithTag("LoadingScreen").GetComponentInChildren<Image>(true).enabled = true;

            SceneManager.LoadScene(StaticStrings.DIALOG_SCENE);

            StaticDialogElements.specialBackground = "";
            StaticDialogElements.dialogSpecialPortraitIsOn = false;
            StaticDialogElements.specialPortraitSprite = null;
            StaticDialogElements.dialogSpecialBackgroundIsOn = false;
            StaticDialogElements.backgroundImage = null;


        }
        ));

        /***********************************Bar date third video unlock part 2*****************************************************/

        begin = new MessageBasicWithAction("She's finished? Like that? No end to it?", NPCCode.NARRATOR,
             delegate ()
             {
                 //TODO ajouter nude photo
                 SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, false, true,
                StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                     + StaticStrings.ASSISTANT_IMAGES_DATES_FOLDER + StaticStrings.ASSISTANT_IMAGES_DATES_CAFE_FOLDER + "9"
     );
             });

        b3 = new MessageBasic("That's it for today!", NPCCode.ASSISTANT);

        b4 = new MessageBasic("Wait really?", NPCCode.MAIN_CHARACTER);

        b6 = new MessageBasic("Yep, dress up now. You should have been quicker, my friend!", NPCCode.ASSISTANT);

        b8 = new MessageBasic("Seriously? You're gonna leave me like that?", NPCCode.MAIN_CHARACTER);

        b9 = new MessageBasic("Yes! I gave you your reward, but I want to keep the rest for later. So, chop chop, get dressed.", NPCCode.ASSISTANT);

        b10 = new MessageBasic("You reluctantly get dressed. Boner still raging. Probably even more than before her \"reward\"...", NPCCode.NARRATOR);

        b11 = new MessageBasic("I mean, I'm gonna have to releave myself here. You sure you do not want to help me?", NPCCode.MAIN_CHARACTER);

        b12 = new MessageBasic("Nope. But not in my appartment please. Go back to your home, or to the club. Good luck walking there!", NPCCode.ASSISTANT);

        b5 = new MessageBasic("That's cruel!", NPCCode.MAIN_CHARACTER);

        b13 = new MessageBasic("Just playful. You'll survive it. But, I can promess you something better for next time. So keep at it!", NPCCode.ASSISTANT);

        b14 = new MessageBasic("Yes Ma'am! Good night now, I need to leave...", NPCCode.MAIN_CHARACTER);

        end = new MessageEnd("Good night!", NPCCode.ASSISTANT);

        begin.AddSuccessor(b3);
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
        b14.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_THIRD_VIDEO_UNLOCK_PART_2_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_THIRD_VIDEO_UNLOCK_DIALOG_ID).done)
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, true);

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


        /****************************************Bar date fourth video unlock part 1********************************************************************/

        begin = new MessageBasicWithAction("OK, listen to me. Listen to me! We don't want a redo of last time, OK? No blue balling this time. You gotta go at her hard and fast! You get me? YOU GET ME? Now go, tiger! Go and get that prize! Show her your skills!", NPCCode.NARRATOR, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, false, true,
                StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                                + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE
                );
        });

        b3 = new MessageBasic("Oh, your eyes are burning with a strong resolve! What's hyping you up?", NPCCode.ASSISTANT);

        b4 = new MessageBasic("You! Or rather what you did to me last time! You know I had to masturbate like 5 times to calm myself down? Little me ended up with burns on him. And my wrists ached for like two days! You know how hard it is to work in a sex club with painful wrists and burns on your penis?", NPCCode.MAIN_CHARACTER);

        b6 = new MessageBasic("I would imagine not much, since I've seen you being perfectly functional during those two days...", NPCCode.ASSISTANT);

        b8 = new MessageBasic("It's not my point! Listen, you promessed me something better last time. I'm here to collect.", NPCCode.MAIN_CHARACTER);

        b9 = new MessageBasic("Haha, aren't you desperate!", NPCCode.ASSISTANT);

        b10 = new MessageBasic("Well kinda. I'm not used to it, but I am!", NPCCode.MAIN_CHARACTER);

        b11 = new MessageBasic("So my little scheme worked then!", NPCCode.ASSISTANT);

        b12 = new MessageBasic("Scheme?", NPCCode.MAIN_CHARACTER);

        b5 = new MessageBasic("I tried to see how you would react to something like that. I now know how you would. I like it.", NPCCode.ASSISTANT);

        b13 = new MessageBasic("So? Are you going to keep your promess?", NPCCode.MAIN_CHARACTER);

        b14 = new MessageBasic("Haha, sure! Follow me home.", NPCCode.ASSISTANT);

        b15 = new MessageBasicWithAction("Yep. Come on, quickly!", NPCCode.MAIN_CHARACTER, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
    StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, true);
        });

        b17 = new MessageBasicWithAction("That was an interesting strategy. But it worked, so who am I to criticize...", NPCCode.NARRATOR, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
    StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, true);
        });

        end = new MessageEnd("Oh, of course, you both go to her appartment, at a relatively quick pace. Once there, she gets naked and lies on her couch, waiting for you, pointing at her tits...", NPCCode.NARRATOR);

        begin.AddSuccessor(b3);
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
        b17.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_FOURTH_VIDEO_UNLOCK_DIALOG_ID, begin,
        delegate ()
        {
            if (RegisterDates.dates.Find(x => x.id == GMGlobalNumericVariables.gnv.CAFE_DATE_ID).videoLevel == 4)
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, true);

                return true;
            }
            return false;
        },
        delegate ()
        {

            DialogSceneBehavior.pathToClip = StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_VIDEOS_FOLDER + StaticStrings.ASSISTANT_FOLDER_VIDEOS_DATES + StaticStrings.ASSISTANT_FOLDER_VIDEOS_DATES_BAR + "Bar 4";
            DialogSceneBehavior.startVideo = true;
            DialogSceneBehavior.startingSceneName = StaticStrings.CLUB_SCENE;
            DialogSceneBehavior.pathToImageBackground = StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY + StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT;

            //Display the loading screen
            GameObject.FindGameObjectWithTag("LoadingScreen").GetComponentInChildren<Image>(true).enabled = true;

            SceneManager.LoadScene(StaticStrings.DIALOG_SCENE);

            StaticDialogElements.specialBackground = "";
            StaticDialogElements.dialogSpecialPortraitIsOn = false;
            StaticDialogElements.specialPortraitSprite = null;
            StaticDialogElements.dialogSpecialBackgroundIsOn = false;
            StaticDialogElements.backgroundImage = null;


        }
        ));

        /****************************************Bar date fourth video unlock part 2********************************************************************/

        begin = new MessageBasic("And that's it!", NPCCode.ASSISTANT);

        b3 = new MessageBasic("AGAIN? She's doing it AGAIN?", NPCCode.NARRATOR);

        b4 = new MessageBasic("Again?", NPCCode.MAIN_CHARACTER);

        b6 = new MessageBasic("Haha, you should see your face! I'm just kidding. But a change of scenery is needed here. Follow me to the kitchen...", NPCCode.ASSISTANT);

        b8 = new MessageBasicWithAction("Sure...", NPCCode.MAIN_CHARACTER, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
    StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, true);
        });

        b9 = new MessageBasic("You follow her, grumbling all the way...", NPCCode.NARRATOR);

        b10 = new MessageBasicWithAction("Once there, she gets on her knees in front of you.", NPCCode.NARRATOR, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
    StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, false, true,
                StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                                + StaticStrings.ASSISTANT_IMAGES_DATES_FOLDER + StaticStrings.ASSISTANT_IMAGES_DATES_CAFE_FOLDER + "9");
        });

        b11 = new MessageBasic("Go ahead, finish.", NPCCode.ASSISTANT);

        b12 = new MessageBasic("What?", NPCCode.MAIN_CHARACTER);

        b5 = new MessageBasic("On my boobs. You want that, don't you?", NPCCode.ASSISTANT);

        end = new MessageEnd("Don't mind if I do!", NPCCode.MAIN_CHARACTER);

        begin.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b4.AddSuccessor(b6);
        b6.AddSuccessor(b8);
        b8.AddSuccessor(b9);
        b9.AddSuccessor(b10);
        b10.AddSuccessor(b11);
        b11.AddSuccessor(b12);
        b12.AddSuccessor(b5);
        b5.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_FOURTH_VIDEO_UNLOCK_PART_2_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_FOURTH_VIDEO_UNLOCK_DIALOG_ID).done)
            {
                    SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, false, true,
                StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                                + StaticStrings.ASSISTANT_IMAGES_DATES_FOLDER + StaticStrings.ASSISTANT_IMAGES_DATES_CAFE_FOLDER + "9");
                

                return true;
            }
            return false;
        },
        delegate ()
        {

            DialogSceneBehavior.pathToClip = StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_VIDEOS_FOLDER + StaticStrings.ASSISTANT_FOLDER_VIDEOS_DATES + StaticStrings.ASSISTANT_FOLDER_VIDEOS_DATES_BAR + "Bar 4 2";
            DialogSceneBehavior.startVideo = true;
            DialogSceneBehavior.startingSceneName = StaticStrings.CLUB_SCENE;
            DialogSceneBehavior.pathToImageBackground = StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY + StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT;

            //Display the loading screen
            GameObject.FindGameObjectWithTag("LoadingScreen").GetComponentInChildren<Image>(true).enabled = true;

            SceneManager.LoadScene(StaticStrings.DIALOG_SCENE);

            StaticDialogElements.specialBackground = "";
            StaticDialogElements.dialogSpecialPortraitIsOn = false;
            StaticDialogElements.specialPortraitSprite = null;
            StaticDialogElements.dialogSpecialBackgroundIsOn = false;
            StaticDialogElements.backgroundImage = null;


        }
        ));

        /****************************************Bar date fourth video unlock part 3********************************************************************/

        begin = new MessageBasic("So, are you relieved?", NPCCode.ASSISTANT);

        b3 = new MessageBasic("YES!", NPCCode.NARRATOR);

        b4 = new MessageBasic("Yes. Thanks for that.", NPCCode.MAIN_CHARACTER);

        b6 = new MessageBasic("Pleasure. Now, I need a shower.", NPCCode.ASSISTANT);

        b8 = new MessageBasic("Sure.", NPCCode.MAIN_CHARACTER);

        b9 = new MessageBasic("I believe you know the way back? Or would you rather sleep on the couch?", NPCCode.ASSISTANT);

        b10 = new MessageBasic("Given what we did on it, I don't think I could sleep on the couch. I better get back.", NPCCode.MAIN_CHARACTER);

        b11 = new MessageBasic("Of course. Good night then!", NPCCode.ASSISTANT);

        end = new MessageEnd("Good night.", NPCCode.MAIN_CHARACTER);

        begin.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b4.AddSuccessor(b6);
        b6.AddSuccessor(b8);
        b8.AddSuccessor(b9);
        b9.AddSuccessor(b10);
        b10.AddSuccessor(b11);
        b11.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_FOURTH_VIDEO_UNLOCK_PART_3_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_FOURTH_VIDEO_UNLOCK_PART_2_DIALOG_ID).done)
            {

                SetPortraitAndBackgroundForNextMessage(
    StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, false, true,
    StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                    + StaticStrings.ASSISTANT_DIALOGS_FOLDER + StaticStrings.ASSISTANT_IMAGES_COVERED);

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

        /****************************************Bar date fifth video unlock part 1********************************************************************/

        begin = new MessageBasicWithAction("OK big boy, we scored last time. BUT, you cannot get sloppy. We can get more, and we will. Do you here me boy? WE WILL! Now, get in there, be the best you can be. You are a winner my friend!", NPCCode.NARRATOR, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
                StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, false, true,
                StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                                + StaticStrings.ASSISTANT_CAFE_DRESS_FOLDER + StaticStrings.ASSISTANT_CAFE_DRESS_BASIC_POSE
                );
        });

        b3 = new MessageBasic("Hey, listen, I don't feel like staying here long today. Do you mind if we shorten the date this time?", NPCCode.ASSISTANT);

        b4 = new MessageBasic("Damn, that's an instant shutdown on our plan. Unlucky.", NPCCode.NARRATOR);

        b6 = new MessageBasic("Sure. Mind if I ask what's wrong?", NPCCode.MAIN_CHARACTER);

        b8 = new MessageBasic("Oh, nothing. I just feel like spending this date in my appartment. You know, \"talking\".", NPCCode.ASSISTANT);

        b9 = new MessageBasic("OK, nevermind. It was indeed an easy victory. But, we are not ones to say no to that. So, go ahead my son, get that goal!", NPCCode.NARRATOR);

        b10 = new MessageBasic("Sure! No problem with that!", NPCCode.MAIN_CHARACTER);

        b11 = new MessageBasicWithAction("Perfect! Let's go!", NPCCode.ASSISTANT, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, true);
        });

        b12 = new MessageBasicWithAction("You quickly pay (did you notice that it's the first time you actually pay? Weird...), and go to her home. Still lovely, of course.", NPCCode.NARRATOR, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, false, true,
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                        + StaticStrings.ASSISTANT_IMAGES_DATES_FOLDER + StaticStrings.ASSISTANT_IMAGES_DATES_CAFE_FOLDER + "7");
        });

        b5 = new MessageBasic("OK, no time to lose. Undress.", NPCCode.ASSISTANT);

        b13 = new MessageBasicWithAction("My pleasure!", NPCCode.MAIN_CHARACTER, delegate ()
        {

            SetPortraitAndBackgroundForNextMessage(
        StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, false, true,
        StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                        + StaticStrings.ASSISTANT_IMAGES_DATES_FOLDER + StaticStrings.ASSISTANT_IMAGES_DATES_CAFE_FOLDER + "9");
        });

        b14 = new MessageBasicWithAction("Let's do this!", NPCCode.ASSISTANT, delegate ()
        {
            SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, true);
        });

        end = new MessageEnd("Yep, that's how it went down. A good day. Maybe even the best day a man ever had...", NPCCode.NARRATOR);

        begin.AddSuccessor(b3);
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
        b14.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_FIFTH_VIDEO_UNLOCK_DIALOG_ID, begin,
        delegate ()
        {
            if (RegisterDates.dates.Find(x => x.id == GMGlobalNumericVariables.gnv.CAFE_DATE_ID).videoLevel == 5)
            {
                SetPortraitAndBackgroundForNextMessage(StaticStrings.DATE_GAME_BACKGROUNDS_FOLDER + StaticStrings.DATE_GAME_BACKGROUND_CAFE, true);

                return true;
            }
            return false;
        },
        delegate ()
        {

            DialogSceneBehavior.pathToClip = StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_VIDEOS_FOLDER + StaticStrings.ASSISTANT_FOLDER_VIDEOS_DATES + StaticStrings.ASSISTANT_FOLDER_VIDEOS_DATES_BAR + "Bar 5 1";
            DialogSceneBehavior.startVideo = true;
            DialogSceneBehavior.startingSceneName = StaticStrings.CLUB_SCENE;
            DialogSceneBehavior.pathToImageBackground = StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY + StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT;

            //Display the loading screen
            GameObject.FindGameObjectWithTag("LoadingScreen").GetComponentInChildren<Image>(true).enabled = true;

            SceneManager.LoadScene(StaticStrings.DIALOG_SCENE);

            StaticDialogElements.specialBackground = "";
            StaticDialogElements.dialogSpecialPortraitIsOn = false;
            StaticDialogElements.specialPortraitSprite = null;
            StaticDialogElements.dialogSpecialBackgroundIsOn = false;
            StaticDialogElements.backgroundImage = null;


        }
        ));

        /****************************************Bar date fifth video unlock part 2********************************************************************/

        begin = new MessageBasic("Come on big boy, let's finish this!", NPCCode.ASSISTANT);

        end = new MessageEnd("Yep. A perfect day.", NPCCode.NARRATOR);

        begin.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_FIFTH_VIDEO_UNLOCK_PART_2_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_FIFTH_VIDEO_UNLOCK_DIALOG_ID).done)
            {
                SetPortraitAndBackgroundForNextMessage(
            StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, false, true,
            StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                            + StaticStrings.ASSISTANT_IMAGES_DATES_FOLDER + StaticStrings.ASSISTANT_IMAGES_DATES_CAFE_FOLDER + "9");


                return true;
            }
            return false;
        },
        delegate ()
        {

            DialogSceneBehavior.pathToClip = StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_VIDEOS_FOLDER + StaticStrings.ASSISTANT_FOLDER_VIDEOS_DATES + StaticStrings.ASSISTANT_FOLDER_VIDEOS_DATES_BAR + "Bar 5 2";
            DialogSceneBehavior.startVideo = true;
            DialogSceneBehavior.startingSceneName = StaticStrings.CLUB_SCENE;
            DialogSceneBehavior.pathToImageBackground = StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY + StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT;

            //Display the loading screen
            GameObject.FindGameObjectWithTag("LoadingScreen").GetComponentInChildren<Image>(true).enabled = true;

            SceneManager.LoadScene(StaticStrings.DIALOG_SCENE);

            StaticDialogElements.specialBackground = "";
            StaticDialogElements.dialogSpecialPortraitIsOn = false;
            StaticDialogElements.specialPortraitSprite = null;
            StaticDialogElements.dialogSpecialBackgroundIsOn = false;
            StaticDialogElements.backgroundImage = null;


        }
        ));


        /****************************************Bar date fifth video unlock part 3********************************************************************/

        begin = new MessageBasic("Whew, I needed this.", NPCCode.ASSISTANT);

        b3 = new MessageBasic("I noticed. That was great! We should definitely do this more often.", NPCCode.MAIN_CHARACTER);

        b4 = new MessageBasic("We'll see. Maybe after another date. In the meantime, would you mind if I take a shower? I'm a bit covered here.", NPCCode.ASSISTANT);

        b6 = new MessageBasic("Sure, go ahead. Do you want me to go?", NPCCode.MAIN_CHARACTER);

        b8 = new MessageBasic("I think you can stay for the night. After what we did, we can share my bed... If you want to, that is.", NPCCode.ASSISTANT);

        b9 = new MessageBasic("Of course. I'm not gonna lie, I'm spent. I'll gladly take your offer.", NPCCode.MAIN_CHARACTER);

        b10 = new MessageBasic("Great. I'll go take that shower then.", NPCCode.ASSISTANT);

        end = new MessageEnd("She takes her shower, comes back, and you spend the night with her. I'll leave you to imagine how many time you had sex again, but let's say this was a night to remember. I'm proud of you my man!", NPCCode.NARRATOR);

        begin.AddSuccessor(b3);
        b3.AddSuccessor(b4);
        b4.AddSuccessor(b6);
        b6.AddSuccessor(b8);
        b8.AddSuccessor(b9);
        b9.AddSuccessor(b10);
        b10.AddSuccessor(end);

        dialogs.Add(new Dialog(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_FIFTH_VIDEO_UNLOCK_PART_3_DIALOG_ID, begin,
        delegate ()
        {
            if (StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_BAR_DATE_FIFTH_VIDEO_UNLOCK_PART_2_DIALOG_ID).done)
            {

                SetPortraitAndBackgroundForNextMessage(
    StaticStrings.DIALOG_BACKGROUNDS_NAME_NICOLE_LOFT, false, true,
    StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_IMAGES_FOLDER
                    + StaticStrings.ASSISTANT_DIALOGS_FOLDER + StaticStrings.ASSISTANT_IMAGES_COVERED);

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

        /*
        begin = new MessageBasic("Bloop", NPCCode.ASSISTANT);

        follower = new MessageEnd("Shoop", NPCCode.ASSISTANT);

        begin.AddSuccessor(follower);


        dialogs.Add(new Dialog(10000000, begin,
            delegate () {

                if (RegisterDates.currentDate != null && RegisterDates.currentDate.name == "Bar" && RegisterDates.currentDate.videoLevel == 1)
                {
                    SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_CRIME_OFFICE, false);
                    
                    return true;
                }
                return false;
            },
            
            delegate()
            {
                DialogSceneBehavior.pathToClip = "Girls/Generic Girl/videos/Performances/Work/Dance/Closer/Generic_Girl_Dance_Closer_01";
                DialogSceneBehavior.startVideo = true;
                DialogSceneBehavior.startingSceneName = StaticStrings.CLUB_SCENE;
                SceneManager.LoadScene(StaticStrings.DIALOG_SCENE);
            }));
            */

        /*
        begin = new MessageBasic("Bloop", NPCCode.ASSISTANT);

        follower = new MessageBasicWithAction("Shoop", NPCCode.ASSISTANT,
            delegate ()
            {

            }
        );

        begin.AddSuccessor(follower);


        dialogs.Add(new Dialog(11111, begin,
            delegate () {

                SetPortraitAndBackgroundForNextMessage(StaticStrings.DIALOG_BACKGROUNDS_NAME_CRIME_OFFICE, false);

                return false;
            }));
            */
    }

    protected static void SetPortraitAndBackgroundForNextMessage(string specialBackgroundName, bool displayEmptyPortrait, bool specialPortraitIsOn = false,
        string specialPortraitName = "")
    {
        StaticDialogElements.specialBackground = specialBackgroundName;

        if (displayEmptyPortrait)
        {
            StaticDialogElements.dialogSpecialPortraitIsOn = true;
            StaticDialogElements.specialPortraitSprite = Resources.Load<Sprite>(StaticStrings.EMPTY_PORTRAIT);
        }
        else
        {
            StaticDialogElements.dialogSpecialPortraitIsOn = specialPortraitIsOn;
            if (specialPortraitIsOn)
            {
                StaticDialogElements.specialPortraitSprite = Resources.Load<Sprite>(specialPortraitName);
            }
        }

    }

    protected static void AddBasicMessage(Message m, Message predecessor)
    {
        predecessor.AddSuccessor(m);
    }

}
