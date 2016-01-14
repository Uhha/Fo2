using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fo2
{
    class FRM
    {
        private FrameDirecion N;
        private FrameDirecion NE;
        private FrameDirecion SE;
        private FrameDirecion S;
        private FrameDirecion SW;
        private FrameDirecion NW;

        private int _numberOfDirections;

        public FRM()
        {
            FileStream fs = new FileStream("C:/HANPWRMJ.FRM", FileMode.Open);
            int hexIn;
            String hex;

            for (int i = 0; (hexIn = fs.ReadByte()) != -1; i++)
            {
                hex = string.Format("{0:X2}", hexIn);
            }



            fs.Close();
        }
    }
}
