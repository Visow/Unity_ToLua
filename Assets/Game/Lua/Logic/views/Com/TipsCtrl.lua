
--[[
    ## unity core for visow
    ##  param config
    ## ViewBase.lua
    ## for prefabs behaviour by lua
    ## create time by #DataTime.New#
]]
------------------------------------------------------------------------------
------------------------------------------------------------------------------
local TipsCtrl = class("TipsCtrl", LuaComponent)

function TipsCtrl:Ctor(...)
    LuaComponent.Ctor(self, ...)
end

function TipsCtrl:Awake()

end

function TipsCtrl:StartByArgs(...)
    LuaComponent.StartByArgs(self, ...)
    local txtCom = ComUtil.FindChildComByList(UnityEngine.UI.Text, "text", self.gameObject.transform)
    if txtCom == nil then return end
    txtCom.text = self:GetStartArg(1)
    self.Rf = self:GetComponent(UnityEngine.RectTransform)
    if self.Rf == nil then return end
    local screenSize = UIRootManager.ScreenSize
    local sequence = DG.Tweening.DOTween.Sequence()
    self.Rf.localPosition = Vector3.New(0, screenSize.y / 2 + self.Rf.sizeDelta.y/ 2, 0) 
    local moveDown = self.Rf:DOLocalMove(Vector3(0, screenSize.y/2 - self.Rf.sizeDelta.y / 2 , 0), 0.3, false)
    -- moveDown:SetEase(DG.Tweening.Ease.OutSine)
    sequence:Append(moveDown)
    sequence:AppendInterval(1)
    local moveUp = self.Rf:DOLocalMove(Vector3(0, screenSize.y / 2 + self.Rf.sizeDelta.y, 0), 0.3, false)
    -- moveUp:SetEase(DG.Tweening.Ease.kOutSine)
    sequence:Append(moveUp)
    sequence:AppendCallback (function()
        destroy(self.gameObject)
    end)
end

function TipsCtrl:Start()

end

function TipsCtrl:OnExit()

end

return TipsCtrl