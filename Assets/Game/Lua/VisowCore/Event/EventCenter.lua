-----------------------------------------------------------------------------------------------
--@auto: visow
--@file: EventCenter.lua
--@desc: 事件中心
--@time: 2017/4/16
-----------------------------------------------------------------------------------------------
require "VisowCore.Event.NetEvent"
cs.exports.EventCenter = class("EventCenter")

local s_instance = nil
function EventCenter.getInstance()
    if not s_instance then
        s_instance = EventCenter.New()
    end
    return s_instance
end

function EventCenter:Ctor()
    self.clientEventList= {}
    self.netEventList = {}
    self.objectMap = {}
    self:initNetEvent()
end

function EventCenter:bNetEventExits(msgId, object)
    if not self.netEventList[msgId] then return end
    for k,event in pairs(self.netEventList[msgId]) do
        if event:get_obj() == object then
            return k
        end
    end
end

function EventCenter:registerTCPSocket(msgId, object, callBack, priority)
    self.netEventList[msgId] = self.netEventList[msgId] or {}
    self.objectMap[object] = self.objectMap[object] or {}

    if self:bNetEventExits(msgId, object) then return end
    local netEvent = NetEvent.New()
    netEvent:set_msgId(msgId)
    netEvent:set_obj(object)
    netEvent:set_callBack(callBack)
    priority = priority or 99999
    netEvent:set_priority(priority)
    table.insert( self.netEventList[msgId], netEvent)
    table.sort( self.netEventList[msgId], function(a, b) 
        return a:get_priority() < b:get_priority()
    end)
    table.insert(self.objectMap[object], netEvent)
end

function EventCenter:removeTCKSocket(msgId, object)
    local idx = self:bNetEventExits(msgId, object)
    if not idx then return end
    for k, objEvent in pairs(self.objectMap[object]) do
        if objEvent.__cname and objEvent.__cname == "NetEvent" then
            if objEvent:get_msgId() == msgId then
                table.remove(self.objectMap[object], k)
                break
            end
        end
    end
    table.remove(self.netEventList[msgId], idx)
    if #self.netEventList[msgId] == nil then
        self.netEventList[msgId] = nil
    end
end

function EventCenter:bClientEventExits(msgId, object)
    if not self.clientEventList[msgId] then return end
    for k, clientEvent in pairs(self.clientEventList[msgId]) do
        if clientEvent:get_obj() == object then
            return k
        end
    end
end

function EventCenter:registerClient(msgId, object, callBack, priority)
    self.clientEventList[msgId] = self.clientEventList[msgId] or {}
    self.objectMap[object] = self.objectMap[object] or {}
    if self:bClientEventExits(msgId, object) then return end
    local clientEvent = ClientEvent.New(msgId, object)
    clientEvent:set_eventId(msgId)
    clientEvent:set_obj(object)
    clientEvent:set_callBack(callBack)
    priority = priority or 99999
    table.insert( self.clientEventList[msgId], clientEvent)
    table.sort( self.clientEventList[msgId], function(a, b) 
        return a:get_priority() < b:get_priority()
    end)
    table.insert(self.objectMap[object], clientEvent)
end

function EventCenter:removeClientEvent(msgId, object)
    if not self.clientEventList[msgId] then return end
    local idx = self:bClientEventExits(msgId, object)
    if not idx then return end
    for k, event in pairs(self.objectMap[object]) do
        if event.__cname and event.__cname == "ClientEvent" then
            if event:get_eventId() == msgId  then
                table.remove(self.objectMap[object], k)
                break
            end
        end
    end
    table.remove(self.clientEventList[msgId], idx)
    if #self.clientEventList[msgId] == 0 then
        self.clientEventList[msgId] = nil
    end
end

function EventCenter:removeObject(object)
    if not object then return end
    if not self.objectMap[object] then return end
    local removeNetEvent = {}
    local removeClientEventList = {}
    for k, event in pairs(self.objectMap[object]) do
        if event and event.__cname and event.__cname == "NetEvent" then
            table.insert( removeNetEvent, event )
        elseif event and event.__cname and event.__cname == "ClientEvent" then
            table.insert(removeClientEventList, event)
        end
    end
    for k,event in pairs(removeNetEvent) do
        self:removeTCKSocket(event:get_msgId(), event:get_sub(), event:get_obj())
    end
    for k,event in pairs(removeClientEventList) do
        self:removeClientEvent(event:get_eventId(), event:get_obj())
    end
    self.objectMap[object] = nil
end

function EventCenter:initNetEvent()
    cs.Util.NetWorkManager():setLuaDelete(handler(self, self.onRecvMsg))
end

function EventCenter:onRecvMsg(msgId, byteBuff)
    log("EventCenter------------------->"..msgId)
    if not self.netEventList[msgId] then return end
    for i = 1, #self.netEventList[msgId] do
        local netEvent = self.netEventList[msgId][i]
        local callBack = netEvent:get_callBack()
        if type(callBack) == "function" then
            local protoType = byteBuff:ReadByte()   -- 消息模式
            netEvent:set_data(byteBuff)
            callBack(netEvent:get_obj(), netEvent, msgId, byteBuff)
        end
    end
end

function EventCenter:onPostClientEvent(msgId, ...)
    if not self.clientEventList[msgId] then return end
    for i = #self.clientEventList[msgId], 1, -1 do
        local curEvent = self.clientEventList[msgId][i]
        local callBack = curEvent:get_callBack()
        if type(callBack) == "function" then
            curEvent:set_eventArgs({...})
            callBack(curEvent:get_obj(), curEvent, ...)
        end
    end
end

function EventCenter.addTCP(msgId, object, callBack, priority)
    EventCenter.getInstance():registerTCPSocket(msgId, object, callBack, priority)
end

function EventCenter.addClient(msgId, object, callBack, priority)
    EventCenter.getInstance():registerClient(msgId, object, callBack, priority)
end

function EventCenter.Destroy(object)
    EventCenter.getInstance():removeObject(object)
end

function EventCenter.postClient(msgId, ...)
    EventCenter.getInstance():onPostClientEvent(msgId, ...)
end


