
--[[
    ## unity core for visow
    ##  param config
    ## ViewBase.lua
    ## for prefabs behaviour by lua
    ## create time by 2017/8/17 17:12
]]
------------------------------------------------------------------------------
------------------------------------------------------------------------------
local LoadingPanel = class("LoadingPanel", LuaComponent)

function LoadingPanel:Ctor(...)
    LuaComponent.Ctor(self, ...)
end

function LoadingPanel:Awake()
    LoadingManager:LoadSceneByName("Home", handler(self, self.onProcess), handler(self, self.onComplete), function()
        ComUtil.GoUI("prefabs/game/battle/BattlePanel.prefab")
        ComUtil.GoUI("prefabs/game/HomePanel/HomePanel.prefab")
    end)
end

function LoadingPanel:Start()

end

function LoadingPanel:onProcess(nVar)
    local textTf = ComUtil.FindChildTfByList( "text", self.gameObject.transform)
    if textTf then
        local textCt = textTf:GetComponent(typeof(UnityEngine.UI.Text))
        if textCt then
            textCt.text = "加载场景中 >>"..nVar.."%"
        end
    end
    local processTf = ComUtil.FindChildTfByList("processBar", self.gameObject.transform)
    if processTf then
        local processCt = processTf:GetComponent(typeof(cs.UIProgressBar))
        if processCt then
            processCt.Percent = nVar
        end
    end
end

function LoadingPanel:onComplete(sName)
    self:onProcess(100)
end

return LoadingPanel