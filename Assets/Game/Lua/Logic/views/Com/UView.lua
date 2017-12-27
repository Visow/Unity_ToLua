cs.exports.UView = {}


function UView.Tips(msg)
    return ComUtil.LuaUICom("prefabs/game/Com/TipCtrl.prefab", nil, "Logic.views.Com.TipsCtrl", msg)
end