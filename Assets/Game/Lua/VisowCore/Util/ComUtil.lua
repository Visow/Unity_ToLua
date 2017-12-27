
cs.exports.ComUtil = {}

--==============================--
--desc: 获取Transform
--time:2017-12-12 09:15:14
--@str:
--@parentTf:
--@return 
--==============================--
function ComUtil.FindChildTfByList(str, parentTf)
    local nameList = string.split(str, ":")
    if parentTf then
        local objtf = parentTf
        for i = 1, #nameList do
            objtf = objtf:Find(nameList[i])
            if objtf == nil then
                return nil
            end
        end
        return objtf
    else
        str = string.gsub(str, ":", "/")
        return child(str)
    end
end

--==============================--
--desc: 获取Component
--time:2017-12-12 09:14:59
--@objectType:
--@nameList:
--@parent:
--@return 
--==============================--
function ComUtil.FindChildComByList(objectType, nameList, parent)
    local objTf = ComUtil.FindChildTfByList(nameList, parent);
    if objTf then
        return objTf:GetComponent(typeof(objectType))
    end
    return nil
end

--==============================--
--desc:实例化预制件
--time:2017-12-12 09:14:54
--@resPath:
--@parent:
--@return 
--==============================--
function ComUtil.GoUI(resPath, parent)
    local prefab = ResManager:GetPrefab(resPath)
    if prefab then
        local go = UnityEngine.GameObject.Instantiate(prefab)
        if go then
            local goRf = go:GetComponent(typeof(UnityEngine.RectTransform))
            if parent then
                goRf:SetParent(parent:GetComponent(typeof(UnityEngine.RectTransform)), false)
            else
                goRf:SetParent(UIRootManager.UIRoot:GetComponent(typeof(UnityEngine.RectTransform)), false)
            end
            return go
        end
    end
end

--==============================--
--desc:实例化UI预制件,并且获取LuaGroup组件
--time:2017-12-12 09:14:48
--@resPath:
--@parent:
--@args:
--@return 
--==============================--
function ComUtil.LuaUI(resPath, parent, ...)
    local go = ComUtil.GoUI(resPath, parent, ...)
    if go == nil then return end
    local luaComGroup = go:GetComponent(typeof(cs.LuaComponentGroup))
    if luaComGroup then
        return luaComGroup
    end
end

function ComUtil.LuaUICom(resPath, parent, modelName, ...)
    local luaComGroup = ComUtil.LuaUI(resPath, parent, ...)
    if luaComGroup then
        local luaCom = luaComGroup:GetLuaComponent(modelName)
        if luaCom then
            luaCom:StartByArgs(...)
            return luaCom
        end
    end
end

-- 平面夹角计算
function ComUtil.V2Angle(p1,p2)  
    local p = {}  
    p.x = p2.x - p1.x  
    p.y = p2.y - p1.y           
    local r = math.atan2(p.y,p.x)*180/math.pi  
    return r  
end 

