
--[[
    ## unity core for visow
    ##  param config
    ## ViewBase.lua
    ## for prefabs behaviour by lua
    ## create time by #DataTime.New#
]]
------------------------------------------------------------------------------
------------------------------------------------------------------------------
local BattlePanel = class("BattlePanel", LuaComponent)

function BattlePanel:Ctor(...)
    LuaComponent.Ctor(self, ...)
end

function BattlePanel:Awake()
    self.yaoGanTf = ComUtil.FindChildTfByList("yaoGan", self.gameObject.transform)
    cs.EventListener.Get(self.yaoGanTf.gameObject).onDragBegin = handler(self, self.onDragBegin)
    cs.EventListener.Get(self.yaoGanTf.gameObject).onDrag = handler(self, self.onDrag)
    cs.EventListener.Get(self.yaoGanTf.gameObject).onDragEnd = handler(self, self.onDragEnd)
    self.centerPos = Vector2(-544, -264)
end

function BattlePanel:Start()

end

function BattlePanel:OnExit()

end

function BattlePanel:onDragBegin(gameObject, eventData)
    local delt = eventData.delta
    local pos = self.yaoGanTf.localPosition
    self.yaoGanTf.localPosition = Vector3(pos.x + delt.x
                                        , pos.y + delt.y
                                        , pos.z)
end

function BattlePanel:onDrag(gameObject, eventData)
    local delt = eventData.delta
    local pos = self.yaoGanTf.localPosition
    local newPos = Vector3(pos.x + delt.x, pos.y + delt.y, pos.z)
    self.yaoGanTf.localPosition = newPos
    local angle = ComUtil.V2Angle(self.centerPos,Vector2(newPos.x, newPos.y))
    ga.FootMan.Instance().Dir = Quaternion.Euler(0, 90 - angle, 0)
end

function BattlePanel:onDragEnd(gameObject, eventData)
    local pos = self.yaoGanTf.localPosition
    self.yaoGanTf.localPosition = Vector3(self.centerPos.x
    , self.centerPos.y
    , pos.z)
end

return BattlePanel