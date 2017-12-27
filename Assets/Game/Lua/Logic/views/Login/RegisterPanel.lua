
--[[
    ## unity core for visow
    ##  param config
    ## ViewBase.lua
    ## for prefabs behaviour by lua
    ## create time by #DataTime.New#
]]
------------------------------------------------------------------------------
------------------------------------------------------------------------------
local RegisterPanel = class("RegisterPanel", LuaComponent)

function RegisterPanel:Ctor(...)
	LuaComponent.Ctor(self, ...)
end

function RegisterPanel:Awake()
    self.accountInputCom = ComUtil.FindChildComByList(UnityEngine.UI.InputField, "bgImg:accountInput", self.gameObject.transform)
    if not self.accountInputCom then return end
    self.passwordInputCom = ComUtil.FindChildComByList(UnityEngine.UI.InputField, "bgImg:passwordInput", self.gameObject.transform)
    if self.passwordInputCom == nil then return end
    self.repaswdInputCom = ComUtil.FindChildComByList(UnityEngine.UI.InputField, "bgImg:repaswdInput", self.gameObject.transform)
    if not self.repaswdInputCom then return end
    self.sureBtn = ComUtil.FindChildComByList(UnityEngine.UI.Button, "bgImg:sureBtn", self.gameObject.transform)
    if not self.sureBtn then return end
    self.sureBtn.onClick:AddListener(handler(self, self.OnSureClick))
    self.closeBtn = ComUtil.FindChildComByList(UnityEngine.UI.Button, "bgImg:closeBtn", self.gameObject.transform)
    if self.closeBtn then
        self.closeBtn.onClick:AddListener(function()
            destroy(self.gameObject)
        end)
    end
end

function RegisterPanel:Start()

end

function RegisterPanel:OnExit()
    if self.co then
        self.co:stop()
        self.co = nil
    end
    self.coArgs = nil
end

function RegisterPanel:OnSureClick()
    local account = self.accountInputCom.text
    local lenAccount = string.len(account)
    if string.BCN(account) then
        UView.Tips(L(1001))
        return
    end
    if string.BSymbol(account) then
        UView.Tips(L(1002))
        return
    end
    if lenAccount < 6 or lenAccount > 11 then
        UView.Tips(L(1003))
        return
    end
    local passwold = self.passwordInputCom.text
    local lenPassword = string.len(passwold)
    if lenPassword < 6 or lenPassword > 11 then
        UView.Tips(L(1004))
        return
    end
    if passwold ~= self.repaswdInputCom.text then
        UView.Tips(L(1005))
        return
    end
    self.coArgs = {account, passwold}
    self.co = coroutine.start(handler(self, self.RegisterAccout))
end

function RegisterPanel:RegisterAccout()
    local www = UnityEngine.WWW("http://192.168.2.243:6689/register_account?account="..self.coArgs[1].."&password="..self.coArgs[2])
    coroutine.www(www);
    log(www.text);
    local text = www.text
    local result = App.Json.decode(www.text)
    if result.errcode ~= 0 then
        UView.Tips(result.errmsg)
    else
        -- 账号注册成功
        UnityEngine.PlayerPrefs:SetString("account", self.coArgs[1])
        UnityEngine.PlayerPrefs:SetString("password", self.coArgs[2])
        -- 通知注册成功
    end
    self.co = nil
end

return RegisterPanel