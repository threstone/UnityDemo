using UnityEngine;
using FairyGUI;
public class Main : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);

        ConfigMgr.Init();

        BuffMgr.Init();

        SkillMgr.Init();

        InitFairyGUI();
    }

    void InitFairyGUI()
    {
        UIPackage.AddPackage("FGUI/Base");
        // var d1 = UIPackage.CreateObject("Base", "Damage").asCom;
        // d1.GetChild("Text").asTextField.text = "567";
        // var d2 = UIPackage.CreateObject("Base", "Damage").asCom;
        // d2.GetChild("Text").asTextField.text = "890";
        // d2.x = d1.x + 200;
        // var main = UIPackage.CreateObject("Base", "Main").asCom;
        // GRoot.inst.AddChild(main);
        // GRoot.inst.AddChild(d1);
        // GRoot.inst.AddChild(d2);
    }
}