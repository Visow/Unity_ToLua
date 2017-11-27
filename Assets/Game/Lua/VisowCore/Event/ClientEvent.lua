-----------------------------------------------------------------------------------------------
--@auto: visow
--@file: Event.lua
--@desc: 事件类型
--@time: 2017/4/16
-----------------------------------------------------------------------------------------------

cs.exports.ClientEvent = class("ClientEvent")

function ClientEvent:Ctor()
    self.eventType = "autofunc"
    self.eventArgs = "autofunc"
    self.eventId = "autofunc"
    self.callBack = nil
    self.obj = "autofunc"
    self.priority = "autofunc"
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

function ClientEvent:set_callBack(callback)
    self.callback = callback
end

function ClientEvent:get_callBack()
    return self.callback
end
