using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookUI
{
    public class ButtonItem : Button
    {
        public Action CommandDelegate { get; set; }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            CommandDelegate?.Invoke();
        }
    }
}
