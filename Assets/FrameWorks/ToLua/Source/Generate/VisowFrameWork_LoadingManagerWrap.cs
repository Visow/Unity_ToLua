﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class VisowFrameWork_LoadingManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(VisowFrameWork.LoadingManager), typeof(VisowFrameWork.ManagerBase));
		L.RegFunction("GetDisplayProgress", GetDisplayProgress);
		L.RegFunction("LoadSceneByName", LoadSceneByName);
		L.RegFunction("LoadSceneAddtiveByName", LoadSceneAddtiveByName);
		L.RegFunction("UnloadSceneByName", UnloadSceneByName);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDisplayProgress(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			VisowFrameWork.LoadingManager obj = (VisowFrameWork.LoadingManager)ToLua.CheckObject<VisowFrameWork.LoadingManager>(L, 1);
			int o = obj.GetDisplayProgress();
			LuaDLL.lua_pushinteger(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadSceneByName(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				VisowFrameWork.LoadingManager obj = (VisowFrameWork.LoadingManager)ToLua.CheckObject<VisowFrameWork.LoadingManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				obj.LoadSceneByName(arg0);
				return 0;
			}
			else if (count == 3)
			{
				VisowFrameWork.LoadingManager obj = (VisowFrameWork.LoadingManager)ToLua.CheckObject<VisowFrameWork.LoadingManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				LuaFunction arg1 = ToLua.CheckLuaFunction(L, 3);
				obj.LoadSceneByName(arg0, arg1);
				return 0;
			}
			else if (count == 4)
			{
				VisowFrameWork.LoadingManager obj = (VisowFrameWork.LoadingManager)ToLua.CheckObject<VisowFrameWork.LoadingManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				LuaFunction arg1 = ToLua.CheckLuaFunction(L, 3);
				LuaFunction arg2 = ToLua.CheckLuaFunction(L, 4);
				obj.LoadSceneByName(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 5)
			{
				VisowFrameWork.LoadingManager obj = (VisowFrameWork.LoadingManager)ToLua.CheckObject<VisowFrameWork.LoadingManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				LuaFunction arg1 = ToLua.CheckLuaFunction(L, 3);
				LuaFunction arg2 = ToLua.CheckLuaFunction(L, 4);
				LuaFunction arg3 = ToLua.CheckLuaFunction(L, 5);
				obj.LoadSceneByName(arg0, arg1, arg2, arg3);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: VisowFrameWork.LoadingManager.LoadSceneByName");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadSceneAddtiveByName(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			VisowFrameWork.LoadingManager obj = (VisowFrameWork.LoadingManager)ToLua.CheckObject<VisowFrameWork.LoadingManager>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			obj.LoadSceneAddtiveByName(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnloadSceneByName(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			VisowFrameWork.LoadingManager obj = (VisowFrameWork.LoadingManager)ToLua.CheckObject<VisowFrameWork.LoadingManager>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			obj.UnloadSceneByName(arg0);
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
}
