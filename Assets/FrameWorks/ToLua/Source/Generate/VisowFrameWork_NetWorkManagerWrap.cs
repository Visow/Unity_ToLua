﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class VisowFrameWork_NetWorkManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(VisowFrameWork.NetWorkManager), typeof(VisowFrameWork.ManagerBase));
		L.RegFunction("AddEvent", AddEvent);
		L.RegFunction("SendConnect", SendConnect);
		L.RegFunction("SendMessage", SendMessage);
		L.RegFunction("setLuaDelete", setLuaDelete);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddEvent(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
			VisowFrameWork.ByteBuffer arg1 = (VisowFrameWork.ByteBuffer)ToLua.CheckObject<VisowFrameWork.ByteBuffer>(L, 2);
			VisowFrameWork.NetWorkManager.AddEvent(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendConnect(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			VisowFrameWork.NetWorkManager obj = (VisowFrameWork.NetWorkManager)ToLua.CheckObject<VisowFrameWork.NetWorkManager>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			obj.SendConnect(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendMessage(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes<string>(L, 2))
			{
				VisowFrameWork.NetWorkManager obj = (VisowFrameWork.NetWorkManager)ToLua.CheckObject<VisowFrameWork.NetWorkManager>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				obj.SendMessage(arg0);
				return 0;
			}
			else if (count == 2 && TypeChecker.CheckTypes<VisowFrameWork.ByteBuffer>(L, 2))
			{
				VisowFrameWork.NetWorkManager obj = (VisowFrameWork.NetWorkManager)ToLua.CheckObject<VisowFrameWork.NetWorkManager>(L, 1);
				VisowFrameWork.ByteBuffer arg0 = (VisowFrameWork.ByteBuffer)ToLua.ToObject(L, 2);
				obj.SendMessage(arg0);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes<UnityEngine.SendMessageOptions>(L, 3))
			{
				VisowFrameWork.NetWorkManager obj = (VisowFrameWork.NetWorkManager)ToLua.CheckObject<VisowFrameWork.NetWorkManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				UnityEngine.SendMessageOptions arg1 = (UnityEngine.SendMessageOptions)ToLua.ToObject(L, 3);
				obj.SendMessage(arg0, arg1);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes<object>(L, 3))
			{
				VisowFrameWork.NetWorkManager obj = (VisowFrameWork.NetWorkManager)ToLua.CheckObject<VisowFrameWork.NetWorkManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				object arg1 = ToLua.ToVarObject(L, 3);
				obj.SendMessage(arg0, arg1);
				return 0;
			}
			else if (count == 4)
			{
				VisowFrameWork.NetWorkManager obj = (VisowFrameWork.NetWorkManager)ToLua.CheckObject<VisowFrameWork.NetWorkManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				object arg1 = ToLua.ToVarObject(L, 3);
				UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)ToLua.CheckObject(L, 4, typeof(UnityEngine.SendMessageOptions));
				obj.SendMessage(arg0, arg1, arg2);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: VisowFrameWork.NetWorkManager.SendMessage");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setLuaDelete(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			VisowFrameWork.NetWorkManager obj = (VisowFrameWork.NetWorkManager)ToLua.CheckObject<VisowFrameWork.NetWorkManager>(L, 1);
			LuaFunction arg0 = ToLua.CheckLuaFunction(L, 2);
			obj.setLuaDelete(arg0);
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

