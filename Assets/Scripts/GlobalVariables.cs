using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVaria
{
    public static string[] REPUTATION = new string[11] {
        "Nobody",
        "Unknown",
        "Heard of",
        "Neighbourhood attraction",
        "Neighbourhood celebrity",
        "Town attraction",
        "Town celebrity",
        "Renowned",
        "Celebrity",
        "Star",
        "Superstar"
    };

    public static string TRAIN = "Training";
    public static string TALK = "Talking";
    public static string WORK = "Working";
    public static string REST = "Resting";

    public static string DANCE = "Dance";
    public static string POSE = "Pose";
    public static string FOREPLAY = "Foreplay";
    public static string ORAL = "Oral";
    public static string SEX = "Sex";
    public static string GROUP = "Group";

    public static string BUST_TYPE_TEXT = "Bust";
    public static string EYE_COLOR_TYPE_TEXT = "Eyes";
    public static string HAIR_COLOR_TEXT = "Hair";
    public static string BODY_TYPE_TEXT = "Body";
    public static string SKIN_COMPLEXION_TEXT = "Skin";
    public static string AGE_TEXT = "Age";
    public static string HEIGHT_TEXT = "Height";

    public static string POPULARITY_TEXT = "Popularity";
    public static string ENERGY_TEXT = "Energy";
    public static string OPENNESS_TEXT = "Openness";

    public static string TRAIN_FOLDER = "Train/";
    public static string TALK_FOLDER = "Talk/";
    public static string WORK_FOLDER = "Train/"; // TODO differentiate train and work content (later...)
    public static string REST_FOLDER = "Rest/";

    public static string DANCE_FOLDER = "Dance/";
    public static string POSE_FOLDER = "Pose/";
    public static string FOREPLAY_FOLDER = "Foreplay/";
    public static string ORAL_FOLDER = "Oral/";
    public static string SEX_FOLDER = "Sex/";
    public static string GROUP_FOLDER = "Group/";

    public static string FINISH_FOLDER = "Finish/";
    public static string FACIAL_FINISH_FOLDER = "Facial/";
    public static string BODY_FINISH_FOLDER = "Body/";
    public static string INSIDE_FINISH_FOLDER = "Inside/";

    public static string FACE_FINISH_SUBFOLDER = "Face/";
    public static string BODY_FINISH_SUBFOLDER = "Body/";
    public static string CREAMPIE_FINISH_SUBFOLDER = "Creampie/";

    public static string SWALLOW_FINISH_SUBFOLDER = "Swallow/";
    public static string TITS_FINISH_SUBFOLDER = "Tits/";
    public static string ANAL_CREAMPIE_FINISH_SUBFOLDER = "AnalCreampie/";

    public static string DANCE_SUBFOLDER = "Dance/";
    public static string CLOSER_DANCE_SUBFOLDER = "Closer/";
    public static string TOPLESS_DANCE_SUBFOLDER = "Topless/";
    public static string MONEY_DANCE_SUBFOLDER = "Money/";

    public static string NAKED_SUBFOLDER = "Naked";
    public static string HAND_MASTURBATION_SUBFOLDER = "HandMast";
    public static string TOY_MASTURBATION_SUBFOLDER = "ToyMast";

    public static string HANDJOB_SUBFOLDER = "HJ";
    public static string FOOTJOB_SUBFOLDER = "FJ";
    public static string TITSJOB_SUBFOLDER = "TJ";

    public static string BLOWJOB_SUBFOLDER = "BJ";
    public static string DEEPTHROAT_SUBFOLDER = "Deepthroat";
    public static string FACEFUCK_SUBFOLDER = "Facefuck";

    public static string MISSIONARY_SUBFOLDER = "Missionary";
    public static string DOGGYSTYLE_SUBFOLDER = "Doggy";
    public static string ANAL_SEX_SUBFOLDER = "Anal";

    public static string THREESOME_SUBFOLDER = "3Some";
    public static string FOURSOME_SUBFOLDER = "4Some";
    public static string GANGBANG_SUBFOLDER = "GB";

    public static string GIRLS_FOLDER = "Girls/";
    public static string IMAGES_FOLDER = "images/";
    public static string VIDEOS_FOLDER = "videos/";
    public static string JSON_DATA_FOLDER = "JSON_data/";
    public static string TEXTS_FOLDER = "texts/";
    public static string PRESTATIONS_FOLDER = "Prestations/";

    public static string GENERIC_GIRL_NAME = "Generic Girl";

    public static string MONEY_SIGN = "$";
    public static string REPUTATION_SIGN = "reputation";
    public static string INFLUENCE_SIGN = "influence";
    public static string CONNECTION_SIGN = "connection";

    public static string CLUB_SCENE = "ClubScene";
    public static string DAY_RESULT_SCENE = "DayResultScene";
    public static string INIT_SCENE = "InitializationScene";
    public static string PLANNING_SCENE = "PlanningScene";
    public static string GIRL_PLANNING_SCENE = "GirlPlanningScene";
    public static string RECRUITMENT_SCENE = "RecruitmentScene";
    public static string IMPROVEMENTS_SCENE = "BuilderScene";
    public static string POLICIES_SCENE = "PoliciesScene";
    public static string STAFF_SCENE = "StaffDisplayScene";
    public static string BOOTHS_MANAGEMENT_SCENE = "ActivityScene";
    public static string ROOMS_BUILT_SCENE = "RoomsBuiltScene";
    public static string MAIN_MENU_SCENE = "MainMenuScene";
    public static string POLICIES_BOUGHT_SCENE = "PoliciesBoughtScene";
    public static string SAVE_SCENE = "SaveScene";
    public static string LOAD_SCENE = "LoadScene";
    public static string LOAD_SCENE_FROM_MAIN_MENU = "LoadSceneFromMainMenu";
    public static string ALL_GIRL_RECRUITED_SCENE = "AllGirlRecruitedScene";
    public static string WARDROBE_SCENE = "WardrobeScene";

    public static string PORTRAIT_FILE = "portrait.jpg";
    public static string PORTRAIT_FILE_NO_EXTENSION = "portrait";
    public static string CLOSEUP_PORTRAIT_FILE = "portrait_small.jpg";
    public static string CLOSEUP_PORTRAIT_FILE_NO_EXTENSION = "portrait_small";
    public static string BIO_FILE = "Bio.txt";
    public static string BIO_FILE_NO_EXTENSION = "Bio";
    public static string DESCRIPTION_FILE = "Description.txt";
    public static string DESCRIPTION_FILE_NO_EXTENSION = "Description";

    public static string RECRUITMENT_ERROR_POPUP_MESSAGE = "Can't recruit more girls, you have them all, you naughty dog!";
    public static string ALL_POLICIES_BOUGHT_ERROR_POPUP_MESSAGE = "Can't pass more policies, you already passed them all!";
    public static string ALL_IMPROVEMENTS_BOUGHT_ERROR_POPUP_MESSAGE = "Can't buy more improvements, you already bought them all!";
    public static string STAFF_DISPLAY_ERROR_POPUP_MESSAGE = "You haven't recruited any girl yet!";
    public static string NOT_ENOUGH_MONEY_RECRUITMENT_ERROR_POPUP_MESSAGE = "Not enough money to hire this girl.";
    public static string NOT_ENOUGH_REPUTATION_ERROR_POPUP_MESSAGE = "Not enough reputation to hire this girl.";
    public static string NOT_ENOUGH_INFLUENCE_ERROR_POPUP_MESSAGE = "Not enough influence to hire this girl.";
    public static string NOT_ENOUGH_CONNECTION_ERROR_POPUP_MESSAGE = "Not enough connection to hire this girl.";

    public static string ASK_FOR_HELP = "Help required!";

    public static string CLIENT_WAITING = "Waiting";

    public static string TIME_LEFT = "Time left:";

    public static string HEIGHT_UNIT = "cm";

    public static string CLIENT_WANTS_TEXT = "Wants";

    public static string SAVE_FILE = "save.sav";

    public static string CLUB_DATA_SAVE_FILE = "clubData.sav";
    public static string RECRUITED_GIRLS_SAVE_FILE = "recruitedGirls.sav";
    public static string POLICIES_DATA_SAVE_FILE = "policiesData.sav";
    public static string IMPROVEMENTS_DATA_SAVE_FILE = "improvementsData.sav";
    public static string NUMBERS_DATA_SAVE_FILE = "numbersData.sav";

    public static string IMPROVEMENTS_FOLDER = "ClubImprovements/";
    public static string IMPROVEMENT_DATA_FILE = "Improvement";

    public static string ADVERTISEMENTS_IMPROVEMENT_NAME = "Advertisements";
    public static string BAR_IMPROVEMENT_NAME = "Bar";
    public static string GYM_IMPROVEMENT_NAME = "Gym";
    public static string KITCHEN_IMPROVEMENT_NAME = "Kitchen";
    public static string PHARMACY_IMPROVEMENT_NAME = "Pharmacy";
    public static string CONDOM_DISPENSER_IMPROVEMENT_NAME = "Condom dispenser";
    public static string STAGE_IMPROVEMENT_NAME = "Stage";
    public static string PRESTATIONS_ROOMS_IMPROVEMENT_NAME = "Prestations rooms";
    public static string MAID_IMPROVEMENT_NAME = "Maid";
    public static string SECRETARY_IMPROVEMENT_NAME = "Secretary";
    public static string SECURITY_IMPROVEMENT_NAME = "Security";
    public static string NEW_BOOTHS_IMPROVEMENT_NAME = "New booths";
    public static string DRESSING_ROOM_IMPROVEMENT_NAME = "Dressing room";
    public static string SPECIAL_TRAINING_IMPROVEMENT_NAME = "Special training";
    public static string WAITRESSES_COSTUMES_IMPROVEMENT_NAME = "Waitresses costumes";

    public static string POLICIES_FOLDER = "Policies/";
    public static string POLICY_DATA_FILE= "Policy";



    public static string ANAL_POLICY_NAME = "Anal";
    public static string ANAL_CREAMPIE_POLICY_NAME = "Anal creampie";
    public static string BLOWJOB_POLICY_NAME = "Blowjob";
    public static string BODY_CUMSHOT_POLICY_NAME = "Body cumshot";
    public static string CREAMPIE_POLICY_NAME = "Creampie";
    public static string DANCE_POLICY_NAME = "Dance";
    public static string DANCE_CLOSER_POLICY_NAME = "Dance closer";
    public static string DANCE_TOPLESS_POLICY_NAME = "Dance topless";
    public static string DEEPTHROAT_POLICY_NAME = "Deepthroat";
    public static string DOGGYSTYLE_POLICY_NAME = "Doggystyle";
    public static string FACEFUCK_POLICY_NAME = "Facefuck";
    public static string FACIAL_POLICY_NAME = "Facial";
    public static string FOOTJOB_POLICY_NAME = "Footjob";
    public static string FOURSOME_POLICY_NAME = "Foursome";
    public static string GANGBANG_POLICY_NAME = "Gangbang";
    public static string HANDJOB_POLICY_NAME = "Handjob";
    public static string MISSIONARY_POLICY_NAME = "Missionary";
    public static string POSE_NAKED_POLICY_NAME = "Pose naked";
    public static string SOLO_FINGERING_POLICY_NAME = "Solo fingering";
    public static string SWALLOW_POLICY_NAME = "Swallow";
    public static string THREESOME_POLICY_NAME = "Threesome";
    public static string TITS_CUMSHOT_POLICY_NAME = "Tits cumshot";
    public static string TITSJOB_POLICY_NAME = "Titsjob";
    public static string TOYS_MASTURBATING_POLICY_NAME = "Toys masturbating";

    public static string COSTUMES_FOLDER = "Costumes/";
    public static string COSTUME_DATA_FILE = "Costume";

    public static string BASIC_COSTUME_NAME = "Basic";
    public static string BUNNY_COSTUME_NAME = "Bunny";
    public static string DIRNDL_COSTUME_NAME = "Dirndl";
    public static string NAKED_COSTUME_NAME = "Naked";
    public static string NURSE_COSTUME_NAME = "Nurse";
    public static string TOPLESS_COSTUME_NAME = "Topless";

    public static string NOT_ENOUGH_ENERGY_ERROR_MESSAGE = "She is too tired to work!\n(energy depleted)";
    public static string NOT_ENOUGH_OPENNESS_ERROR_MESSAGE = "She doesn't want to do that!\n(not enough openness)";

    public static string NOT_ENOUGH_MONEY_IMPROVEMENT_ERROR_MESSAGE = "You don't have enough money to buy this improvement.";
    public static string NOT_ENOUGH_INFLUENCE_IMPROVEMENT_ERROR_MESSAGE = "You don't have enough influence to buy this improvement.";
    public static string NOT_ENOUGH_CONNECTION_IMPROVEMENT_ERROR_MESSAGE = "You don't have enough connection to buy this improvement.";

    public static string NOT_ENOUGH_MONEY_POLICY_ERROR_MESSAGE = "You don't have enough money to buy this policy.";
    public static string NOT_ENOUGH_INFLUENCE_POLICY_ERROR_MESSAGE = "You don't have enough influence to buy this policy.";
    public static string NOT_ENOUGH_CONNECTION_POLICY_ERROR_MESSAGE = "You don't have enough connection to buy this policy.";
    public static string NO_GIRL_ABLE_POLICY_ERROR_MESSAGE = "You don't employ a girl who will accept to do that!";

    public static string[] IMPROVEMNTS_DIRECTORIES_NAMES = new string[14]
    {
        "Advertisements",
        "Bar",
        "Gym",
        "Condom dispenser",
        "Dressing room",
        "Kitchen",
        "Maid",
        "New booths",
        "Pharmacy",
        "Secretary",
        "Security",
        "Prestations rooms",
        "Special training",
        "Waitresses costumes"
    };


    public static string[] TUTORIAL_CLUB_STRINGS_PHASE_ZERO = new string[7] {
        "In this game, you are the owner of a very special club. You recruit girls, improve your equipment and pass new policies to allow your girls to perform all kinds of actions, most of them (if not all of them) sexual in nature.\n You are currently on the club management screen."
    , "On the top of the screen, you can see (from left to right) the number of in game days since you started (it is currently on day 1), your connection (currently unused in this prototype), your influence (currently unused in this prototype), your reputation and your money."
    ,"The number of days passed is only an indicator, it is not used in any particular way."
        ,"Connection and influence will represent the level of connection you have with the police and influence you have over the different crime organizations. "
        ,"Your reputation is how well-known your establishment is. The more the reputation, the better (and more importantly, richer) the clients. It can be lost if your girls perform poorly or it can be spent to buy certain improvements or policies (more on that later)."
        ,"Your money is simply the amount of money you have in the bank. It is currently at " + GMGlobalNumericVariables.gnv.STARTING_MONEY + MONEY_SIGN + " and will soon reach 0 when you recruit your first girl."   
        ,"Speaking of recruiting girls, let's do that.\n Start by clicking on the \"Recruit girls\" button."
    };

    public static string[] TUTORIAL_RECRUITMENT_STRINGS_PHASE_ZERO = new string[7] {
        "Here, you can find all the information on the girls you want to recruit, and, of course, hire them.",
        "First off, on the left of this screen, you can see different personal informations about this girl. They include various physical traits, her height, age and biography (those are not used in this prototype).",
        "In the center, you can see her name at the top and her picture below. This tutorial is currently above her picture, but fear not, it will soon disappear so that you can admire every recruitable girls.",
        "On the top right, you can see three values: her energy, openness and popularity. All of those are important for the various performances she will deliver during work. It will be explained later.",
        "Below the button taking you back to the club management screen, you can see the values of her skills. These represent how good she is at a certain type of performance, scaling from 0 to 100. During work, the higher her skill in a domain, the more money she will earn for doing it.",
        "Finally, you can see on the bottom right the button to hire her and the cost to do so. You can also recruit her by clicking on her portrait. For the moment, you can't hire all of them, only those that cost "+ GMGlobalNumericVariables.gnv.STARTING_MONEY + MONEY_SIGN + " and nothing else. You can navigate between all the girls by clicking on the arrows at the top of the screen.",
        "Click the \"Next\" button to hide this text box. When you have decided on the girl you want, recruit her and click \"Back to club management\". This tutorial will then resume.",
    };

    public static string[] TUTORIAL_CLUB_STRINGS_PHASE_ONE = new string[1] {
        "Now that you recruited a girl, it is time to build the room in which she will work. For that, you need to improve your club.\n Click on the \"Improve your club\" button."
    };

    public static string[] TUTORIAL_IMPROVEMENTS_STRINGS_PHASE_ZERO = new string[6] {
        "Here, you can find all the info on the various improvements available, and buy them.",
        "Each one has a different effect, for which you can see a simple explanation on the right of the image representing the improvement itself. If this image has a green border, it means you have the ressources needed to buy it.",
        "For the moment, you can only buy one, as it is free and you don't have anymore money. It is the dancing stage, that your girls need in order to be able to dance for the clients.",
        "Clicking on it, you will see that it explains in more details what it does, its price and give you the ability to buy it.",
        "When you buy an improvement, it is instantly added to your club and you will be able to use it immediatly.",
        "Click the \"Next\" button to hide this text box. Scroll to the bottom of the list and buy the stage then click \"To club\". This tutorial will then resume.",
    };

    public static string[] TUTORIAL_CLUB_STRINGS_PHASE_TWO = new string[1] {
        "You built the stage, but it is not enough for your girls to be able to dance for the clients. You need to pass a policy allowing them to do so.\n Click on the \"New policies\" button."
    };

    public static string[] TUTORIAL_POLICIES_STRINGS_PHASE_ZERO = new string[6] {
        "Here, you can find all the info on the various policies available, and buy them.",
        "Each one has a different effect, for which you can see a simple explanation on the right of the image representing the policy itself. If this image has a green border, it means you have the ressources needed to buy it.",
        "For the moment, there is only one, because you don't have the prerequisites for the other. It is the dancing policy, that your girls need in order to be able to dance for the clients.",
        "Clicking on it, you will see that it explains in more details what it does, display its price and give you the ability to buy it.",
        "When you buy a policy, it is instantly added to your club and you will be able to use it immediatly. In addition, it will unlock other policies to buy. Each policy requires you to have passed other specific policies before. They also require to employ at least one girl with a minimal openness. This is to ensure that you have at least one girl able to perform whatever policy you just passed.",
        "Click the \"Next\" button to hide this text box. When you have passed the dance policy, click \"To club\". This tutorial will then resume.",
    };

    public static string[] TUTORIAL_CLUB_STRINGS_PHASE_THREE = new string[1] {
        "Your girl is now ready to work. You must now decide if she will work for this day. Click on \"Planning\" to begin  the process of assigning her to work."
    };

    public static string[] TUTORIAL_PLANNING_STRINGS_PHASE_ZERO = new string[2] {
        "Here, you can decide for each girl if she will work or rest today. Working will make her available for today's session, while resting will give her some energy back.",
        "You currently have only one girl, and she is completely rested, so she should work today. Click the \"Next\" button to hide this text box then on \"Work\" to send her to work and \"Start the day\" to begin the work day."
    };

    public static string[] TUTORIAL_BOOTH_GAME_STRINGS_PHASE_ZERO = new string[10] {
        "Now, you can start your working day. Here, you will have clients coming to the club, wanting for a girl to take care of them.",
        "When a client arrive, they will be automatically put into a booth. You will then have to drag and drop a girl on the empty space on their left to assign a girl to them.",
        "Keep in mind that a girl working will lose some energy, and if she doesn't have any, she won't be able to work. She also needs to have enough openness to do whatever it is the client wants. Her energy is displayed in green on her portrait, and you can see her openness along with other traits by clicking on it.",
        "Click the \"Next\" button to hide this text box, wait for a client to come then assign them a girl." ,
        "Now that your girl is working, you just have to wait for her to do her thing. Enjoy watching. You can get a better view by clicking on \"Zoom in\", don't worry, it will pause the game. You can also see different informations on the client by clicking on their portrait.\n Click the \"Next\" button to hide this text box.",
        "Something interesting just happened: your client asked for a little something on the side. Unfortunately, you currently have no way to fulfill their desire. You can buy improvements to be able to do so.",
        "There are four types of side treats: drinks, foods, drugs and condoms. Each client will ask for one of those, and fulfilling their desire will give them a happiness boost, which can translate to an increase in earnings.",
        "Speaking of earnings, have you noticed the little \"+x\" appearing on the right of the client? This is indicating that your girl has earned some money. At the end of the day, she will take a cut of this money and give you what is left.",
        "Finally, after some time, the client will finish their business. Currently, they will just leave. But at some point, you will be able to buy a \"finisher\" policy. This will allow your girl to help the client finish their business and therefore earn more money.",
        "But for now, you just have to wait for the client to leave. A new client will come to an empty booth when it is available. You can end the working phase at any time by clicking on \"End day\". Click the \"Next\" button to hide this text box and play for this working day until it is over or press \"End day\" to end it early."
    };

    public static string[] TUTORIAL_CLUB_STRINGS_PHASE_FOUR = new string[2] {
        "Finally, you can access the menu by pressing escape, where you can save or load a game, go back to the main menu or quit.",
        "You have reached the end of this tutorial. You can now freely enjoy the game. And since it is still a prototype, I'll be happy to hear anything you have to say about it. Have fun!"
    };
}



