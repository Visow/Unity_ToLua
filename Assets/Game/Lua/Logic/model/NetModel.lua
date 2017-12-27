cs.exports.NetModel = {}

function NetModel.Connect()
    -- 网络管理器
    NetWorkManager:SendConnect("192.168.2.243", 6688)
end

function NetModel.Send(msgId, byteBuffer)
    local sender = cs.ByteBuffer.New()
    sender:WriteShort(msgId)
    sender:WriteByte(ProtocalType.BINARY)
    sender:Apped(byteBuffer)
    NetWorkManager:SendMessage(sender)
end