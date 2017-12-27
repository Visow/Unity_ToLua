--region *.lua
--Date
--此文件由[BabeLua]插件自动生成

local Language = class("Language")
Language.pInstance = nil;

function Language:Ctor()
    self.m_tbStr = {};
    self.m_tbStr["1000"] = "测试测试测试{1}哈哈哈哈{2}"
    self.m_tbStr["1001"] = "账号不能有中文"
    self.m_tbStr["1002"] = "账号不能带有特殊字符"
    self.m_tbStr["1003"] = "账号长度不正确,应该在6-11之间"
    self.m_tbStr["1004"] = "密码长度不正确,应该在6-11之间"
    self.m_tbStr["1005"] = "俩次输入的密码不相同"
end

function Language:getStr(sKey, ...)
    local temp = {...};
    local str, index = string.gsub(self.m_tbStr[sKey .. ""], "%{(%d+)}", function(str)
            return temp[str + 0];
        end);
    return str;
end

function Language.getInstance()
    if Language.pInstance == nil then
        Language.pInstance = Language.New();
    end
    return Language.pInstance;
end

function cs.exports.L(sKey, ...)
    return Language.getInstance():getStr(sKey, ...);
end



return Language
--endregion
