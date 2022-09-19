using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KK.SARIcon
{
    public class IconItem
    {

        public IconItem() { }
        public IconItem(String text, Point location, Int32 index)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new ApplicationException("必须指定图标标题！");
            }

            this.Text = text;
            this.Location = location;
            this.Index = index;
        }

        public String Text { get; set; }
        public Point Location { get; set; }
        public Int32 Index { get; set; }
    }
}
