using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookUI
{
    public class DesigningPanel : Panel
    {
        private const int k_MaxWidthForButton = 160;
        private const int k_MaxHeightForButton = 28;
        private const int k_LocationHeightDiff = 30;
        private const int k_MaxVerticalLocation = 61;
        private const int k_MaxHorizontalLocation = 16;
        private readonly List<ButtonItem> r_ButtonList = new List<ButtonItem>();
        private int m_LatestLocation = -31;
        
        public void Add(ButtonItem i_ButtonItem)
        {
            if (m_LatestLocation + k_LocationHeightDiff > k_MaxVerticalLocation)
            {
                throw new Exception("Panel is already full! Please delete one of the button's first.");
            }
            else
            {
                m_LatestLocation += k_LocationHeightDiff;
                i_ButtonItem.Width = k_MaxWidthForButton;
                i_ButtonItem.Height = k_MaxHeightForButton;
                i_ButtonItem.Location = new Point(k_MaxHorizontalLocation, m_LatestLocation);
                i_ButtonItem.Font =
                    new System.Drawing.Font(
                        "Century Gothic",
                        9F,
                        System.Drawing.FontStyle.Bold,
                        System.Drawing.GraphicsUnit.Point,
                        0);
                i_ButtonItem.BackColor =
                    System.Drawing.Color.FromArgb(
                        74,
                        210,
                        149);
                i_ButtonItem.FlatStyle =
                    System.Windows.Forms.FlatStyle.Flat;
                r_ButtonList.Add(i_ButtonItem);
                this.Controls.Add(i_ButtonItem);
            }
        }

        public void Remove(ButtonItem i_ButtonItem)
        {
            r_ButtonList.Remove(i_ButtonItem);
            this.Controls.Remove(i_ButtonItem);
            m_LatestLocation -= k_LocationHeightDiff;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }
}
