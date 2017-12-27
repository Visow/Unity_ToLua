
--==============================--
--desc: 是否有中文字符
--time:2017-12-12 03:02:48
--@str:
--@return 
--==============================--
function string.BCN(str)
    local width = 0
    for i = 1, string.len(str) do
        local curByte = string.byte(str, i)
        if curByte > 127 then
            return true
        end
    end
end

--==============================--
--desc: 是否有特殊字符
--time:2017-12-12 03:02:38
--@str:
--@return 
--==============================--
function string.BSymbol(str)
    local index = string.find( str, "[^%w_@.]" )
    if index then
        return true
    end
    return false
end


function string.PaserInt(value, count)
    local sub = 10 ^ (count - 1)
    local str = ""
    for i = 0, count - 1 do
        if value / (sub / 10 ^ i) < 1 then
            str = str.."0"
        else
            break
        end
    end
    if value == 0 then return str end
    return str..value
end

function string.PaserStr(str)
    local tmp = {}  
    local tempId = 0;
    for tempId in string.gmatch(str, "%d+") do
        table.insert(tmp,tempId + 0);
    end
    return tmp
end

function string:split(sep)
    local sep, fields = sep or ":", {}  
    local pattern = string.format("([^%s]+)", sep)  
    self:gsub(pattern, function (c) 
                            fields[#fields + 1] = c 
                        end)  
    return fields  
end