using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using RimWorld;
using Verse;

namespace UINotIncluded.Widget.Workers
{
    public class Numbers_worker : Button_Worker
    {
        public Numbers_worker(Configs.NumbersConfig config) : base(config) { }

        public override bool ShowTooltip => false;
        public override void OpenConfigWindow()
        {
            Find.WindowStack.Add(new UINotIncluded.Windows.EditMainButton_Window(this.config));
        }
        public override void InterfaceTryActivate()
        {
            ((Configs.NumbersConfig)this.config).Update();
            UINI.Warning("Pressed on the button " + config.SettingLabel);
            base.InterfaceTryActivate();
        }

    }
}
