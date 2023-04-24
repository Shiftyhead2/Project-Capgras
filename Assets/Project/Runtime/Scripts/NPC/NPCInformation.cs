using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInformation : MonoBehaviour
{
  [field: SerializeField]
#if UNITY_EDITOR
  [field: ReadOnlyInspector]
#endif
  public string Name { get; private set; }

  [field: SerializeField]
#if UNITY_EDITOR
  [field: ReadOnlyInspector]
#endif
  public string Gender { get; private set; }

  [field: SerializeField]
#if UNITY_EDITOR
  [field: ReadOnlyInspector]
#endif
  public bool isDoppleganger { get; private set; }

  public SituationObject beggingObject;
  public SituationObject bribeObject;

  public float begChance = 0.5f;
  public float bribeChance = 0.5f;

  private void Awake()
  {
    GenerateInformation();
  }


  void GenerateInformation()
  {
    GetGender();
    GetName();
    isDoppleganger = IsDoppleganger();
  }

  void GetName()
  {
    Name = GameEvents.onNameGenerated?.Invoke(Gender, false, Gender);
    GameEvents.onUpdateIDFields?.Invoke(1, Name);
  }



  void GetGender()
  {
    Gender = GameEvents.onGenderGenerated?.Invoke();
    GameEvents.onUpdateIDFields?.Invoke(0, Gender);
  }

  bool IsDoppleganger()
  {
    float chance = Random.Range(0f, 1f);
    return chance <= 0.3f;
  }

}
