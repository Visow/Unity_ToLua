-- Main 主体

require "LoadScript"

local function main()
    log("PreMain: 开始更新")
    -- 初始化资源管理器
    cs.exports.ResManager = cs.Util.ResManager()
    -- 初始化UI管理器
    cs.exports.UIRootManager = cs.Util.UIRoot()

    -- 加载更新界面
    local updatePrefab = ResManager:GetPrefab("prefabs/game/UpdatePanel/UpdatePanel.prefab")
    if updatePrefab then
        local updatePanel = UnityEngine.GameObject.Instantiate(updatePrefab)
        if updatePanel then
            local updateRf = updatePanel:GetComponent(typeof(UnityEngine.RectTransform))
            updateRf:SetParent(UIRootManager.UIRoot:GetComponent(typeof(UnityEngine.RectTransform)), false)
        end
    end
end

local status, msg = xpcall(main, __G__TRACKBACK__)
if not status then
    print("PreMain: "..msg)
end
