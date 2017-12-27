﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class VisowFrameWork_LuaComponentGroupWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(VisowFrameWork.LuaComponentGroup), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("GetLuaComponent", GetLuaComponent);
		L.RegFunction("CreateLuaComponent", CreateLuaComponent);
		L.RegFunction("Focus", Focus);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("luaComponenetName", get_luaComponenetName, set_luaComponenetName);
		L.RegVar("luaComponenetList", get_luaComponenetList, set_luaComponenetList);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLuaComponent(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			VisowFrameWork.LuaComponentGroup obj = (VisowFrameWork.LuaComponentGroup)ToLua.CheckObject<VisowFrameWork.LuaComponentGroup>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			VisowFrameWork.LuaComponent o = obj.GetLuaComponent(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateLuaComponent(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			VisowFrameWork.LuaComponent o = VisowFrameWork.LuaComponentGroup.CreateLuaComponent(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Focus(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			VisowFrameWork.LuaComponentGroup obj = (VisowFrameWork.LuaComponentGroup)ToLua.CheckObject<VisowFrameWork.LuaComponentGroup>(L, 1);
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.Focus(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_luaComponenetName(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			VisowFrameWork.LuaComponentGroup obj = (VisowFrameWork.LuaComponentGroup)o;
			System.Collections.Generic.List<string> ret = obj.luaComponenetName;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index luaComponenetName on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_luaComponenetList(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			VisowFrameWork.LuaComponentGroup obj = (VisowFrameWork.LuaComponentGroup)o;
			System.Collections.Generic.List<VisowFrameWork.LuaComponent> ret = obj.luaComponenetList;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index luaComponenetList on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_luaComponenetName(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			VisowFrameWork.LuaComponentGroup obj = (VisowFrameWork.LuaComponentGroup)o;
			System.Collections.Generic.List<string> arg0 = (System.Collections.Generic.List<string>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.List<string>));
			obj.luaComponenetName = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index luaComponenetName on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_luaComponenetList(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			VisowFrameWork.LuaComponentGroup obj = (VisowFrameWork.LuaComponentGroup)o;
			System.Collections.Generic.List<VisowFrameWork.LuaComponent> arg0 = (System.Collections.Generic.List<VisowFrameWork.LuaComponent>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.List<VisowFrameWork.LuaComponent>));
			obj.luaComponenetList = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index luaComponenetList on a nil value");
		}
	}
}

