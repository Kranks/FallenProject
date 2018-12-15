using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "GameSettings", fileName = "newSettings")]
public class GameSettings : ScriptableObject {

    public enum Control {
        NONE,
        ESC,
        SKILL1,
        SKILL2,
        SKILL3,
        SKILL4,
        SKILL5,
        SKILL6,
        SKILL7,
        INVENTORY,
        MAP,
        MOVEUP,
        MOVEDOWN,
        MOVELEFT,
        MOVERIGHT
    }

    public Dictionary<Control, KeyCode> Controls = new Dictionary<Control, KeyCode>();

    public KeyCode key;
}
