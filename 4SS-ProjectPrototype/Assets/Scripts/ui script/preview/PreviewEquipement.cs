using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewEquipement : MonoBehaviour {

    public PlayerInfo player;
    public GameObject previewItem;

    [SerializeField]
    private Image itemPreview;
    [SerializeField]
    private Text namePreview;
    [SerializeField]
    private Text lifePreview;
    [SerializeField]
    private Text attaquePreview;
    [SerializeField]
    private Text competence;
    [SerializeField]
    private Text defensePreview;
    [SerializeField]
    private Text precisionPreview;
    [SerializeField]
    private Text speedPreview;

    public void PointerEnter(int id) {

        previewItem.SetActive(true);
        Equipement equip = player.equipements[id];

        itemPreview.sprite = equip.spriteUI;
        namePreview.text = equip.name;
        lifePreview.text = equip.life.ToString();
        attaquePreview.text = equip.attack.ToString();
        defensePreview.text = equip.defence.ToString();
        speedPreview.text = equip.speed.ToString();
        competence.text = equip.competence.description;
    
    }

    public void PointerExit() {
        previewItem.SetActive(false);
    }
}
