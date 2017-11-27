-----------------------------------------------------------------------------------------------
--@auto: visow
--@file: NetEvent.lua
--@desc: 玩法界面
--@time: 2017/4/16
-----------------------------------------------------------------------------------------------
require "VisowCore.Event.ClientEvent"

cs.exports.NetEvent = class("NetEvent", ClientEvent)

function NetEvent:Ctor()
    ClientEvent.Ctor(self)
    self.msgId = "autofunc"
    self.sub = "autofunc"
    self.data = "autofunc"
    self.dataSize = "autofunc"

    for k,v in pairs(self) do
        if type(v) == "string" and v == "autofunc" then
            self["get_"..k] = function(self)
                if self[k] == 0 then
                    return nil
                end
                return self[k]
            end
            self["set_"..k] = function(self, value)
                self[k] = value
            end
        end
    end
end