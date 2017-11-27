using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;

namespace LibServer
{
    class ClientReceiveFilter : FixedHeaderReceiveFilter<BinaryRequestInfo>
    {
        public ClientReceiveFilter()
            : base(2)
        {
        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            return (ushort)header[offset];
        }

        protected override BinaryRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] buffer, int offset, int length)
        {
            return new BinaryRequestInfo(null, buffer.CloneRange(offset, length));
        }
    }
}
