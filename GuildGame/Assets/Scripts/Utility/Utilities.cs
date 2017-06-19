using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class Utilities {

	///DebugLog開關
	public static bool IsDebug = true;

	private static Dictionary<string, object> saveDict = new Dictionary<string, object>(); 

	private static Dictionary<K, V> ToDictionary<K, V> (Hashtable table)
	{
		return table.Cast<DictionaryEntry>().ToDictionary(kvp =>(K)kvp.Key, kvp => (V)kvp.Value);
	}

	///<summary>DebugLog</summary>
	///<remarks>建議使用這裡的DebugLog而不是用Unity的</remarks>
	public static void DebugLog(object log)
	{
		if(IsDebug)
		{
			Debug.Log(log);
		}
	}

	///<summary>讀取存檔</summary>
	///<remarks>帶入key值取得本機對應存檔</remarks>
	public static object LoadSave(string key)
	{
		if(saveDict == null || saveDict.Count <= 0)
		{
			//@TODO 暫時使用Unity存檔 之後會串接steam存檔
			saveDict = ToDictionary<string, object>((Hashtable)MiniJSON.jsonDecode(PlayerPrefs.GetString("Save")));
		}

		if(saveDict.ContainsKey(key))
		{
			return saveDict[key];
		}
		else
		{
			return null;
		}
	}

	///<summary>存檔</summary>
	///<remarks>帶入key值與value存檔</remarks>
	public static void SaveKey(string key, object value)
	{
		if(saveDict == null || saveDict.Count <= 0)
		{
			saveDict = ToDictionary<string, object>((Hashtable)MiniJSON.jsonDecode(PlayerPrefs.GetString("Save")));
		}

		if(saveDict.ContainsKey(key))
		{
			saveDict[key] = value;
		}
		else
		{
			saveDict.Add(key, value);
		}

		string newSave = MiniJSON.jsonEncode(saveDict);

		//@TODO 暫時使用Unity存檔 之後會串接steam存檔
		PlayerPrefs.SetString("Save", newSave);
	}

	//Object轉換成Long
    public static long LoadLong(object value, long defaultVal = 0)
    {
        try {
            if ( value != null ) {
                return Convert.ToInt64(value);
                } else {
                return defaultVal;
            }
            } catch(Exception) {
            return defaultVal;
        }
    }
    
    //Object轉換成Double
    public static double LoadDouble(object value, double defaultVal = 0)
    {
        try {
            if ( value != null ) {
                return Convert.ToDouble(value);
                } else {
                return defaultVal;
            }
            } catch(Exception) {
            return defaultVal;
        }
    }
    
    //Object轉換成Int
    public static int LoadInt(object value, int defaultVal = 0)
    {
        try {
            if ( value != null ) {
                return Convert.ToInt32(value);
                } else {
                return defaultVal;
            }
            } catch(Exception) {
            return defaultVal;
        }
    }
    
    //Object轉換成Float
    public static float LoadFloat(object value, float defaultVal = 0)
    {
        try {
            if ( value != null ) {
                return Convert.ToSingle(value);
                } else {
                return defaultVal;
            }
            } catch(Exception) {
            return defaultVal;
        }
    }
    
    //Object轉換成Bool
    public static bool LoadBool(object value, bool defaultVal = false)
    {
        try {
            if ( value != null ) {
                return Convert.ToBoolean(value);
                } else {
                return defaultVal;
            }
            } catch(Exception) {
            return defaultVal;
        }
    }
    
    //Object轉換成String
    public static string LoadString(object value, string defaultVal = "")
    {
        try {
            if ( value != null ) {
                return Convert.ToString(value);
                } else {
                return defaultVal;
            }
            } catch(Exception) {
            return defaultVal;
        }
    }

    public static string[] ToStringArray(object arg) 
    {
	  var collection = arg as IEnumerable;
	  if (collection != null) {
	    return collection
	      .Cast<object>()
	      .Select(x => x.ToString())
	      .ToArray();
	  }

	  if (arg == null) {
	    return new string[] { };
	  }

	  return new string[] { arg.ToString() };
	}

    //<summary>產生TimeStamp</summary>
    public static long GetTimestamp(DateTime value)
    {
        TimeSpan span = (value - new DateTime(1970, 1, 1, 0 ,0 ,0 ,0).ToLocalTime());
        return ((long)span.TotalSeconds);
    }

    public static long GetTimeDifferencesInMinutes(long value)
    {
    	DateTime dt = ReturnTimestamp(value);
    	TimeSpan span = DateTime.Now.Subtract(dt);
    	return (long)span.TotalMinutes;
    }
    
    //<summary>轉換TimeStamp</summary>
    public static DateTime ReturnTimestamp(long value)
    {
        DateTime dt = new DateTime(1970,1, 1, 0, 0, 0,0).AddSeconds(value).ToLocalTime();
        return dt;
    }  

    //<summary>目前時間與UTC時間的小時差</summary>
    public static double GetUtcOffsetHour()
    {
    	TimeSpan tSpan = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
        return tSpan.TotalHours;
    }

    public static long GetUtcTimestamp()
    {
        double timeStamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds; 
        return (long)timeStamp;
    }

    public static string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);
	 
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
	 
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
	 
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
	 
		return hashString.PadLeft(32, '0');
	}

	public static int GetResponseCode(WWW request) 
	{
	  int ret = 0;
	  if (request.responseHeaders == null) {
	    Debug.LogError("no response headers.");
	  }
	  else {
	    if (!request.responseHeaders.ContainsKey("STATUS")) {
	      Debug.LogError("response headers has no STATUS.");
	    }
	    else {
	      ret = ParseResponseCode(request.responseHeaders["STATUS"]);
	    }
	  }

	  return ret;
	}

	public static int ParseResponseCode(string statusLine) 
	{
	  int ret = 0;

	  string[] components = statusLine.Split(' ');
	  if (components.Length < 3) {
	    Debug.LogError("invalid response status: " + statusLine);
	  }
	  else {
	    if (!int.TryParse(components[1], out ret)) {
	      Debug.LogError("invalid response code: " + components[1]);
	    }
	  }

	  return ret;
	}

	public static string ColorToHex(Color32 color)
	{
		string hex = "#" + color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		return hex;
	}
}

