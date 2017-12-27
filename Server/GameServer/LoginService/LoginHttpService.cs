using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using LibServer.Service;
using LibServer.Common;
using LibServer.DBBase;

using MySql.Data.MySqlClient;
using Newtonsoft.Json;

class HttpTread : BaseThread
{
    public delegate void BaseThreadDelegate();
    BaseThreadDelegate _listenning;

    public void StartByDelegate(BaseThreadDelegate listen)
    {
        _listenning = listen;
        Start();
    }

    public override void run()
    {
        _listenning();
        IsRuning = false;
    }
}

public class LoginService : HttpService
{
    HttpTread thread = new HttpTread();
    DBThread _DBHandler = new DBThread();
    public LoginService(int port)
    {
        host = "http://*:" + port + "/";
        //_DBHandler.setConnString(~~~~);
    }

    public void Start()
    {
        thread.StartByDelegate(Listen);
    }

    public new void Stop()
    {
        base.Stop();
    }

    public override void OnGetRequest(HttpListenerRequest request, HttpListenerResponse response)
    {
        string[] context = request.RawUrl.Split('?');
        if (context.Length != 2)
        {
            RES_Common res = new RES_Common();
            res.errcode = 1;
            res.errmsg = "请求的URL不存在,或者参数错误";
            send(JsonConvert.SerializeObject(res), response);
            return;
        }

        switch (context[0])
        { 
            case "/login_account" :
                login_account(request, response);
                return;
            case "/register_account" :
                register_account(request, response);
                return;
            default:
                break;
        }
        Console.WriteLine("Get request: {0}", request.Url);
    }

    public override void OnPostRequest(HttpListenerRequest request, HttpListenerResponse response)
    {
        Console.WriteLine("POST request: {0}", request.Url);
    }

    private void login_account(HttpListenerRequest request, HttpListenerResponse response)
    {
        string account = request.QueryString["account"];
        string password = request.QueryString["password"];
        if (account == null || password == null)
        {
            RES_Common res = new RES_Common();
            res.errcode = 1;
            res.errmsg = "账号或密码不存在";
            send(JsonConvert.SerializeObject(res), response);
            return;
        }

        CustomArgs args = new CustomArgs();
        args.AddParam("response", response);
        args.AddParam("account", account);
        args.AddParam("password", password);
        _DBHandler.addTask(db_login_account, args);
    }

    private void register_account(HttpListenerRequest request, HttpListenerResponse response)
    {
        string respone_str = String.Empty;
        string account = request.QueryString["account"];
        string password = request.QueryString["password"];
        if (account == null || password == null)
        {
            Dictionary<string, object> body = new Dictionary<string,object>();
            body.Add("err_str", "account or password is null");
            RES_Common res = new RES_Common();
            res.errcode = 1;
            res.errmsg = "account or password is null";
            respone_str = JsonConvert.SerializeObject(respone_str);
            send(respone_str, response);
            return;
        }

        CustomArgs args = new CustomArgs();
        args.AddParam("response", response);
        args.AddParam("account", account);
        args.AddParam("password", password);
        _DBHandler.addTask(db_register_account, args);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="args"></param>
    public void db_login_account(MySqlConnection conn, CustomArgs args)
    {
        HttpListenerResponse response = args.GetParam("response") as HttpListenerResponse;
        string account = args.GetParam("account") as string;
        string password = args.GetParam("password") as string;
        if (response == null)
            return;
        DBStoredProcedCmd cmd = new DBStoredProcedCmd("PRO_LOGIN_ACCOUNT", conn);
        cmd.AddParamVChar("account", account, account.Length);
        cmd.AddParamVChar("password", password, password.Length);
        string md5 = LibServer.Utility.Security.MD5(account + password + DateTime.Now.ToString());
        cmd.AddParamVChar("token", md5, md5.Length);
        cmd.AddOutParamInt("errcode");
        cmd.AddOutParamText("errmsg", LOGIN_DEFINE.VAR_CHAR_LENGHT_255);
        int bsuc = cmd.Execute();
        if (bsuc == 0)
        {
            int nErrCode = (int)cmd.GetParam("errcode").Value;
            string err_string = cmd.GetParam("errmsg").Value as string;
            if (nErrCode != 0)
            {
                RES_Common res = new RES_Common();
                res.errcode = nErrCode;
                res.errmsg = err_string;
                send(JsonConvert.SerializeObject(res), response);
            }
            else
            {
                // 账号密码校验成功, 返回登录token
                RES_LoginSuc res = new RES_LoginSuc();
                res.errcode = 0;
                res.errmsg = "";
                res.token = cmd.GetValue(0, "token") as string; ;
                res.tokenstamp = (System.DateTime)cmd.GetValue(0, "tokenstamp");
                send(JsonConvert.SerializeObject(res), response);
            }
        }
        else
            throw new Exception("执行存储过程 PRO_LOGIN_ACCOUNT 失败!");
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="args"></param>
    public void db_register_account(MySqlConnection conn, CustomArgs args)
    {
        HttpListenerResponse response = args.GetParam("response") as HttpListenerResponse;
        string account = args.GetParam("account") as string;
        string password = args.GetParam("password") as string;
        DBStoredProcedCmd cmd = new DBStoredProcedCmd("PRO_REGISTER_ACCOUNT", conn);
        cmd.AddParamVChar("account", account, account.Length);
        cmd.AddParamVChar("password", password, password.Length);
        string md5 = LibServer.Utility.Security.MD5(account + password + DateTime.Now.ToString());
        cmd.AddParamVChar("token", md5, md5.Length);
        cmd.AddOutParamInt("errcode");
        cmd.AddOutParamText("errmsg", LOGIN_DEFINE.VAR_CHAR_LENGHT_255);
        int bsuc = cmd.Execute();
        if (bsuc == 0)
        {
            RES_Common res = new RES_Common();
            res.errcode = (int)cmd.GetParam("errcode").Value;
            
            res.errmsg = cmd.GetParam("errmsg").Value as string;

            send(JsonConvert.SerializeObject(res), response);
            return;
            // 注册成功;
        }
        else
            throw new Exception("执行存储过程 PRO_REGISTER_ACCOUNT 失败!");
    }

    public void send(string res, HttpListenerResponse response)
    {
        try
        {
            response.ContentLength64 = Encoding.UTF8.GetByteCount(res);
            response.ContentType = "text/html; charset=UTF-8";
        }
        finally
        {
            Stream output = response.OutputStream;
            StreamWriter writer = new StreamWriter(output);
            writer.Write(res);
            Console.WriteLine("send response msg: >>>>>" + res );
            Console.WriteLine(response.ToString());
            writer.Close();
        }
    }
}