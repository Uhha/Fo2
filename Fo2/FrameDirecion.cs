using System.Collections.Generic;

namespace Fo2
{
    internal class FrameDirecion
    {
        private LinkedList<Frame> _frames;
        private int _directionOffsetX;
        private int _directionOffsetY;
        private int _numberOfFrames;

        public int _directionOffset;


        public FrameDirecion(int numberOfFrames, int directionOffset)
        {
            _frames = new LinkedList<Frame>();
            _numberOfFrames = numberOfFrames;
            _directionOffset = directionOffset;
        }
    }
}