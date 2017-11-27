
cs.exports.ComUtil = {}


-- 实例化预制件
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

