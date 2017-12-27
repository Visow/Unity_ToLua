
--[[
    ## unity core for visow
    ##  param config
    ## ViewBase.lua
    ## for prefabs behaviour by lua
    ## create time by 2017/8/17 17:12
]]
------------------------------------------------------------------------------
------------------------------------------------------------------------------

local LoginPanel = class("LoginPanel", LuaComponent)

function LoginPanel:Ctor(...)
    LuaComponent.Ctor(self, ...)
    log("LoginPanel: Ctor")
    -- EventCenter.addTCP(Protocal.Connect, self, self.OnConnect)
    -- EventCenter.addTCP(Protocal.Login, self, self.OnLogin)
    -- NetModel.Connect()
end

function LoginPanel:Awake()
    local pBtnTf = ComUtil.FindChildTfByList("bgImg:loginBtn", self.gameObject.transform)
    if pBtnTf then
        local pBtnCom = pBtnTf:GetComponent(typeof(UnityEngine.UI.Button))
        if pBtnCom then
            pBtnCom.onClick:AddListener(handler(self, self.onLoginClick))
        end
    end
    local registerBtnCom = ComUtil.FindChildComByList(UnityEngine.UI.Button, "bgImg:rejisterBtn", self.gameObject.transform)
    if registerBtnCom then
        registerBtnCom.onClick:AddListener(handler(self, self.onRegisterEvent))
    end
end

function LoginPanel:Start()
    log("LuaPrint: Start")
end

function LoginPanel:UpDate()
    log("LuaPrint: UpDate")
end

--desc: 基类中调用
function LoginPanel:OnExit()

end

function LoginPanel:onLoginClick()
    log("LoginPanel:onLoginClick")
end

function LoginPanel:onRegisterEvent()
    ComUtil.GoUI("prefabs/game/LoginPanel/RegisterPanel.prefab")
end

function LoginPanel:OnConnect(event, msgId, byteBuff)
    -- 发送登录信息到服务端
    local byteBuffer = cs.ByteBuffer.New()
    byteBuffer:WriteInt(111111)
    byteBuffer:WriteString("wsw19920926")
    NetModel.Send(Protocal.Login, byteBuffer)
end

function LoginPanel:OnLogin(event, msgId, byteBuff)
    local passWord = byteBuff:ReadString()
    log("LoginPanel:OnLogin(event, msgId, byteBuf)--->>>"..passWord)
end

return LoginPanel