using System;
using System.Collections.Generic;

namespace CnDCopy.Kernel.LocationManagers
{
    public class CopyRequestState
    {
        public int BytesRead { get; set; }
        public long FileSize { get; set; }
        public event Action CopyDone = () => { };
        public event Action<byte[]> Buffering = bytes => { };


        internal void FireCopyDone()
        {
            CopyDone();
        }

        internal void FireBuffering(byte[] data)
        {
            Buffering(data);
        }
    }
}