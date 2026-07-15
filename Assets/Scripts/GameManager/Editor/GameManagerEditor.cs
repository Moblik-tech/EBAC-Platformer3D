using System.Linq;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager fsm = (GameManager)target;

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("State Machine");

        if (fsm.stateMachine == null) return;

        if (fsm.stateMachine.CurrentBase != null)
        {
            EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.CurrentBase.ToString());
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");

        if (showFoldout == true)
        {
            if (fsm.stateMachine.dictionaryState != null)
            {
                var keys = fsm.stateMachine.dictionaryState.Keys.ToArray();
                var values = fsm.stateMachine.dictionaryState.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format($"{keys[i]} <-:-> {values[i]}"));
                }
            }
        }
    }
}