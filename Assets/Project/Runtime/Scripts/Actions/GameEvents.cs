using System;
using System.Collections;
using System.Collections.Generic;

public static class GameEvents
{
    public static Func<string> onFirstNameGenerated; //triggered when first name has been generated
    public static Func<string> onLastNameGenerated; //triggered when last name has been generated
    public static Func<string> onGenderGenerated; //triggered when gender has been generated
    public static Action onCallPerson; //triggered when the player interacts with an object that spawns the person
    public static Action onComputerInteraction; // triggered when the player interacts with the computer
    public static Action<int, string> onUpdateIDFields; //triggered when the field UI needs to be updated
    public static Action<FieldData> onUpdateBiometricFields; // triggered when the biometrics field UI needs to be updated
    public static Action<bool> onSuspiciousFlag; //triggered when the player flags the NPC as suspicious
    public static Action<string, string, string,Action,bool> showModal; //triggered when the modal needs to be shown
    public static Action onEnterDetectiveMode; //triggered when player enters detective mode
    public static Action onExitDetectiveMode; //triggered when player exits detective mode
    public static Action onShowMouse; //triggered when the game needs to show the mouse cursor
    public static Action onHideMouse; //triggered when the game needs to hide the mouse cursor
    public static Action<int, string> onPassField; //triggered when a field is selected passing it's contents like id and it's value
    public static Action<int, string> onUnselect; //triggered when an UI element was unselected
    public static Action onDetectiveModalClosed; //triggered when the modal closes(this action is passed to the modal script)
    public static Action onCitationModalClosed;//triggered when the modal closes(this action is passed to the modal script)
    public static Action<bool> onNPCDocumentsChecked; //triggered when the NPC documents have been checked. The bool is if the NPC has been approved or not
    public static Action onNPCFullyChecked; //triggered when the NPC documents have been checked regardless if the NPC has been approved or not
    public static Action<bool> onProcessPerson; //triggered when the NPC enters or exists the trigger zone
    public static Action onCitationZoneEnter; //triggered when the NPC enters the citation zone trigger
    public static Action<List<FieldData>,bool,bool> onProcessFieldData; //triggered when the NPC enter the citation zone trigger passing the required list of field data for checking if the player should recieve a citation
    public static Action onCitationGiven; //triggered when the player gets a citation
    public static Action onAIWaypointReached; //triggered when the NPC reaches a certain checkpoint
    public static Action<int> onUpdateText; //triggered when some text should be updated
    public static Action<int, int> onTimePassed; //triggered when time has passed. Used in updating the time UI
    public static Action<SituationObject> onNPCSituation; //triggered when an NPC has a situation that needs to be resolved
    public static Action<int> onChoiceButtonClicked; //triggered when any of the choice buttons have been clicked
    public static Action<int> onSituationChoice; //triggered when a choice has been made
    public static Action onSituationResolved; //triggered when the situation has been fully resolved
    public static Action updateApprovalCount; //triggered when the approval count needs to be updated if the player accepted an NPC that was begging/bribing
    public static Action onMicrophoneInteracted; //triggered when the microphone has been interacted with
    public static Action onDisablePlayerInput; //triggered when we need to disable the player input
    public static Action onEnablePlayerInput; //triggered when we need to enable the player input
}
