-----------------------------------------------------------------------------------------------
--@auto: visow
--@file: Mask.lua
--@desc: 位操作
--@time: 2017/4/16
-----------------------------------------------------------------------------------------------

cs.exports._MASK_ = function(flag)
    return bit.lshift(0x01, flag)
end

cs.exports.Mask = {}

function Mask.HasAny(flag, mask)
    return bit.band(flag, mask) ~= 0
end

function Mask.HasAll(flag, mask)
    return bit.band(flag, mask) == mask
end

function Mask.Add(flag, mask)
    return bit._or(flag, mask)
end

function Mask.Del(flag, mask)
    return bit.band(flag, bit.bnot(mask))
end

function Mask.remove(flag, mask)
    return bit.band(flag, bit.bnot(mask))
end

function Mask.IsAdd(oldflag, newflag, mask)
    return not bit.HasAny(oldflag, mask) and bit.HasAny(newflag, mask)
end

function Mask.IsDel(oldflag, newflag, mask)
    return bit.HasAny(oldflag, mask) and not bit.HasAny(newflag, mask)
end