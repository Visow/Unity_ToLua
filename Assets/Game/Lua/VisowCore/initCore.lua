
--[[
    ## unity core for visow
    ## base form LuaFrameWork_UGUI
    ## create time by 2017/8/17 17:12
]]
------------------------------------------------------------------------------
------------------------------------------------------------------------------

__G__TRACKBACK__ = function(msg)
    local msg = debug.traceback(msg, 3)
    print(msg)
    return msg
end

-- load framework lua files
require "VisowCore.functions"
require "VisowCore.App"


-- export global variable
local __g = _G
cs.exports = {}
setmetatable(cs.exports, {
    __newindex = function(_, name, value)
        rawset(__g, name, value)
    end,

    __index = function(_, name)
        return rawget(__g, name)
    end
})

-- disable create unexpected global variable
function cs.disable_global()
    setmetatable(__g, {
        __newindex = function(_, name, value)
            error(string.format("USE \" cs.exports.%s = value \" INSTEAD OF SET GLOBAL VARIABLE", name), 0)
        end
    })
end

if App.Config.CC_DISABLE_GLOBAL then
    cs.disable_global()
end