
cs.exports.ProtocalType = {}
    ProtocalType.BINARY = 0     --//二进制
    ProtocalType.PB_LUA = 1     --//pblua
    ProtocalType.PBC    = 2     --//pbc
    ProtocalType.SPROTO = 3     --//sproto


cs.exports.Protocal = {}
--///BUILD TABLE
    Protocal.Connect            = 101;      --  //连接服务器
    Protocal.Exception          = 102;      --  //异常掉线
    Protocal.Disconnect         = 103;      --   //正常断线
    Protocal.Login 			    = 104;	    -- //登录游戏