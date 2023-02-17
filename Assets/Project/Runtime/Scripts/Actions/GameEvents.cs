using System;
using System.Collections;
using System.Collections.Generic;

public static class GameEvents
{
    public static Func<string> onFirstNameGenerated;
    public static Func<string> onLastNameGenerated;
    public static Func<string> onGenderGenerated;
    public static Action onCallPerson;
    public static Action onComputerInteraction;
    public static Action<int, string> onUpdateIDFields;
    public static Action<FieldData> onUpdateBiometricFields;
    public static Action<bool> onSuspiciousFlag;
    public static Action<string, string, string,Action,bool> showModal;
    public static Action onEnterDetectiveMode;
    public static Action onExitDetectiveMode;
    public static Action onShowMouse;
    public static Action onHideMouse;
    public static Action<int, string> onPassField;
    public static Action<int, string> onUnselect;
    public static Action onDetectiveModalClosed;
    public static Action onCitationModalClosed;
    public static Action<bool> onNPCDocumentsChecked;
    public static Action onNPCFullyChecked;
    public static Action<bool> onProcessPerson;
    public static Action onCitationZoneEnter;
    public static Action<List<FieldData>,bool> onProcessFieldData;
    public static Action onCitationGiven;
    public static Action onAIWaypointReached;
}
