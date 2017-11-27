
--------------------------------------------------------------------------------------------------
-- luaIde 调试代码
local breakSocketHandle,debugXpCall = require("LuaDebugjit")("localhost",7003)
local timer = Timer.New(function() 
breakSocketHandle() end, 1, -1, false)
timer:Start();

---------------------------------------------------------------------------------------------------

require "LoadScript"


local function main()
    log("Main: 更新结束")
    -- 资源管理器
    cs.exports.ResManager = cs.Util.ResManager()
    -- UI管理器
    cs.exports.UIRootManager = cs.Util.UIRoot()

    -- 事件管理器
    EventCenter.getInstance()

    cs.exports.NetWorkManager = cs.Util.NetWorkManager()
    cs.exports.LoadingManager = cs.Util.LoadingManager()

    -- 加载登录界面
    local updatePrefab = ResManager:GetPrefab("Prefabs/Game/LoginPanel/LoginPanel.prefab")
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
    print("main: "..msg)
end
