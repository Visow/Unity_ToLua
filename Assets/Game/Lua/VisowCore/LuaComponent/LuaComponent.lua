
--[[
    ## unity core for visow
    ##  param config
    ## ViewBase.lua
    ## for prefabs behaviour by lua
    ## create time by 2017/8/17 17:12
]]
------------------------------------------------------------------------------
------------------------------------------------------------------------------

local LuaComponentFunction = {
    Awake = 0,
    Start = 1,
	OnDestroy = 2,
    Focus = 3,
    Enable = 4,
}

--==============================--
--desc: C# 事件回调句柄
--time:2017-08-24 03:19:32
--@instance:
--@funcType:
--@opt:
--@return 
--==============================--
local function LifecycleHandler(instance, funcType, opt)
    if funcType == LuaComponentFunction.Awake then
        if LuaComponent.Awake then
            instance:Awake(opt)
        end
    elseif funcType == LuaComponentFunction.Start then
        if LuaComponent.Start then
            instance:Start()
        end
    elseif funcType == LuaComponentFunction.OnDestroy then
        if LuaComponent.OnDestroy then
            instance:OnDestroy()
        end
    elseif funcType == LuaComponentFunction.Focus then
        if LuaComponent.Focus then
            instance:Focus(opt)
        end
    elseif funcType == LuaComponentFunction.Enable then
        if LuaComponent.Enable then
            instance:Enable()
        end
    end
end

--==============================--
--desc:C#调用, 创建LuaComponent组件
--time:2017-08-24 03:19:50
--@modelName:
--@return 
--==============================--
function cs.exports.CreateLuaComponent(modelName)
    local status, cls = xpcall(function()
            return require(modelName)
        end, function(msg)
        if not string.find(msg, string.format("'%s' not found:", modelName)) then
            print("load luacomponent error: ", msg)
        end
    end)
    if not  status then return end
    local bLuaComponent = false
    local __super = cls.__supers
    for _, super in pairs(__super) do
        if super.__cname == "LuaComponent" then
            bLuaComponent = true
            break
        end
    end
    if not bLuaComponent then return end

    cls.__enablecustomnew = true
    local instance = cls.__create()
    local peer = tolua.getpeer(instance)
    if not peer then
        peer = {}
    end

    peer.__index = cls
    setmetatable(peer, cls)
    tolua.setpeer(instance, peer)
    instance.class = cls
    instance:Ctor()
    return instance
end

--==============================--
--desc: LuaComponenet 构造
--time:2017-08-24 03:20:14
--@args:
--@return 
--==============================--
local function newLuaComponent(...)
    local args = {...}
    local component = cs.LuaComponent.New()
    if not component then return end
    component:SetHandler(function(instance, funcType, opt)
        LifecycleHandler(instance, funcType, opt)
    end)
    return component
end

-- 类声明
cs.exports.LuaComponent = class("LuaComponent", newLuaComponent)
function LuaComponent:Ctor(...)
    log("LuaComponent:Ctor...")
end

function LuaComponent:Awake(opt)

end

--==============================--
--desc: 作为启动参数接口
--time:2017-12-14 05:18:18
--@args:
--@return 
--==============================--
function LuaComponent:StartByArgs(...)
    self.startArgs = {...}
end

function LuaComponent:GetStartArg(index)
    return self.startArgs[index]
end

function LuaComponent:Start(opt)

end

function LuaComponent:OnExit()

end

function LuaComponent:OnDestroy(opt)
    log(self.__cname.."============>OnDestroy")
    EventCenter.Destroy(self)
    self:OnExit()
end

function LuaComponent:GetComponent(type)
    return self.gameObject:GetComponent(typeof(type))
end


function LuaComponent:Focus(opt)

end

function LuaComponent:Enable(opt)

end