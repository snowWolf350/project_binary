using UnityEngine;

[CreateAssetMenu(fileName = "HelpSO", menuName = "Scriptable Objects/HelpSO")]
public class HelpSO : ScriptableObject
{
    [TextArea(3,10)]
    public string _helpInfo;
}
