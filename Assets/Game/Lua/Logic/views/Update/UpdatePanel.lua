
--[[
    ## unity core for visow
    ##  param config
    ## ViewBase.lua
    ## for prefabs behaviour by lua
    ## create time by 2017/8/17 17:12
]]
------------------------------------------------------------------------------
------------------------------------------------------------------------------
cs.exports.UpdatePanel = class("UpdatePanel", LuaComponent)

local EventID  = { 
    LoadVersionInfo = 0,    -- 版本信息加载结束
    CheckVersion = 1,
    DownLoad = 2,
    Completed = 3,
    Error = 4,
}

function UpdatePanel:Ctor(...)
    self.updateManager = cs.Util.UpdateManager()
    self.updateManager:Prepare(handler(self, self.OnUpdateManagerCallBack))
    LuaComponent.Ctor(self)
end

function UpdatePanel:Awake()
    LuaComponent.Awake(self)

end

function UpdatePanel:Start()
    LuaComponent.Start(self)
    self.updateManager:StartGetVersionInfo()
    self:SetMsg("获取版本信息中......")
  
end

function UpdatePanel:OnUpdateManagerCallBack(eventID, opt1, opt2, opt3, opt4)
    if eventID == EventID.LoadVersionInfo then
        self.updateManager:StartCheckVersion()
    elseif eventID == EventID.CheckVersion then
        local total = opt1
        local idx = opt2
        self:SetCheckProcess(total, idx)
    elseif eventID == EventID.DownLoad then
        local total = opt1
        local idx = opt2
        self:SetDownLoadProcess(total, idx)
    elseif eventID == EventID.Completed then

    elseif eventID == EventID.Error then
        log(self.__cname.."====================>更新失败,"..opt1)
    end
end

function UpdatePanel:SetMsg(str)
    local textTf = FindChildTfByList( "text", self.gameObject.transform)
    if textTf then
        local textCt = textTf:GetComponent(typeof(UnityEngine.UI.Text))
        if textCt then
            textCt.text = str
        end
    end
end

function UpdatePanel:SetCheckProcess(total, idx)
    self:SetMsg("资源比对中:"..idx.."/"..total)
    local processTf = FindChildTfByList("progressBar", self.gameObject.transform)
    if processTf then
        local processCt = processTf:GetComponent(typeof(cs.UIProgressBar))
        if processCt then
            local value = (idx * 100)/total;
            processCt.Percent = value
        end
    end
    if total == idx then
        self.updateManager:StartDownLoad()
    end
end

function UpdatePanel:SetDownLoadProcess(total, idx)
    self:SetMsg("资源下载中:"..idx.."/"..total)
    local processTf = FindChildTfByList("progressBar", self.gameObject.transform)
    if processTf then
        local processCt = processTf:GetComponent(typeof(cs.UIProgressBar))
        if processCt then
            local value = (idx * 100)/total;
            processCt.Percent = value
        end
    end
    if total == idx then
        -- 资源下载完成, 重新启动lua虚拟机.
        cs.Util.UpdateManager():StartGame()
    end
end

return UpdatePanel