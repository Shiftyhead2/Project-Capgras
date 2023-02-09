using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static Func<string> onFirstNameGenerated;
    public static Func<string> onLastNameGenerated;
    public static Func<string> onGenderGenerated;
    public static Action onCallPerson;
    public static Action onComputerInteraction;
    public static Action<int, string> onUpdateIDFields;
    public static Action<FieldData> onUpdateBiometricFields;
    public static Action<string, string, string> showModal;
    public static Action onEnterDetectiveMode;
    public static Action onExitDetectiveMode;
    public static Action<int, string> onPassField;
    public static Action<int, string> onUnselect;
    public static Action onModalClosed;
}
