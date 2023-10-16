using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class Panel_Test : UIBase
{
    public override string panelName => "Panel_Test";

    #region Field

    private Button button;

    #endregion


    protected override void InitComponent(Transform trans)
    {
        button = GetUIComponent<Button>(trans, "Button");
        button.onClick.AddListener(() =>
        {
            Debug.Log("成功调用!");
        });
    }
}

